using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TrungTamNgoaiNgu.DTO;
using System.Data;

namespace TrungTamNgoaiNgu.DAL
{
    public class HocVienDAL
    {
        private string chuoiKetNoi =
            "Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        private HocVienDTO DocHocVien(SqlDataReader rd)
        {
            return new HocVienDTO
            {
                IdHocVien = (int)rd["idHocVien"],
                TenHocVien = rd["tenHocVien"].ToString(),
                NgaySinh = (DateTime)rd["ngaySinh"],
                GioiTinh = (bool)rd["gioiTinh"],
                Email = rd["email"].ToString(),
                SoDienThoai = rd["soDienThoai"].ToString(),
                DiaChi = rd["diaChi"].ToString(),
                TrangThai = rd["trangThai"].ToString(),
                IdTaiKhoan = (int)rd["idTaiKhoan"]
            };
        }

        public List<HocVienDTO> XemTatCa()
        {
            var danhSach = new List<HocVienDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HocVien_XemTatCa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                    danhSach.Add(DocHocVien(rd));
            }
            return danhSach;
        }

        public HocVienDTO LayTheoId(int idHocVien)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HocVien_LayTheoId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);
                conn.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                    return DocHocVien(rd);
            }
            return null;
        }

        public bool Them(HocVienDTO hv)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HocVien_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tenHocVien", hv.TenHocVien);
                cmd.Parameters.AddWithValue("@ngaySinh", hv.NgaySinh);
                cmd.Parameters.AddWithValue("@gioiTinh", hv.GioiTinh);
                cmd.Parameters.AddWithValue("@email", hv.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", hv.SoDienThoai);
                cmd.Parameters.AddWithValue("@diaChi", hv.DiaChi);
                cmd.Parameters.AddWithValue("@trangThai", hv.TrangThai);
                cmd.Parameters.AddWithValue("@idTaiKhoan", hv.IdTaiKhoan);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Sua(HocVienDTO hv)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_HocVien_Sua", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idHocVien", hv.IdHocVien);
                cmd.Parameters.AddWithValue("@tenHocVien", hv.TenHocVien);
                cmd.Parameters.AddWithValue("@ngaySinh", hv.NgaySinh);
                cmd.Parameters.AddWithValue("@gioiTinh", hv.GioiTinh);
                cmd.Parameters.AddWithValue("@email", hv.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", hv.SoDienThoai);
                cmd.Parameters.AddWithValue("@diaChi", hv.DiaChi);
                cmd.Parameters.AddWithValue("@trangThai", hv.TrangThai);

                conn.Open();
                int kq = cmd.ExecuteNonQuery();

                if (kq == 0)
                    throw new Exception("Không có bản ghi nào được cập nhật. Kiểm tra idHocVien.");

                return true;
            }
        }


        public bool Xoa(int idHocVien)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_HocVien_Xoa", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);

                conn.Open();
                int kq = cmd.ExecuteNonQuery();

                if (kq == 0)
                    throw new Exception("Không tìm thấy học viên để xóa");

                return true;
            }
        }


        public List<HocVienDTO> TimKiem(string tuKhoa)
        {
            var danhSach = new List<HocVienDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HocVien_TimKiem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tuKhoa", tuKhoa);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                    danhSach.Add(DocHocVien(rd));
            }
            return danhSach;
        }


        public HocVienDTO GetByTaiKhoan(int idTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT 
                idHocVien,
                tenHocVien,
                ngaySinh,
                gioiTinh,
                email,
                soDienThoai,
                diaChi,
                idTaiKhoan
            FROM HocVien
            WHERE idTaiKhoan = @id", conn);

                cmd.Parameters.AddWithValue("@id", idTaiKhoan);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new HocVienDTO
                    {
                        IdHocVien = (int)dr["idHocVien"],
                        TenHocVien = dr["tenHocVien"].ToString(),
                        NgaySinh = dr["ngaySinh"] == DBNull.Value
                                    ? (DateTime?)null
                                    : (DateTime)dr["ngaySinh"],
                        GioiTinh = (bool)dr["gioiTinh"],
                        Email = dr["email"].ToString(),
                        SoDienThoai = dr["soDienThoai"].ToString(),
                        DiaChi = dr["diaChi"].ToString(),
                        IdTaiKhoan = (int)dr["idTaiKhoan"]
                    };
                }
            }
            return null;
        }


    }
}
