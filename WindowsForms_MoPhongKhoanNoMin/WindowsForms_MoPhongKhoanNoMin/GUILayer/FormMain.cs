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
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        //biến LiteCAD
        private int hWnd;   //biến hiển thị giao diện - Windows        
        private int hCmd;   //biến hiển thị giao diện - Command
        private int hProp;  //biến hiển thị giao diện - Properties
        private int hDrw;   //biến lưu bản vẽ hiện tại
        //
        private ManageCircle mgCircles;
        private int nameOfBlock;
        //biến lưu sự kiện mouse click
        F_MOUSEDBLCLK _EventMouseDbclick_LoMin;
        F_MOUSEDBLCLK _EventMouseDbclick_XayDungLuoiLoMin;
        //dữ liệu để dựng lưới lỗ khoan
        private int soHang, soCot, khoangCachHang, khoangCachCot;

        public FormMain()
        {
            InitializeComponent();            
            Lcad.PropPutStr(0, Lcad.LC_PROP_G_REGCODE, "1234"); // 1234 là mã số khi mua LiteCad đã đăng ký 
            //Lcad.PropPutStr(0, Lcad.LC_PROP_G_DIRDATA, @"..\..\LiteCAD\Data");
            //Lcad.PropPutStr(0, Lcad.LC_PROP_G_DIRPLUG, @"..\..\LiteCAD\Data\Plugins");
            //Lcad.PropPutStr(0, Lcad.LC_PROP_G_DIRCFG, @"..\..\LiteCAD");
            Lcad.PropPutBool(0, Lcad.LC_PROP_G_DLGRECENT, true);
            Lcad.PropPutBool(0, Lcad.LC_PROP_G_ADDRECENT, true);
            //gán các sự kiện mouse click với các hàm
            //_EventMouseDbclick_LoMin = new F_MOUSEDBLCLK(MouseDblClkProc_LoMin);
            _EventMouseDbclick_XayDungLuoiLoMin = new F_MOUSEDBLCLK(MouseDblClkProc_XayDungLuoiLoMin);
            Lcad.Initialize();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {           
            //tạo cửa sổ - Windows
            hWnd = Lcad.CreateWindow(panelControl_hWnd.Handle, Lcad.LC_WS_VIEWTABS, 0, 0, panelControl_hWnd.Width, panelControl_hWnd.Height);
            //tạo cửa sổ - Command
            hCmd = Lcad.CreateCmdwin(panelControl_hCmd.Handle, 0, 0, panelControl_hCmd.Width, panelControl_hCmd.Height);
            //tạo cửa sổ - Properties
            hProp = Lcad.CreatePropwin(splitContainerControl.Panel1.Handle, 0, 0, splitContainerControl.Panel1.Width, splitContainerControl.Panel1.Height);
            //liên kết các cửa sổ với nhau
            Lcad.WndSetCmdwin(hWnd, hCmd);           
            Lcad.WndSetPropwin(hWnd, hProp);
            //tạo bản vẽ, gán cho cửa sổ LiteCad + đặt tên
            hDrw = Lcad.CreateDrawing();
            Lcad.DrwNew(hDrw, "", hWnd);
            Lcad.WndSetFocus(hWnd);
            //lấy giá trị hBlockModel của bản vẽ
            int hBlockModel = Lcad.PropGetHandle(hDrw, Lcad.LC_PROP_DRW_BLOCK_MODEL);
            //điều chỉnh kích thước
            Lcad.CmdwinResize(hCmd, 0, 0, panelControl_hCmd.Width, panelControl_hCmd.Height);
            Lcad.PropwinResize(hProp, 0, 0, splitContainerControl.Panel1.Width, splitContainerControl.Panel1.Height);
            Lcad.WndResize(hWnd, 0, 0, panelControl_hWnd.Width, panelControl_hWnd.Height);
            //vẽ lại hình
            Lcad.WndExeCommand(hWnd, Lcad.LC_CMD_REGEN, 0);
            Lcad.DrwRegenViews(hDrw, 0);
            Lcad.Initialize();           
        }
        
        //aka FormMain_Loaded :D    
        private void FormMain_Shown(object sender, EventArgs e)
        {
            //load form bắt đầu
            FormStart st = new FormStart();
            st.MyGetData = new FormStart.GetData(LoadFile_FormStart);
            st.Show();
            st.Activate();
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            Lcad.CmdwinResize(hCmd, 0, 0, panelControl_hCmd.Width, panelControl_hCmd.Height);
            Lcad.PropwinResize(hProp, 0, 0, splitContainerControl.Panel1.Width, splitContainerControl.Panel1.Height);
            Lcad.WndResize(hWnd, 0, 0, panelControl_hWnd.Width, panelControl_hWnd.Height);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormXayDungLoKhoan1 bh = new FormXayDungLoKhoan1();
            bh.MyGetData = new FormXayDungLoKhoan1.GetData(loadFile_FormXayDungLoKhoan1);
            bh.Show();
        }
        private void loadFile_FormXayDungLoKhoan1(int _soHang, int _soCot, int _khoangCachHang, int _khoangCachCot)
        {
            this.soHang = _soHang;
            this.soCot = _soCot;
            this.khoangCachHang = _khoangCachHang;
            this.khoangCachCot = _khoangCachCot;
            Lcad.OnEventMouseDblClk(_EventMouseDbclick_XayDungLuoiLoMin);
        }

        private void barButtonPage1_1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }
        private void LoadFile_FormStart(String id, String path)
        {
            if (id.Equals("-1"))
            {
                mgCircles = new ManageCircle();
                this.Text = path;
                this.nameOfBlock = 1;
                Lcad.DrwLoad(hDrw, path, this.Handle, hWnd);
                Lcad.DrwRegenViews(hDrw, 0);
                Lcad.WndExeCommand(hWnd, Lcad.LC_CMD_ZOOM_EXT, 0);
            }
            else
            {
                //lấy danh sách lỗ khoan
                List<LoKhoan> danhSachLoKhoan = BS_LoKhoan.DanhSachLoKhoan(id);                
                this.Text = path;
                this.nameOfBlock = 1;
                Lcad.DrwLoad(hDrw, path, this.Handle, hWnd);
                int hBlockModel = Lcad.PropGetHandle(hDrw, Lcad.LC_PROP_DRW_BLOCK_MODEL);
                foreach (LoKhoan value in danhSachLoKhoan)
                {
                    int maLK = int.Parse(value.MaLoKhoan);
                    int x = int.Parse(value.ToaDoX.ToString());
                    int y = int.Parse(value.ToaDoY.ToString());
                    double r = value.BanKinh;
                    int hEntCircle = Lcad.BlockAddCircle(hBlockModel, x, y, r, false);
                    Lcad.PropPutInt(hEntCircle, Lcad.LC_PROP_ENT_ID, maLK);
                }
                Lcad.DrwRegenViews(hDrw, 0);
                Lcad.WndExeCommand(hWnd, Lcad.LC_CMD_ZOOM_EXT, 0);
                //getCircleFromAutocadFile();
            }
        }

        private void MouseDblClkProc_XayDungLuoiLoMin(int hWnd, int Button, int Flags, int Xwin, int Ywin, double Xdrw, double Ydrw)
        {
            if (mgCircles == null)
            {
                mgCircles = new ManageCircle();
            }
            int hModelBlock = Lcad.PropGetHandle(hDrw, Lcad.LC_PROP_DRW_BLOCK_MODEL);
            double r = 0.5;
            int hEntCircle = Lcad.BlockAddCircle(hModelBlock, Xdrw, Ydrw, r, false);
            Lcad.BlockUnselect(hModelBlock);
            // Add a column of new Circles
            List<Circle> listCotDau = new List<Circle>();
            Circle root = new Circle(hEntCircle);
            listCotDau.Add(root);
            //Them hang
            //khoangCachHang = khoangCachHang * -1;
            for (int i = 0; i < soHang - 1; i++)
            {
                int newHEnt = Lcad.BlockAddClone(hModelBlock, listCotDau[i].GetHandle());
                Lcad.BlockSelectEnt(hModelBlock, newHEnt, true);
                Lcad.BlockSelMove(hModelBlock, 0, khoangCachHang, false, true);
                Lcad.BlockUnselect(hModelBlock);
                listCotDau.Add(new Circle(newHEnt));
            }
            mgCircles.AddListCircle(listCotDau);
            //Them cot           
            for (int i = 0; i < soCot - 1; i++)
            {
                List<Circle> listCotGanNhat = new List<Circle>();
                List<Circle> listCotMoi = new List<Circle>();
                if (khoangCachCot > 0)
                {
                    listCotGanNhat = mgCircles.GetListCirleOfLastColumn();
                }
                else
                {
                    listCotGanNhat = mgCircles.GetListCirleOfFirstColumn();
                }
                foreach (var circle in listCotGanNhat)
                {
                    int newHEnt = Lcad.BlockAddClone(hModelBlock, circle.GetHandle());
                    Lcad.BlockSelectEnt(hModelBlock, newHEnt, true);
                    Lcad.BlockSelMove(hModelBlock, khoangCachCot, 0, false, true);
                    Lcad.BlockUnselect(hModelBlock);
                    listCotMoi.Add(new Circle(newHEnt));
                }
                mgCircles.AddListCircle(listCotMoi);
            }
            MessageBox.Show("hang" + soHang + "\ncot" + soCot + "\nkc hang" + khoangCachHang + "\nkc cot" + khoangCachCot + "\nX" + Xdrw + "\nY" + Ydrw);
            Lcad.DrwRegenViews(hDrw, 0);
            Lcad.WndExeCommand(hWnd, Lcad.LC_CMD_ZOOM_EXT, 0);
            Lcad.OnEventMouseDblClk(_EventMouseDbclick_LoMin);
        }
    }
}