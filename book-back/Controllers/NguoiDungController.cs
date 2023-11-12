using BL.BaseBL;
using BL.NguoiDungBL;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;

namespace book_back.Controllers
{
    public class NguoiDungController : BaseController<Common.Entity.NguoiDung>
    {
        private INguoiDungBL _nguoiDungBL;
        public NguoiDungController(INguoiDungBL nguoiDungBL) : base(nguoiDungBL)
        {
            _nguoiDungBL = nguoiDungBL;
        }
    }
}
