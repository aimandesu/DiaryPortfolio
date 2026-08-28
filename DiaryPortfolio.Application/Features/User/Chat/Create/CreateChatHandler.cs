using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Chat.Create
{
    public class CreateChatHandler : IRequestHandler<CreateChatRequest, ResultResponse<string>>
    {
        private readonly IChatNotifier _chatNotifier;
        private readonly IConversationRepository _conversationService;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateChatHandler(
            IChatNotifier chatNotifier,
            IConversationRepository conversationService,
            IChatMessageRepository chatMessageRepository,
            IUnitOfWork unitOfWork)
        {
            _chatNotifier = chatNotifier;
            _conversationService = conversationService;
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<string>> Handle(
            CreateChatRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var conversation = await _conversationService.GetConversation(
                    request.MessageUpload.ConversationId ?? Guid.Empty);

                if (conversation.Result == null)
                {
                    var result = await _conversationService.CreateConversation(
                        request.MessageUpload.ConversationEnum.ToString(),
                        request.MessageUpload.UserIds);

                    if (result.Error != Error.None)
                    {
                        return ResultResponse<string>.Failure(
                            result.Error);
                    }

                    conversation = result!;
                }

                //save message to db - pass the conversation id
                var chatMessage = await _chatMessageRepository.CreateMessage(
                    request.MessageUpload,
                    conversation?.Result?.Id ?? Guid.Empty);

                if (chatMessage.Error != Error.None)
                {
                    return ResultResponse<string>.Failure(
                            chatMessage.Error);
                }

                //broadcast needs to take from the conversation variable instead of directly
                var broadcast = new BroadcastModel
                {
                    MessageId = chatMessage.Result.Id,
                    ConversationId = conversation?.Result?.Id.ToString(),
                    Message = request.MessageUpload.Message,
                };

                await _chatNotifier.SendMessageToConversation(broadcast);

                ////change to enum later, switch case

                //if (!string.IsNullOrEmpty(request.BroadcastModel.UserId))
                //{
                //    await _chatNotifier.BroadcastMessagePrivate(request.BroadcastModel);
                //}
                //else if (!string.IsNullOrEmpty(request.BroadcastModel.GroupId))
                //{
                //    await _chatNotifier.BroadcastMessageGroup(request.BroadcastModel);
                //}
                //else
                //{
                //    await _chatNotifier.BroadcastMessageAll(request.BroadcastModel);
                //}

                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<string>.Success(conversation?.Result?.Id.ToString() ?? "");
            }
            catch (Exception ex)
            {
                return ResultResponse<string>.Failure(
                    new Error(HttpStatusCode.Conflict, ex.Message));
            }
        }
    }
}
