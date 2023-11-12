using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.BaseDL
{
    public interface IBaseDL<T>
    {
        string GetTableName();

        Task<IEnumerable<T>> GetAllRecordsAsync();

        Task<T> GetRecordByIdAsync(Guid recordId);

        Task InsertRecordAsync(T record);

        Task UpdateRecordAsync(Guid recordId, T record);

        Task DeleteMultiAsync(List<Guid> listId);
    }
}
