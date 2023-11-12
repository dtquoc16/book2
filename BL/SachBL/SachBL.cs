using BL.BaseBL;
using Common.Entity;
using DL.BaseDL;
using DL.SachDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SachBL
{
    public class SachBL : BaseBL<Sach>, ISachBL
    {
        private readonly ISachDL _sachDL;
        public SachBL(ISachDL sachDL) : base(sachDL)
        {
            _sachDL = sachDL;
        }
    }
}
