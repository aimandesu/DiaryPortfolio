using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequireOwnershipAttribute : Attribute
    {
        public Type ResourceType { get; }
        public RequireOwnershipAttribute(Type resourceType)
        {
            ResourceType = resourceType;
        }
    }
}
