using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BaseBL
{
    public interface IBaseBL<T>
    {
        //Task<IEnumerable<T>> GetAllRecordsAsync();

        Task<T> GetRecordByIdAsync(Guid recordId);

        Task InsertRecordAsync(T record);

        Task UpdateRecordAsync(Guid recordId, T record);

        Task DeleteMultiAsync(List<Guid> listId);
    }
}
