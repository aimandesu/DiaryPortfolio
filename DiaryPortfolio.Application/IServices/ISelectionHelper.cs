using DiaryPortfolio.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IServices
{
    public interface ISelectionHelper
    {
        Task<Guid> GetSelectionIdAsync<TEnum>(
            TEnum enumValue, 
            CancellationToken cancellationToken = default)
            where TEnum : Enum;

        Task<SelectionResult?> GetSelectionResultAsync<TEnum>(
            TEnum enumValue, 
            CancellationToken cancellationToken = default)
            where TEnum : Enum;
    }
}
