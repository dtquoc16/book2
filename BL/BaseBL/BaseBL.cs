using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.BaseDL;
using DL.UnitOfWork;

namespace BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        protected readonly IBaseDL<T> _baseDL;

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        public async Task DeleteMultiAsync(List<Guid> listId)
        {
            await _baseDL.DeleteMultiAsync(listId);
        }

        //public Task<IEnumerable<T>> GetAllRecordsAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<T> GetRecordByIdAsync(Guid recordId)
        {
           return  await _baseDL.GetRecordByIdAsync(recordId);
        }

        public async Task InsertRecordAsync(T record)
        {
            await _baseDL.InsertRecordAsync(record);
        }

        public async Task UpdateRecordAsync(Guid recordId, T record)
        {
            await _baseDL.UpdateRecordAsync(recordId, record);
        }
    }
}
