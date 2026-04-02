using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.FileDistributor;
using DiaryPortfolio.Application.IRepository.IFileHandlerRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository.FileHandler
{
    public class FileHandlerRepository : IFileHandlerRepository
    {
        private readonly IFilePathHandlerRepository _filePathHandlerRepository;

        public FileHandlerRepository(
            IFilePathHandlerRepository filePathHandlerRepository)
        {
            _filePathHandlerRepository = filePathHandlerRepository;
        }

        public void DeleteFile(string filePath)
        {
            var fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
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
            MediaType mediaType)
        {
            var imagesAllowed = new List<string>
            {
                ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg"
            };

            var videosAllowed = new List<string>
            {
                ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv"
            };

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
                        MediaSubType.Image);

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
                        MediaSubType.Video);

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
                else
                {
                    return ResultResponse<List<Dictionary<MediaSubType, MediaDistributor>>>.Failure(
                        new Error(
                              Code: "UnsupportedFileType",
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
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
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
                        var videoInfo = ffProbe.GetMediaInfo(filePath);

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
            MediaSubType mediaSubType)
        {
            var path = _filePathHandlerRepository.BuildPath(
                mediaType,
                mediaSubType,
                media.FileName
            );

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            media.Stream.Position = 0;
            await using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            await media.Stream.CopyToAsync(fs);

            return path;
        }

    }
}
