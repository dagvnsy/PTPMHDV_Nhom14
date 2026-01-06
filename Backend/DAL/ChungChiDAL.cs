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
    public class ChungChiDAL
    {
        string chuoiKetNoi = @"Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public bool Them(int idHocVien, int idKhoaHoc, DateTime ngayCap, string xepLoai)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_ChungChi_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);
                cmd.Parameters.AddWithValue("@idKhoaHoc", idKhoaHoc);
                cmd.Parameters.AddWithValue("@ngayCap", ngayCap);
                cmd.Parameters.AddWithValue("@xepLoai", xepLoai);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool Xoa(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_ChungChi_Xoa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idChungChi", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<ChungChiDTO> GetAll()
        {
            List<ChungChiDTO> ds = new List<ChungChiDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_ChungChi_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new ChungChiDTO
                    {
                        IdChungChi = (int)dr["idChungChi"],
                        IdHocVien = (int)dr["idHocVien"],
                        IdKhoaHoc = (int)dr["idKhoaHoc"],
                        NgayCap = (DateTime)dr["ngayCap"],
                        XepLoai = dr["xepLoai"].ToString()
                    });
                }
            }
            return ds;
        }

        public List<ChungChiDTO> GetByHocVien(int idHocVien)
        {
            List<ChungChiDTO> ds = new List<ChungChiDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_ChungChi_GetByHocVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new ChungChiDTO
                    {
                        IdChungChi = (int)dr["idChungChi"],
                        IdHocVien = idHocVien,
                        IdKhoaHoc = (int)dr["idKhoaHoc"],
                        NgayCap = (DateTime)dr["ngayCap"],
                        XepLoai = dr["xepLoai"].ToString()
                    });
                }
            }
            return ds;
        }

        public ChungChiPDFDTO LayThongTinInPDF(int idChungChi)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_ChungChi_InPDF", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idChungChi", idChungChi);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new ChungChiPDFDTO
                    {
                        TenHocVien = dr["tenHocVien"].ToString(),
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        NgayCap = (DateTime)dr["ngayCap"],
                        XepLoai = dr["xepLoai"].ToString()
                    };
                }
            }
            return null;
        }

        public List<ChungChiDTO> GetByKhoaHoc(int idKhoaHoc)
        {
            List<ChungChiDTO> ds = new List<ChungChiDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_CapChungChi_GetByKhoaHoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKhoaHoc", idKhoaHoc);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ds.Add(new ChungChiDTO
                    {
                        IdHocVien = (int)rd["idHocVien"],
                        TenHocVien = rd["tenHocVien"].ToString(),
                        Diem = (decimal)rd["diem"],
                        XepLoai = rd["xepLoai"].ToString()
                    });
                }
            }
            return ds;
        }

        public ChungChiDTO GetByHocVienVaKhoaHoc(int idHocVien, int idKhoaHoc)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_ChungChi_Check", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);
                cmd.Parameters.AddWithValue("@idKhoaHoc", idKhoaHoc);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new ChungChiDTO
                    {
                        IdChungChi = (int)dr["idChungChi"]
                    };
                }
            }
            return null;

        }

    }


    

}
