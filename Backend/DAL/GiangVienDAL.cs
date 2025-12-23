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
    public class GiangVienDAL
    {
        string chuoiKetNoi = @"Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public List<GiangVienDTO> GetAll()
        {
            List<GiangVienDTO> ds = new List<GiangVienDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_GiangVien_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ds.Add(Map(rd));
                }
            }
            return ds;
        }

        public GiangVienDTO GetById(int id)
        {
            GiangVienDTO gv = null;
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_GiangVien_GetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idGiangVien", id);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    gv = Map(rd);
                }
            }
            return gv;
        }

        public List<GiangVienDTO> TimKiem(string tuKhoa)
        {
            List<GiangVienDTO> ds = new List<GiangVienDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_GiangVien_TimKiem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tuKhoa", tuKhoa);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ds.Add(Map(rd));
                }
            }
            return ds;
        }

        public bool Them(GiangVienDTO gv)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_GiangVien_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tenGiangVien", gv.TenGiangVien);
                cmd.Parameters.AddWithValue("@gioiTinh", gv.GioiTinh);
                cmd.Parameters.AddWithValue("@trinhDo", gv.TrinhDo);
                cmd.Parameters.AddWithValue("@chuyenMon", gv.ChuyenMon);
                cmd.Parameters.AddWithValue("@email", gv.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", gv.SoDienThoai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Sua(GiangVienDTO gv)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_GiangVien_Sua", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idGiangVien", gv.IdGiangVien);
                cmd.Parameters.AddWithValue("@tenGiangVien", gv.TenGiangVien);
                cmd.Parameters.AddWithValue("@gioiTinh", gv.GioiTinh);
                cmd.Parameters.AddWithValue("@trinhDo", gv.TrinhDo);
                cmd.Parameters.AddWithValue("@chuyenMon", gv.ChuyenMon);
                cmd.Parameters.AddWithValue("@email", gv.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", gv.SoDienThoai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_GiangVien_Xoa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idGiangVien", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private GiangVienDTO Map(SqlDataReader rd)
        {
            return new GiangVienDTO
            {
                IdGiangVien = (int)rd["idGiangVien"],
                TenGiangVien = rd["tenGiangVien"].ToString(),
                GioiTinh = (bool)rd["gioiTinh"],
                TrinhDo = rd["trinhDo"].ToString(),
                ChuyenMon = rd["chuyenMon"].ToString(),
                Email = rd["email"].ToString(),
                SoDienThoai = rd["soDienThoai"].ToString()
            };
        }


        public GiangVienDTO GetByTaiKhoan(int idTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM GiangVien WHERE idTaiKhoan=@id", conn);
                cmd.Parameters.AddWithValue("@id", idTaiKhoan);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new GiangVienDTO
                    {
                        IdGiangVien = (int)dr["idGiangVien"],
                        TenGiangVien = dr["tenGiangVien"].ToString(),
                        Email = dr["email"].ToString()
                    };
                }
            }
            return null;
        }

    }
}
