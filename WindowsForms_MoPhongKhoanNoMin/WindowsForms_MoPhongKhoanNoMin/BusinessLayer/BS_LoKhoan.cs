using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using WindowsForms_MoPhongKhoanNoMin.DataAccessLayer;

namespace WindowsForms_MoPhongKhoanNoMin.BusinessLayer
{
    class BS_LoKhoan
    {
        /// <summary>
        /// Hàm lấy danh sách lỗ khoan của một bản vẽ
        /// </summary>
        /// <param name="_id">ID bản vẽ</param>
        /// <returns>List<LoKhoan></returns>
        public static List<LoKhoan> DanhSachLoKhoan(String _id)
        {
            List<LoKhoan> danhSachLoKhoan = new List<LoKhoan>();
            Connection conn = new Connection();
            string sql = "SELECT * FROM LoKhoan WHERE MaBanVe = " + _id;
            SQLiteDataReader dr = conn.GetData(sql);
            while (dr.Read())
            {
                danhSachLoKhoan.Add(new LoKhoan(dr["MaHoChieu"].ToString(), dr["MaLK"].ToString(), dr["MaBanVe"].ToString(), dr["MaMayKhoan"].ToString(), dr.GetDouble(4), dr.GetDouble(5), dr["HuongKhoan"].ToString(), dr.GetDouble(7), dr.GetDouble(8)));
            }
            return danhSachLoKhoan;
        }
    }
}
