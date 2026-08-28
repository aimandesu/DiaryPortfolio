using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IServices
{
    public interface IChatGroupService
    {
        Task JoinConversation(
            string connectionId, 
            Guid roomId);

        Task LeaveConversation(
            string connectionId,
            Guid roomId);
    }
}
