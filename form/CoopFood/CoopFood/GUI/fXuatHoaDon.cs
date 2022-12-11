using CoopFood.DAO;
using CoopFood.DTO;
using CoopFood.Enumerates;
using CoopFood.Utills;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoopFood
{
    public partial class fXuatHoaDon : Form
    {
        public fXuatHoaDon()
        {
            InitializeComponent();
            InitFromDefault();
        }

        private async void InitFromDefault()
        {
            await LoadXuatHoaDon();

            dtgvCTHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            NhaCungCapBinding();
        }

        BindingSource danhsachCTHD = new BindingSource();

        private async Task LoadXuatHoaDon(string maHD = null)
        {
            dtgvCTHD.DataSource = await HoaDonDAO.Instance.DanhSachHoaDon(maHD);
            //danhsachCTHD.DataSource = CTHDDAO.Instance.DanhSachCTHD(702);
            //danhsachHoaDon.DataSource = await HoaDonDAO.Instance.DanhSachHoaDon(maHD);

            NhanVienDAO.Instance.ThemDanhSachNhanvienVaoComboBox(cbTenNhanVien);
            KhachHangDAO.Instance.ThemDanhSachKhachHangVaoComboBox(cbTenKhachHang);
            SanPhamDAO.Instance.ThemDanhSachSanPhamVaoComboBox(cbTenSanPham);
            DonViDAO.Instance.ThemDanhSachDonViVaoComboBox(cbDonVi);
        }

        void NhaCungCapBinding()
        {
            dtpNgayLap.DataBindings.Add(new Binding("Value", dtgvCTHD.DataSource,"NgayMua", true, DataSourceUpdateMode.Never));
            txtMaHoaDon.DataBindings.Add(new Binding("Text", dtgvCTHD.DataSource, "MaHD", true, DataSourceUpdateMode.Never));
            cbTenNhanVien.DataBindings.Add(new Binding("Text", dtgvCTHD.DataSource, "MaNV", true, DataSourceUpdateMode.Never));
            //cbLoaiSanPham.DataBindings.Add(new Binding("Text", dtgvCTHD.DataSource, "MaLSP", true, DataSourceUpdateMode.Never));
            //cbTenSanPham.DataBindings.Add(new Binding("Text", dtgvCTHD.DataSource, "TenSP", true, DataSourceUpdateMode.Never));
            //cbDonVi.DataBindings.Add(new Binding("Text", dtgvCTHD.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            txtSoLuong.DataBindings.Add(new Binding("Text", dtgvCTHD.DataSource, "SoLuongBan", true, DataSourceUpdateMode.Never));
            txtGiaBan.DataBindings.Add(new Binding("Text", dtgvCTHD.DataSource, "GiaSP", true, DataSourceUpdateMode.Never));
        }

        private async void btnTao_Click(object sender, EventArgs e)
        {
            var maHD = (await HoaDonDAO.Instance.DanhSachHoaDon(null)).Max(m => m.MaHD) + 1;

            txtMaHoaDon.Text = maHD.ToString();
            cbTenNhanVien.Text = "";
            cbTenKhachHang.Text = "";
            cbTenSanPham.Text = "";
            cbDonVi.Text = "";
            txtSoLuong.Text = "";
            txtGiaBan.Text = "";
            txtTongTien.Text = "";
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            var result = new Result();

            try
            {
                var hoadon = new HoaDon()
                {
                    MaHD = int.Parse(txtMaHoaDon.Text),
                    MaNV = int.Parse(cbTenNhanVien.SelectedValue.ToString()),
                    MaKH = int.Parse(cbTenKhachHang.SelectedValue.ToString()),
                    MaSP = int.Parse(cbTenSanPham.SelectedValue.ToString()),
                    NgayMua = dtpNgayLap.Value,
                    TongTien = int.Parse(txtTongTien.Text)
                };

                if ((await HoaDonDAO.Instance.DanhSachHoaDon(null)).Find(x => x.MaHD == hoadon.MaHD) == null)
                    result = HoaDonDAO.Instance.ThemHoaDon(hoadon);
                else
                    result = HoaDonDAO.Instance.SuaHoaDon(hoadon);

                AfterCommit(result);
            }
            catch
            {
                MessageBoxUtil.ShowMessageBox("Hệ thống tạm thời gián đoạn. Vui lòng thử lại sau", MessageBoxType.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    AfterCommit(HoaDonDAO.Instance.XoaHoaDon(Int32.Parse(txtMaHoaDon.Text)));
                }
            }
            catch
            {
                MessageBoxUtil.ShowMessageBox("Hệ thống tạm thời gián đoạn. Vui lòng thử lại sau", MessageBoxType.Error);
            }
        }

        private void AfterCommit(Result result)
        {
            if (result.IsSuccessed)
            {
                MessageBoxUtil.ShowMessageBox(result.Message, MessageBoxType.Information);
                _ = LoadXuatHoaDon();
            }
            else if (result.IsSuccessed == false)
                MessageBoxUtil.ShowMessageBox(result.Message, MessageBoxType.Error);
        }

        private async void btnTimKiem_Click(object sender, EventArgs e) => await LoadXuatHoaDon(txtTimKiem.Text);
    }
}
