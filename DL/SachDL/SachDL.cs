using Common.Entity;
using DL.BaseDL;
using DL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.SachDL
{
    public class SachDL : BaseDL<Sach>, ISachDL
    {
        public SachDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override string GetTableName()
        {
            return "sanpham";
        }
    }
}
