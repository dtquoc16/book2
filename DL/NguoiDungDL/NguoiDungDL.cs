using Common.Entity;
using DL.BaseDL;
using DL.NguoiDungDL;
using DL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.NguoiDung
{
    public class NguoiDungDL : BaseDL<Common.Entity.NguoiDung>, INguoiDungDL
    {
        public NguoiDungDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override string GetTableName()
        {
            return "nguoidung";
        }
    }
}
