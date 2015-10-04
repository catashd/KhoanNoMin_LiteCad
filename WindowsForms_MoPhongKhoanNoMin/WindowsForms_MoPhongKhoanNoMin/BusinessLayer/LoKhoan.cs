using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_MoPhongKhoanNoMin.BusinessLayer
{
    class LoKhoan
    {
        private String maHoChieu;
        private String maLoKhoan;
        private String maBanVe;
        private String maMayKhoan;
        private double banKinh;
        private double chieuSau;
        private String huongKhoan;
        private double toaDoX;
        private double toaDoY;

        public String MaHoChieu { get { return maHoChieu; } set { maHoChieu = value; } }
        public String MaLoKhoan { get { return maLoKhoan; } set { maLoKhoan = value; } }
        public String MaBanVe { get { return maBanVe; } set { maBanVe = value; } }
        public String MaMayKhoan { get { return maMayKhoan; } set { maMayKhoan = value; } }
        public double BanKinh { get { return banKinh; } set { banKinh = value; } }
        public double ChieuSau { get { return chieuSau; } set { chieuSau = value; } }
        public String HuongKhoan { get { return huongKhoan; } set { huongKhoan = value; } }
        public double ToaDoX { get { return toaDoX; } set { toaDoX = value; } }
        public double ToaDoY { get { return toaDoY; } set { toaDoY = value; } }

        public LoKhoan()
        {
            this.maHoChieu = null;
            this.maLoKhoan = null;
            this.maBanVe = null;
            this.maMayKhoan = null;
            this.banKinh = -1;
            this.chieuSau = -1;
            this.huongKhoan = null;
            this.toaDoX = 0;
            this.toaDoY = 0;
        }

        public LoKhoan(String _maHoChieu, String _maLoKhoan, String _maBanVe, String _maMayKhoan, double _banKinh, double _chieuSau, String _huongKhoan, double _toaDoX, double _toaDoY)
        {
            this.maHoChieu = _maHoChieu;
            this.maLoKhoan = _maLoKhoan;
            this.maBanVe = _maBanVe;
            this.maMayKhoan = _maMayKhoan;
            this.banKinh = _banKinh;
            this.chieuSau = _chieuSau;
            this.huongKhoan = _huongKhoan;
            this.toaDoX = _toaDoX;
            this.toaDoY = _toaDoY;
        }
    }
}
