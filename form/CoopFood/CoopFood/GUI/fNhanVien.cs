﻿using CoopFood.DAO;
using CoopFood.DTO;
using CoopFood.Enumerates;
using CoopFood.Utills;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoopFood
{
    public partial class fNhanVien : Form
    {
        public fNhanVien()
        {
            InitializeComponent();

            InitFromDefault();
        }

        private async void InitFromDefault()
        {
            await LoadNhanVien();

            dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            NhanVienBinding();
            ChucVuDAO.Instance.ThemDanhSachChucVuVaoComboBox(cbTenChucVu);
        }

        private async Task LoadNhanVien(string tenNV = null) => dtgvNhanVien.DataSource = await NhanVienDAO.Instance.DanhSachNhanVien(tenNV);
       
        void NhanVienBinding()
        {
            txtMaNhanVien.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "MaNV", true, DataSourceUpdateMode.Never));
            txtTenNhanVien.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "TenNV", true, DataSourceUpdateMode.Never));
            cbGioiTinh.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "GioiTinh", true, DataSourceUpdateMode.Never));
            dtpNgaySinh.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "NgaySinh", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "DiaChi", true, DataSourceUpdateMode.Never));
            txtCMND.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "CMND", true, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "Email", true, DataSourceUpdateMode.Never));
            txtSoDienThoai.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            dtpNgayVaoLam.DataBindings.Add(new Binding("Value", dtgvNhanVien.DataSource, "NgayVaoLam", true, DataSourceUpdateMode.Never));
            cbTenChucVu.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "MaCV", true, DataSourceUpdateMode.Never));
        }

        private async void btnTao_Click(object sender, EventArgs e)
        {
            var maNV = (await NhanVienDAO.Instance.DanhSachNhanVien(null)).Max(m => m.MaNV) + 1;

            txtMaNhanVien.Text = maNV.ToString();
            txtTenNhanVien.Text = "";
            cbGioiTinh.Text = "";
            dtpNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtCMND.Text = "";
            txtEmail.Text = "";
            txtSoDienThoai.Text = "";
            dtpNgayVaoLam.Text = "";
            cbTenChucVu.Text = "";
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            var result = new Result();

            try
            {
                var employee = new NhanVien()
                {
                    MaNV = Int32.Parse(txtMaNhanVien.Text),
                    TenNV = txtTenNhanVien.Text,
                    GioiTinh = cbGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text,
                    CMND = txtCMND.Text,
                    Email = txtEmail.Text,
                    SDT = txtSoDienThoai.Text,
                    NgayVaoLam = dtpNgayVaoLam.Value,
                    MaCV = Int32.Parse(cbTenChucVu.SelectedValue.ToString())
                };

                if ((await NhanVienDAO.Instance.DanhSachNhanVien(null)).Find(x => x.MaNV == employee.MaNV) == null)
                    result = NhanVienDAO.Instance.ThemNhanVien(employee);
                else
                    result = NhanVienDAO.Instance.SuaNhanVien(employee);

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
                    AfterCommit(NhanVienDAO.Instance.XoaNhanVien(Int32.Parse(txtMaNhanVien.Text)));
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
                _ = LoadNhanVien();
            }
            else if (result.IsSuccessed == false)
                MessageBoxUtil.ShowMessageBox(result.Message, MessageBoxType.Error);
        }

        private async void btnTimKiem_Click(object sender, EventArgs e) => await LoadNhanVien(txtTimKiem.Text);
    }
}
