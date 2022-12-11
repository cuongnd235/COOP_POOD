using CoopFood.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopFood.DAO
{
    public class HoaDonDAO
    {
        private static HoaDonDAO instance;

        public static HoaDonDAO Instance
        {
            get { if (instance == null) instance = new HoaDonDAO(); return instance; }
            set { instance = value; }
        }
        private HoaDonDAO() { }

        public async Task<List<HoaDon>> DanhSachHoaDon(string maHD)
        {
            string sql = "SELECT h.*, ct.* FROM HD h join CT_HD ct ON h.MaHD = ct.MaHD {0}";
            sql = string.IsNullOrWhiteSpace(maHD) ? string.Format(sql, String.Empty) : string.Format(sql, $" WHERE MaHD like '%{maHD}%'");

            return await DataProvider.Instance.SqlQueryAsync<HoaDon>(sql);
        }

        public Result ThemHoaDon(HoaDon hoaDon)
        {
            string sql = @"INSERT INTO HD (MaHD, MaNV, MaKH, NgayMua, ) VALUES ({0}, {1}, {2}, '{3}', {4}";
            string query = string.Format(sql, hoaDon.MaHD, hoaDon.MaNV, hoaDon.MaKH, hoaDon.NgayMua, hoaDon.TongTien);

            var result = DataProvider.Instance.ExecuteNonQuery(query);

            return new Result()
            {
                IsSuccessed = result > 0,
                Message = result > 0 ? "Thêm mới thành công" : "Thêm mới thất bại. Vui lòng thử lại sau."
            };
        }

        public Result SuaHoaDon(HoaDon hoaDon)
        {
            string sql = @"UPDATE HD SET MaNV = {0}, MaKH = {1}, NgayMua = '{2}', TongTien = {3} WHERE MaHD = {4};";

            string query = string.Format(sql, hoaDon.MaNV, hoaDon.MaKH, hoaDon.NgayMua, hoaDon.TongTien, hoaDon.MaHD);

            var result = DataProvider.Instance.ExecuteNonQuery(query);

            return new Result()
            {
                IsSuccessed = result > 0,
                Message = result > 0 ? "Sửa thành công" : "Sửa thất bại. Vui lòng thử lại sau."
            };
        }

        public Result XoaHoaDon(int maHD)
        {
            var result = DataProvider.Instance.ExecuteNonQuery($"DELETE FROM HD WHERE MaHD = '{maHD}'");

            return new Result()
            {
                IsSuccessed = result > 0,
                Message = result > 0 ? "Xoá thành công" : "Xoá thất bại. Vui lòng thử lại sau."
            };
        }
    }
}
