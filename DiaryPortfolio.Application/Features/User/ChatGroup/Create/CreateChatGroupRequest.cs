using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.ChatGroup.Create
{
    public sealed record class CreateChatGroupRequest(
        string ChatName);
}
