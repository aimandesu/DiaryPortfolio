using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IServices
{
    public interface IChatNotifier
    {
        Task BroadcastMessageAll(BroadcastModel broadcastModel);
        Task BroadcastMessagePrivate(BroadcastModel broadcastModel);
        Task BroadcastMessageGroup(BroadcastModel broadcastModel);
        Task DeleteMessage(BroadcastModel broadcastModel);
    }
}
