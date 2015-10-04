using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace WindowsForms_MoPhongKhoanNoMin.GUILayer
{
    public partial class FormXayDungLoKhoan1 : DevExpress.XtraEditors.XtraForm
    {
        private int huongVe;
        List<RadioButton> radioButtonTemplate = new List<RadioButton>();

        // khai báo 1 hàm delegate
        public delegate void GetData(int soHang, int soCot, int khoangCachHang, int khoangCachCot);
        // khai báo 1 kiểu hàm delegate
        public GetData MyGetData;

        public FormXayDungLoKhoan1()
        {
            InitializeComponent();
            //set title
            labelFormTitle.Text = this.Text;
            //nhóm các controls
            radioButtonTemplate.Add(radioButtonTemp1);
            radioButtonTemplate.Add(radioButtonTemp2);
            radioButtonTemplate.Add(radioButtonTemp3);
            radioButtonTemplate.Add(radioButtonTemp4);
            textBoxSoHang.KeyPress += textBoxInput_KeyPress;
            textBoxSoCot.KeyPress += textBoxInput_KeyPress;
            textBoxKhoangCachHang.KeyPress += textBoxInput_KeyPress;
            textBoxKhoangCachCot.KeyPress += textBoxInput_KeyPress;
            //UI
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.BackColor = ColorTranslator.FromHtml("#e74c3c");
            buttonChonToaDo.FlatAppearance.BorderSize = 0;
            buttonChonToaDo.BackColor = ColorTranslator.FromHtml("#455A64");
            foreach (RadioButton rb in radioButtonTemplate)
            {
                rb.FlatAppearance.BorderSize = 5;
                rb.CheckedChanged += new System.EventHandler(radioButtonTempGroups_CheckedChanged);
            }
        }

        private void FormXayDungLoKhoan1_Load(object sender, EventArgs e)
        {
            radioButtonTemp1.Checked = true;
        }

        /// <summary>
        /// Hàm truyền dữ liệu về FormMain để vẽ lưới lỗ khoan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChonToaDo_Click(object sender, EventArgs e)
        {
            int soHang, soCot, khoangCachHang, khoangCachCot;
            soHang = int.Parse(textBoxSoHang.Text);
            soCot = int.Parse(textBoxSoCot.Text);
            khoangCachHang = int.Parse(textBoxKhoangCachHang.Text);
            khoangCachCot = int.Parse(textBoxKhoangCachCot.Text);
            if (soHang == 0 || soCot == 0 || khoangCachHang == 0 || khoangCachCot == 0)
            {
                MessageBox.Show("Nhap sai nhe :D");
            }
            else
            {
                switch (huongVe)
                {
                    case 1:
                        khoangCachHang = khoangCachHang * -1;
                        //khoangCachCot khong doi
                        break;
                    case 2:
                        //khoangCachHang khong doi
                        //khoangCachCot khong doi
                        break;
                    case 3:
                        khoangCachHang = khoangCachHang * -1;
                        khoangCachCot = khoangCachCot * -1;
                        break;
                    case 4:
                        //khoangCachHang khong doi
                        khoangCachCot = khoangCachCot * -1;
                        break;
                }
                MyGetData(soHang, soCot, khoangCachHang, khoangCachCot);
                this.Close();
            }
        }

        /// <summary>
        /// Hàm kiểm tra sự kiện nhập phím có phải phím số không
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonTempGroups_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                rb.ForeColor = ColorTranslator.FromHtml("#90A4AE");
            }
            else
            {
                rb.ForeColor = Color.WhiteSmoke;
            }
            switch (rb.Name)
            {
                case "radioButtonTemp1":
                    if (rb.Checked)
                    {
                        huongVe = 1;
                    }
                    break;
                case "radioButtonTemp2":
                    if (rb.Checked)
                    {
                        huongVe = 2;
                    }
                    break;
                case "radioButtonTemp3":
                    if (rb.Checked)
                    {
                        huongVe = 3;
                    }
                    break;
                case "radioButtonTemp4":
                    if (rb.Checked)
                    {
                        huongVe = 4;
                    }
                    break;                
            }
        }
    }
}