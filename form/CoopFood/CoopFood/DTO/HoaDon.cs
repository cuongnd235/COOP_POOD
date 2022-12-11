using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoopFood.DTO
{
    public class HoaDon
    {
        public int MaHD { get; set; }
        public int MaNV { get; set; }
        public int MaKH { get; set; }
        public DateTime NgayMua { get; set; }
        public decimal TongTien { get; set; }
        public int MaSP { get; set; }
        public int SoLuongBan { get; set; }
        public decimal GiaSP { get; set; }

    }

    public class HoaDonRes
    {
        public int MaHD { get; set; }
        public int MaNV { get; set; }
        public int MaKH { get; set; }
        public int MaSP { get; set; }
        public DateTime NgayMua { get; set; }
        public decimal TongTien { get; set; }
    }
}
