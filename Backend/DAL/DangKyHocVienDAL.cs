using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.DAL
{
    public class DangKyHocVienDAL
    {
        string chuoiKetNoi = @"Data Source=.;Initial Catalog=QLTTNN;Integrated Security=True";

        public bool DangKy(DangKyHocVienDTO dto)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HocVien_DangKy", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tenDangNhap", dto.TenDangNhap);
                cmd.Parameters.AddWithValue("@matKhau", dto.MatKhau);
                cmd.Parameters.AddWithValue("@tenHocVien", dto.TenHocVien);
                cmd.Parameters.AddWithValue("@ngaySinh", dto.NgaySinh);
                cmd.Parameters.AddWithValue("@gioiTinh", dto.GioiTinh);
                cmd.Parameters.AddWithValue("@email", dto.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", dto.SoDienThoai);
                cmd.Parameters.AddWithValue("@diaChi", dto.DiaChi);

                conn.Open();
                cmd.ExecuteNonQuery(); 
                return true;
            }
        }

    }
}
