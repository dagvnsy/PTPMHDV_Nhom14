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
    public class DangKyHocDAL
    {
        string chuoiKetNoi = @"Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public bool Them(DangKyHocDTO dk)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_DangKyHoc_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", dk.IdHocVien);
                cmd.Parameters.AddWithValue("@idLopHoc", dk.IdLopHoc);
                cmd.Parameters.AddWithValue("@trangThai", dk.TrangThai);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(int idDangKy)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_DangKyHoc_Xoa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDangKy", idDangKy);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<DangKyHocDTO> GetAll()
        {
            List<DangKyHocDTO> ds = new List<DangKyHocDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_DangKyHoc_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(Map(dr));
                }
            }
            return ds;
        }

        public DangKyHocDTO GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_DangKyHoc_GetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDangKy", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    return Map(dr);
            }
            return null;
        }

        private DangKyHocDTO Map(SqlDataReader dr)
        {
            return new DangKyHocDTO
            {
                IdDangKy = (int)dr["idDangKy"],
                IdHocVien = (int)dr["idHocVien"],
                IdLopHoc = (int)dr["idLopHoc"],
                NgayDangKy = (DateTime)dr["ngayDangKy"],
                TrangThai = dr["trangThai"].ToString()
            };
        }


        //k cho đăng kí khóa mình đã đăng kí
        public bool DaDangKy(int idHocVien, int idLopHoc)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT COUNT(*) FROM DangKyHoc 
              WHERE idHocVien=@hv AND idLopHoc=@lop", conn);
                cmd.Parameters.AddWithValue("@hv", idHocVien);
                cmd.Parameters.AddWithValue("@lop", idLopHoc);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }

}
