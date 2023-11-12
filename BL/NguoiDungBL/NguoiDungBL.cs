using BL.BaseBL;
using Common.Entity;
using DL.BaseDL;
using DL.NguoiDungDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.NguoiDungBL
{
    public class NguoiDungBL : BaseBL<Common.Entity.NguoiDung>, INguoiDungBL
    {
        private readonly INguoiDungDL _nguoiDungDL;
        public NguoiDungBL(INguoiDungDL nguoiDungDL) : base(nguoiDungDL)
        {
            _nguoiDungDL = nguoiDungDL;
        }
    }
}
