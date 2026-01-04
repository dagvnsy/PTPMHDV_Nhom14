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
    public class KetQuaThiDAL
    {
        string chuoiKetNoi =
            @"Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public void ThemKetQua(int idKyThi, int idHocVien, decimal diem)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KetQuaThi_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idKyThi", idKyThi);
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);
                cmd.Parameters.AddWithValue("@diem", diem);
                cmd.Parameters.AddWithValue("@ketQua", diem >= 5 ? "ĐẠT" : "KHÔNG ĐẠT");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<KetQuaThiDTO> GetByLopHoc(int idKyThi)
        {
            List<KetQuaThiDTO> list = new List<KetQuaThiDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KetQuaThi_GetByLopHoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKyThi", idKyThi);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    list.Add(new KetQuaThiDTO
                    {
                        IdHocVien = (int)rd["IdHocVien"],
                        TenHocVien = rd["TenHocVien"].ToString(),
                        Diem = rd["Diem"] == DBNull.Value ? null : (decimal?)rd["Diem"],
                        KetQua = rd["KetQua"].ToString()
                    });
                }
            }
            return list;
        }

        public bool Luu(KetQuaThiDTO dto)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KetQuaThi_Luu", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idKyThi", dto.IdKyThi);
                cmd.Parameters.AddWithValue("@idHocVien", dto.IdHocVien);
                cmd.Parameters.AddWithValue("@diem", dto.Diem);
                cmd.Parameters.AddWithValue("@ketQua", dto.KetQua);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<KetQuaThiDTO> GetByKyThi(int idKyThi)
        {
            List<KetQuaThiDTO> ds = new List<KetQuaThiDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KetQuaThi_GetByKyThi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKyThi", idKyThi);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ds.Add(new KetQuaThiDTO
                    {
                        IdKyThi = (int)rd["idKyThi"],
                        IdHocVien = (int)rd["idHocVien"],
                        Diem = (decimal)rd["diem"],
                        KetQua = rd["ketQua"].ToString(),
                        TenHocVien = rd["tenHocVien"].ToString()
                    });
                }
            }
            return ds;
        }


        public bool Sua(KetQuaThiDTO kq)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KetQuaThi_Sua", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idKyThi", kq.IdKyThi);
                cmd.Parameters.AddWithValue("@idHocVien", kq.IdHocVien);
                cmd.Parameters.AddWithValue("@diem", kq.Diem);
                cmd.Parameters.AddWithValue("@ketQua", kq.KetQua);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }





    }
}
