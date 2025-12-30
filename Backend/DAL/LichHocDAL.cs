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
    public class LichHocDAL
    {
        string chuoiKetNoi = @"Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public bool Them(LichHocDTO lh)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LichHoc_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLopHoc", lh.IdLopHoc);
                cmd.Parameters.AddWithValue("@ngayHoc", lh.NgayHoc);
                cmd.Parameters.AddWithValue("@caHoc", lh.CaHoc);
                cmd.Parameters.AddWithValue("@phongHoc", lh.PhongHoc);
                cmd.Parameters.AddWithValue("@ghiChu", lh.GhiChu);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Sua(LichHocDTO lh)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LichHoc_Sua", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLichHoc", lh.IdLichHoc);
                cmd.Parameters.AddWithValue("@ngayHoc", lh.NgayHoc);
                cmd.Parameters.AddWithValue("@caHoc", lh.CaHoc);
                cmd.Parameters.AddWithValue("@phongHoc", lh.PhongHoc);
                cmd.Parameters.AddWithValue("@ghiChu", lh.GhiChu);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LichHoc_Xoa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLichHoc", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<LichHocDTO> GetAllFull()
        {
            List<LichHocDTO> ds = new List<LichHocDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LichHoc_GetAllFull", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new LichHocDTO
                    {
                        IdLichHoc = (int)dr["idLichHoc"],
                        IdLopHoc = (int)dr["idLopHoc"],
                        NgayHoc = (DateTime)dr["ngayHoc"],
                        CaHoc = dr["CaHoc"].ToString(),
                        PhongHoc = dr["PhongHoc"].ToString(),
                        GhiChu = dr["ghiChu"].ToString(),
                        TenLopHoc = dr["tenLopHoc"].ToString(),
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        TenGiangVien = dr["tenGiangVien"].ToString()
                    });
                }
            }
            return ds;
        }


        public LichHocDTO GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LichHoc_GetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLichHoc", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    return Map(dr);
            }
            return null;
        }

        public List<LichHocDTO> GetByLopHoc(int idLopHoc)
        {
            List<LichHocDTO> ds = new List<LichHocDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LichHoc_GetByLopHoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLopHoc", idLopHoc);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(Map(dr));
                }
            }
            return ds;
        }

        private LichHocDTO Map(SqlDataReader dr)
        {
            return new LichHocDTO
            {
                IdLichHoc = (int)dr["idLichHoc"],
                IdLopHoc = (int)dr["idLopHoc"],
                NgayHoc = (DateTime)dr["ngayHoc"],
                CaHoc = dr["CaHoc"].ToString(),
                PhongHoc = dr["PhongHoc"].ToString(),
                GhiChu = dr["ghiChu"].ToString()
            };
        }
    }

}
