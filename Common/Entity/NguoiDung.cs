﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity
{
    public class NguoiDung
    {
        public Guid NguoiDungId { get; set; }

        public string TenTaiKhoan { get; set; }

        public string MatKhau { get; set; }

        public string HoTen { get; set; }

        public string SoDienThoai { get; set; }

        public string DiaChi { get; set; }

        public int VaiTro { get; set; }
    }
}
