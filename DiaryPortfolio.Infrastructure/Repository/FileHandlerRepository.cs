using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.FileDistributor;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class FileHandlerRepository : IFileHandlerRepository
    {
        private readonly IFilePathHandlerRepository _filePathHandlerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHandlerRepository(
            IFilePathHandlerRepository filePathHandlerRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _filePathHandlerRepository = filePathHandlerRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteFile(string filePath)
        {
            var fullPath = Path.Combine(
                _webHostEnvironment.ContentRootPath,
                filePath.TrimStart('/')
            );

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public void DeleteFiles(List<string> tempFilePaths)
        {
            foreach (var path in tempFilePaths)
            {
                DeleteFile(path);
            }
        }

        public async Task<ResultResponse<List<Dictionary<MediaSubType, MediaDistributor>>>> DistributeFiles(
            List<MediaStream> fileStreams,
            MediaType mediaType,
            string? id = null)
        {
            var imagesAllowed = AllowedContentTypes.Images;

            var videosAllowed = AllowedContentTypes.Videos;

            var filesAllowed = AllowedContentTypes.Documents;

            List<Dictionary<MediaSubType, MediaDistributor>> distributedFiles = [];

            foreach (var media in fileStreams)
            {
                string fileName = media.FileName;
                Stream fileStream = media.Stream;

                string fileExtension = Path.GetExtension(fileName).ToLower();
                if (imagesAllowed.Contains(fileExtension))
                {
                    // Logic to save image and get its path
                    string imagePath = await SaveFileAndGetPath(
                        media, // pass Stream and FileName
                        mediaType, // pass MediaType from controller
                        MediaSubType.Image,
                        id);

                    var metadata = ReadMediaMetadata(imagePath, MediaSubType.Image, fileExtension);

                    distributedFiles.Add(new Dictionary<MediaSubType, MediaDistributor>
                    {
                        {
                            MediaSubType.Image,
                            new MediaDistributor
                            {
                                Photos = new PhotoModel
                                {
                                    Url = "/" + imagePath.Replace("\\", "/"),
                                    Mime = metadata.Mime,
                                    Width = double.Parse(metadata.Width ?? "0"),
                                    Height = double.Parse(metadata.Height ?? "0"),
                                    Size = metadata.Size
                                }
                            }
                        }
                    });
                }
                else if (videosAllowed.Contains(fileExtension.ToLower()))
                {
                    // Logic to save video and get its path
                    string videoPath = await SaveFileAndGetPath(
                        media, // pass Stream and FileName
                        mediaType, // pass MediaType from controller
                        MediaSubType.Video,
                        id);

                    var metadata = ReadMediaMetadata(videoPath, MediaSubType.Video, fileExtension);

                    distributedFiles.Add(new Dictionary<MediaSubType, MediaDistributor>
                    {
                        {
                            MediaSubType.Video,
                            new MediaDistributor
                            {
                                Videos = new VideoModel
                                {
                                    Url = "/" + videoPath.Replace("\\", "/"),
                                    Mime = metadata.Mime,
                                    Size = metadata.Size,
                                    Duration = (int)(metadata.Duration ?? 0)
                                }
                            }
                        }
                    });
                }
                else if (filesAllowed.Contains(fileExtension.ToLower()))
                {
                    string filePath = await SaveFileAndGetPath(
                        media,
                        mediaType,
                        MediaSubType.File,
                        id);

                    distributedFiles.Add(new Dictionary<MediaSubType, MediaDistributor>
                    {
                        {
                            MediaSubType.File,
                            new MediaDistributor
                            {
                                Files = new FileModel
                                {
                                    Url = "/" + filePath.Replace("\\", "/"),
                                }
                            }
                        }
                    });
                }
                else
                {
                    return ResultResponse<List<Dictionary<MediaSubType, MediaDistributor>>>.Failure(
                        new Error(
                              Status: System.Net.HttpStatusCode.UnsupportedMediaType,
                              Description: $"The file type '{fileExtension}' is not supported."
                          )
                      );
                }

            }

            return ResultResponse<List<Dictionary<MediaSubType, MediaDistributor>>>.Success(
                distributedFiles);
        }

        public MediaMetadata ReadMediaMetadata(
            string filePath, 
            MediaSubType mediaSubType,
            string fileExtension)
        {
            var absolutePath = Path.Combine(_webHostEnvironment.ContentRootPath, filePath);

            using (FileStream fs = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
            {
                var metadata = new MediaMetadata
                {
                    Size = fs.Length,
                    Mime = fileExtension
                };

                switch (mediaSubType)
                {
                    case MediaSubType.Image:
                        fs.Position = 0;
                        using (Image image = Image.Load(fs))
                        {
                            metadata.Width = image.Width.ToString();
                            metadata.Height = image.Height.ToString();
                        }
                        break;
                    case MediaSubType.Video:
                        var ffProbe = new NReco.VideoInfo.FFProbe();
                        var videoInfo = ffProbe.GetMediaInfo(absolutePath);

                        // Find the video stream (in case there are multiple streams)
                        var videoStream = videoInfo.Streams.FirstOrDefault(s => s.CodecType == "video");

                        if (videoStream != null)
                        {
                            metadata.Width = videoStream.Width.ToString();
                            metadata.Height = videoStream.Height.ToString();
                            metadata.Duration = videoInfo.Duration.TotalSeconds;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mediaSubType), mediaSubType, null);
                }
                return metadata;
            }
        }

        private async Task<string> SaveFileAndGetPath(
            MediaStream media,
            MediaType mediaType,
            MediaSubType mediaSubType,
            string? id = null)
        {
            var path = _filePathHandlerRepository.BuildPath(
                mediaType,
                mediaSubType,
                media.FileName,
                id: id
            );

            var absolutePath = Path.Combine(_webHostEnvironment.ContentRootPath, path); //writing to server and local

            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

            media.Stream.Position = 0;
            await using var fs = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);
            await media.Stream.CopyToAsync(fs);

            return path;
        }

    }
}
