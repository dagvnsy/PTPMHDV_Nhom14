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
    public class KhoaHocDAL
    {
        private string chuoiKetNoi =
            "Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public List<KhoaHocDTO> GetAll()
        {
            List<KhoaHocDTO> ds = new List<KhoaHocDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_KhoaHoc_GetAll", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new KhoaHocDTO
                    {
                        IdKhoaHoc = (int)dr["idKhoaHoc"],
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        Level = dr["level"].ToString(),
                        SoBuoiHoc = (int)dr["soBuoiHoc"],
                        MoTa = dr["moTa"].ToString(),
                        LoaiTieng = dr["loaiTieng"].ToString(),
                    });
                }
            }
            return ds;
        }




        public bool Them(KhoaHocDTO kh)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_KhoaHoc_Them", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tenKhoaHoc", kh.TenKhoaHoc);
                cmd.Parameters.AddWithValue("@level", kh.Level);
                cmd.Parameters.AddWithValue("@soBuoiHoc", kh.SoBuoiHoc);
                cmd.Parameters.AddWithValue("@moTa", kh.MoTa);
                cmd.Parameters.AddWithValue("@loaiTieng", kh.LoaiTieng);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Sua(KhoaHocDTO kh)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_KhoaHoc_Sua", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKhoaHoc", kh.IdKhoaHoc);
                cmd.Parameters.AddWithValue("@tenKhoaHoc", kh.TenKhoaHoc);
                cmd.Parameters.AddWithValue("@level", kh.Level);
                cmd.Parameters.AddWithValue("@soBuoiHoc", kh.SoBuoiHoc);
                cmd.Parameters.AddWithValue("@moTa", kh.MoTa);
                cmd.Parameters.AddWithValue("@loaiTieng", kh.LoaiTieng);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_KhoaHoc_Xoa", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKhoaHoc", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public KhoaHocDTO GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_KhoaHoc_GetById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKhoaHoc", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new KhoaHocDTO
                    {
                        IdKhoaHoc = (int)dr["idKhoaHoc"],
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        Level = dr["level"].ToString(),
                        SoBuoiHoc = (int)dr["soBuoiHoc"],
                        MoTa = dr["moTa"].ToString(),
                        LoaiTieng = dr["loaiTieng"].ToString()
                    };
                }
            }
            return null;
        }


        public List<KhoaHocDTO> GetByLoaiTieng(string loaiTieng)
        {
            List<KhoaHocDTO> ds = new List<KhoaHocDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand("sp_KhoaHoc_GetByLoaiTieng", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@loaiTieng", loaiTieng);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new KhoaHocDTO
                    {
                        IdKhoaHoc = (int)dr["idKhoaHoc"],
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        Level = dr["level"].ToString(),
                        SoBuoiHoc = (int)dr["soBuoiHoc"],
                        MoTa = dr["moTa"].ToString(),
                        LoaiTieng = dr["loaiTieng"].ToString()
                    });
                }
            }
            return ds;
        }



        public List<KhoaHocDTO> TimKiem(string keyword)
        {
            List<KhoaHocDTO> ds = new List<KhoaHocDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KhoaHoc_TimKiem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tuKhoa", keyword);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ds.Add(new KhoaHocDTO
                    {
                        IdKhoaHoc = (int)rd["idKhoaHoc"],
                        TenKhoaHoc = rd["tenKhoaHoc"].ToString(),
                        Level = rd["level"].ToString(),
                        SoBuoiHoc = (int)rd["soBuoiHoc"],
                        MoTa = rd["moTa"].ToString(),
                        LoaiTieng = rd["loaiTieng"].ToString()
                    });
                }
            }
            return ds;
        }




    }

}
