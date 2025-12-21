using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DTO.TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.DAL
{
    public class TaiKhoanDAL
    {
        string chuoiKetNoi = @"Data Source=.;Initial Catalog=QLTTNN;Integrated Security=True";

        public bool Them(TaiKhoanDTO tk)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tenDangNhap", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@matKhau", tk.MatKhau);
                cmd.Parameters.AddWithValue("@loaiNguoiDung", tk.LoaiNguoiDung);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DoiMatKhau(int idTaiKhoan, string matKhauMoi)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_DoiMatKhau", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTaiKhoan", idTaiKhoan);
                cmd.Parameters.AddWithValue("@matKhauMoi", matKhauMoi);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CapNhatTrangThai(int idTaiKhoan, bool trangThai)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_CapNhatTrangThai", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTaiKhoan", idTaiKhoan);
                cmd.Parameters.AddWithValue("@trangThai", trangThai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public TaiKhoanDTO DangNhap(string tenDangNhap, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_DangNhap", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@matKhau", matKhau);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new TaiKhoanDTO
                    {
                        IdTaiKhoan = (int)rd["idTaiKhoan"],
                        TenDangNhap = rd["tenDangNhap"].ToString(),
                        LoaiNguoiDung = rd["loaiNguoiDung"].ToString()
                    };
                }
                return null;
            }
        }
    }
}
