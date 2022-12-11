using CoopFood.DTO;
using System;
using System.Collections.Generic;
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

        public async Task<List<HoaDon>> DanhSachHoaDon(string keySearch)
        {
            string sql = @"SELECT hd.*, ct.*, nv.TenNV, kh.TenKH, sp.TenSP, sp.GiaBan, dvt.TenDV AS TenDVT FROM HD hd
            JOIN CT_HD ct ON hd.MaHD = ct.MaHD
            JOIN NHANVIEN nv ON hd.MaNV = nv.MaNV
            JOIN KHACHHANG kh ON hd.MaKH = kh.MaKH
            JOIN SANPHAM sp ON ct.MaSP = sp.MaSP
            LEFT JOIN DONVITINH dvt ON sp.MaDVT = dvt.MaDVT {0}";
            sql = string.IsNullOrWhiteSpace(keySearch) ? string.Format(sql, String.Empty) : string.Format(sql, $" WHERE kh.TenKH like N'%{keySearch}%' or nv.TenNV like N'%{keySearch}%' or hd.MaHD like '%{keySearch}%'");

            return await DataProvider.Instance.SqlQueryAsync<HoaDon>(sql);
        }

        public Result ThemHoaDon(HoaDon hoaDon)
        {
            int result = 0;

            string query = string.Format("INSERT INTO HD (MaHD, MaNV, MaKH, NgayMua, TongTien) VALUES ({0}, {1}, {2}, '{3}', {4})", hoaDon.MaHD, hoaDon.MaNV, hoaDon.MaKH, hoaDon.NgayMua, hoaDon.TongTien);
            result = DataProvider.Instance.ExecuteNonQuery(query);

            string queryUpdateCTHD = string.Format("INSERT INTO CT_HD (MaHD, MaSP, SoLuongBan) VALUES ({0}, {1}, {2})", hoaDon.MaHD, hoaDon.MaSP, hoaDon.SoLuongBan);
            result = DataProvider.Instance.ExecuteNonQuery(queryUpdateCTHD);

            string queryUpdateTichLuy = string.Format("UPDATE KHACHHANG SET TichLuy = {0} WHERE MaKH = {1}", (int)(hoaDon.SoLuongBan*hoaDon.GiaBan / 10000), hoaDon.MaKH);
            result = DataProvider.Instance.ExecuteNonQuery(queryUpdateTichLuy);

            return new Result()
            {
                  IsSuccessed = result > 0,
                Message = result > 0 ? "Thêm mới thành công" : "Thêm mới thất bại. Vui lòng thử lại sau."
            };
        }

        public Result SuaHoaDon(HoaDon hoaDon)
        {
            int result = 0;

            string query = string.Format("UPDATE HD SET MaNV = {0}, MaKH = {1}, NgayMua = '{2}', TongTien = {3} WHERE MaHD = {4}", hoaDon.MaNV, hoaDon.MaKH, hoaDon.NgayMua, hoaDon.TongTien, hoaDon.MaHD);
            result = DataProvider.Instance.ExecuteNonQuery(query);

            string query1 = string.Format("UPDATE CT_HD SET MaSP = {0}, SoLuongBan = {1} WHERE MaHD = {2} and MaSP = {3}", hoaDon.MaSP, hoaDon.SoLuongBan, hoaDon.MaHD, hoaDon.MaSP);
            result = DataProvider.Instance.ExecuteNonQuery(query1);

            return new Result()
            {
                IsSuccessed = result > 0,
                Message = result > 0 ? "Sửa thành công" : "Sửa thất bại. Vui lòng thử lại sau."
            };
        }

        public Result XoaHoaDon(int maHD, int maSP)
        {
            var result = DataProvider.Instance.ExecuteNonQuery($"DELETE hd FROM HD hd INNER JOIN CT_HD ct ON hd.MaHD = hd.MaHD WHERE hd.MaHD = {maHD} and ct.MaSP = {maSP}");

            return new Result()
            {
                IsSuccessed = result > 0,
                Message = result > 0 ? "Xoá thành công" : "Xoá thất bại. Vui lòng thử lại sau."
            };
        }
    }
}
