using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using WindowsForms_MoPhongKhoanNoMin.DataAccessLayer;

namespace WindowsForms_MoPhongKhoanNoMin.BusinessLayer
{
    class BS_BanVe
    {
        public static List<BanVe> DanhSachBanVeGanNhat()
        {
            List<BanVe> danhSachBanVe = new List<BanVe>();
            String sql = "SELECT * FROM BanVe ORDER BY NgayChinhSua DESC LIMIT 5";
            Connection conn = new Connection();
            SQLiteDataReader dr = conn.GetData(sql);
            while (dr.Read())
            {
                danhSachBanVe.Add(new BanVe(dr["ID"].ToString(), dr["TenBanVe"].ToString(), dr.GetString(2)));
            }
            return danhSachBanVe;
        }
    }
}
