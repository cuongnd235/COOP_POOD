﻿
namespace CoopFood
{
    partial class fThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartDoanhthu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartLuongNhanVien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbDieuKienLoc = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhthu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLuongNhanVien)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDoanhthu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhthu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDoanhthu.Legends.Add(legend1);
            this.chartDoanhthu.Location = new System.Drawing.Point(28, 194);
            this.chartDoanhthu.Name = "chartDoanhthu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "VND";
            this.chartDoanhthu.Series.Add(series1);
            this.chartDoanhthu.Size = new System.Drawing.Size(504, 413);
            this.chartDoanhthu.TabIndex = 0;
            this.chartDoanhthu.Text = "chart1";
            // 
            // chartLuongNhanVien
            // 
            chartArea2.Name = "ChartArea1";
            this.chartLuongNhanVien.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartLuongNhanVien.Legends.Add(legend2);
            this.chartLuongNhanVien.Location = new System.Drawing.Point(642, 194);
            this.chartLuongNhanVien.Name = "chartLuongNhanVien";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "VND";
            this.chartLuongNhanVien.Series.Add(series2);
            this.chartLuongNhanVien.Size = new System.Drawing.Size(495, 413);
            this.chartLuongNhanVien.TabIndex = 1;
            this.chartLuongNhanVien.Text = "chart2";
            // 
            // cbDieuKienLoc
            // 
            this.cbDieuKienLoc.FormattingEnabled = true;
            this.cbDieuKienLoc.Items.AddRange(new object[] {
            "Quý",
            "Năm"});
            this.cbDieuKienLoc.Location = new System.Drawing.Point(812, 122);
            this.cbDieuKienLoc.Name = "cbDieuKienLoc";
            this.cbDieuKienLoc.Size = new System.Drawing.Size(209, 24);
            this.cbDieuKienLoc.TabIndex = 34;
            this.cbDieuKienLoc.DropDownClosed += new System.EventHandler(this.cbDieuKienLoc_DropDownClosed);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(391, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(291, 41);
            this.label12.TabIndex = 35;
            this.label12.Text = "BIỂU ĐỒ THỐNG KÊ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(807, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 25);
            this.label1.TabIndex = 36;
            this.label1.Text = "Điều kiện lọc:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(145, 623);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 25);
            this.label2.TabIndex = 37;
            this.label2.Text = "Biểu đồ báo cáo doanh thu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(736, 623);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(285, 25);
            this.label3.TabIndex = 38;
            this.label3.Text = "Biểu đồ báo cáo lương nhân viên";
            // 
            // fThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 705);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDieuKienLoc);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.chartLuongNhanVien);
            this.Controls.Add(this.chartDoanhthu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fThongKe";
            this.Text = "fThongKe";
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhthu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLuongNhanVien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhthu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLuongNhanVien;
        private System.Windows.Forms.ComboBox cbDieuKienLoc;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}