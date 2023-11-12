using BL.BaseBL;
using BL.SachBL;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;

namespace book_back.Controllers
{
    public class SachController : BaseController<Sach>
    {
        private readonly ISachBL _sach;

        public SachController(ISachBL sachBL) : base(sachBL)
        {
            _sach = sachBL;
        }
    }
}
