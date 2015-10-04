using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_MoPhongKhoanNoMin.BusinessLayer
{
    class BanVe
    {
        private String iD;
        private String tenbanve;
        private String ngayChinhSua;

        public String ID { get { return iD; } set { iD = value; } }
        public String TenBanVe { get { return tenbanve; } set { tenbanve = value; } }
        public String NgayChinhSua { get { return ngayChinhSua; } set { ngayChinhSua = value; } }

        public BanVe()
        {
            this.iD = null;
            this.tenbanve = null;
            this.ngayChinhSua = null;
        }

        public BanVe(String ID, String TenBanVe, String NgayChinhSua)
        {
            this.iD = ID;
            this.tenbanve = TenBanVe;
            this.ngayChinhSua = NgayChinhSua;
        }
    }
}
