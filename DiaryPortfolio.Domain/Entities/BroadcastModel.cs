using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class BroadcastModel
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? GroupId { get; set; } = null;
        public string? UserId { get; set; } = null;

    }
}
