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

using WindowsForms_MoPhongKhoanNoMin.BusinessLayer;

namespace WindowsForms_MoPhongKhoanNoMin.GUILayer
{
    public partial class FormStart : DevExpress.XtraEditors.XtraForm
    {
        private String templatePath;
        List<BanVe> danhSachBanVe = new List<BanVe>();
        List<HyperlinkLabelControl> duongDanBanVe = new List<HyperlinkLabelControl>();
        List<Label> ngayBanVe = new List<Label>();
        List<RadioButton> radioButtonTemplate = new List<RadioButton>();

        // khai báo 1 hàm delegate
        public delegate void GetData(String id, String path);
        // khai báo 1 kiểu hàm delegate
        public GetData MyGetData;

        public FormStart()
        {
            InitializeComponent();
            //set title
            labelFormTitle.Text = this.Text;
            //nhóm các controls
            duongDanBanVe.Add(banVe1);
            duongDanBanVe.Add(banVe2);
            duongDanBanVe.Add(banVe3);
            duongDanBanVe.Add(banVe4);
            duongDanBanVe.Add(banVe5);
            ngayBanVe.Add(labelNgay1);
            ngayBanVe.Add(labelNgay2);
            ngayBanVe.Add(labelNgay3);
            ngayBanVe.Add(labelNgay4);
            ngayBanVe.Add(labelNgay5);
            radioButtonTemplate.Add(radioButtonTemp1);
            radioButtonTemplate.Add(radioButtonTemp2);
            radioButtonTemplate.Add(radioButtonTemp3);
            radioButtonTemplate.Add(radioButtonTemp4);
            radioButtonTemplate.Add(radioButtonTemp5);
            radioButtonTemplate.Add(radioButtonTemp6);
            //UI
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.BackColor = ColorTranslator.FromHtml("#e74c3c");
            buttonTaoMoi.FlatAppearance.BorderSize = 0;
            buttonTaoMoi.BackColor = ColorTranslator.FromHtml("#455A64");
            foreach (RadioButton rb in radioButtonTemplate)
            {
                rb.FlatAppearance.BorderSize = 5;
                rb.CheckedChanged += new System.EventHandler(radioButtonTempGroups_CheckedChanged);
            }
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            radioButtonTemp1.Checked = true;
            danhSachBanVe = BS_BanVe.DanhSachBanVeGanNhat();
            int i = 0;
            foreach (HyperlinkLabelControl value in duongDanBanVe)
            {
                if (danhSachBanVe.Count > i)
                {
                    value.Text = danhSachBanVe[i].TenBanVe;
                    value.Click += new System.EventHandler(link_Click);
                    i++;
                }
            }
            i = 0;
            foreach (Label value in ngayBanVe)
            {
                if (danhSachBanVe.Count > i)
                {
                    value.Text = danhSachBanVe[i].NgayChinhSua;
                    i++;
                }
                this.Refresh();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTaoMoi_Click(object sender, EventArgs e)
        {
            MyGetData("-1", templatePath);
            this.Close();
        }

        private void link_Click(object sender, EventArgs e)
        {
            HyperlinkLabelControl linkBanVe = sender as HyperlinkLabelControl;
            switch (linkBanVe.Name)
            {
                case "banVe1":
                    MyGetData(danhSachBanVe[0].ID.ToString(), templatePath);
                    break;
                case "banVe2":
                    MyGetData(danhSachBanVe[1].ID.ToString(), templatePath);
                    break;
                case "banVe3":
                    MyGetData(danhSachBanVe[2].ID.ToString(), templatePath);
                    break;
                case "banVe4":
                    MyGetData(danhSachBanVe[3].ID.ToString(), templatePath);
                    break;
                case "banVe5":
                    MyGetData(danhSachBanVe[4].ID.ToString(), templatePath);
                    break;
                default:
                    break;
            }
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
                        templatePath = @"..\..\Template\Temp_1.lcd";
                    }
                    break;
                case "radioButtonTemp2":
                    if (rb.Checked)
                    {
                        templatePath = @"..\..\Template\Temp_2.lcd";
                    }
                    break;
                case "radioButtonTemp3":
                    if (rb.Checked)
                    {
                        templatePath = @"..\..\Template\Temp_3.lcd";
                    }
                    break;
                case "radioButtonTemp4":
                    if (rb.Checked)
                    {
                        templatePath = @"..\..\Template\Temp_4.lcd";
                    }
                    break;
                case "radioButtonTemp5":
                    if (rb.Checked)
                    {
                        templatePath = @"..\..\Template\Temp_5.lcd";
                    }
                    break;
                case "radioButtonTemp6":
                    if (rb.Checked)
                    {
                        templatePath = @"..\..\Template\Temp_6.lcd";
                    }
                    break;
            }           
        }       
    }
}