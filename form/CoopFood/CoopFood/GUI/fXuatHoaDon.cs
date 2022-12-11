using CoopFood.DAO;
using CoopFood.DTO;
using CoopFood.Enumerates;
using CoopFood.Utills;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
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
        }

        private async Task LoadXuatHoaDon(string keySearch = null)
        {
            decimal totalMoney = 0;
            var result = await HoaDonDAO.Instance.DanhSachHoaDon(keySearch);
            foreach (var item in result)
                totalMoney += item.SoLuongBan * item.GiaSP;

            txtTongTien.Text = totalMoney.ToString();
            dtgvCTHD.DataSource = result;

            NhanVienDAO.Instance.ThemDanhSachNhanvienVaoComboBox(cbTenNhanVien);
            KhachHangDAO.Instance.ThemDanhSachKhachHangVaoComboBox(cbTenKhachHang);
            SanPhamDAO.Instance.ThemDanhSachSanPhamVaoComboBox(cbTenSanPham);
        }

        private async void btnTao_Click(object sender, EventArgs e)
        {
            var maHD = (await HoaDonDAO.Instance.DanhSachHoaDon(null)).Max(m => m.MaHD) + 1;

            txtMaHoaDon.Text = maHD.ToString();
            cbTenNhanVien.Text = "";
            cbTenKhachHang.Text = "";
            cbTenSanPham.Text = "";
            txtSoLuong.Text = "";
            txtGiaBan.Text = "";
            dtpNgayLap.Value = DateTime.Now;
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
                    NgayMua = dtpNgayLap.Value,

                    MaSP = int.Parse(cbTenSanPham.SelectedValue.ToString()),
                    SoLuongBan = int.Parse(txtSoLuong.Text),
                    GiaSP = decimal.Parse(txtGiaBan.Text),
                };
                hoadon.TongTien = hoadon.SoLuongBan * hoadon.GiaSP;

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

        private void dtgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgvCTHD.Rows[e.RowIndex];

                txtMaHoaDon.Text = row.Cells["MaHD"].Value.ToString();

                cbTenNhanVien.SelectedValue = int.Parse(row.Cells["MaNV"].Value.ToString());
                cbTenNhanVien.Text = row.Cells["TenNV"].Value.ToString();

                cbTenKhachHang.SelectedValue = int.Parse(row.Cells["MaKH"].Value.ToString());
                cbTenKhachHang.Text = row.Cells["TenKH"].Value.ToString();

                cbTenSanPham.SelectedValue = int.Parse(row.Cells["MaSP"].Value.ToString());
                cbTenSanPham.Text = row.Cells["TenSP"].Value.ToString();

                txtSoLuong.Text = row.Cells["SoLuongBan"].Value.ToString();
                txtGiaBan.Text = row.Cells["GiaSP"].Value.ToString();
                dtpNgayLap.Value = DateTime.Parse(row.Cells["NgayMua"].Value.ToString());
                txtTongTien.Text = row.Cells["TongTien"].Value.ToString();
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dtgvCTHD.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "XuatHoaDon" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dtgvCTHD.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dtgvCTHD.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dtgvCTHD.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Xuất dữ liệu hoá đơn thành công");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu xuất");
            }
        }

    }

}
