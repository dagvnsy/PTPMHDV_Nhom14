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
    public class HoaDonDAL
    {
        string chuoiKetNoi = @"Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public bool Them(HoaDonDTO hd)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HoaDon_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idHocVien", hd.IdHocVien);
                cmd.Parameters.AddWithValue("@idLopHoc", hd.IdLopHoc);
                cmd.Parameters.AddWithValue("@soTien", hd.SoTien);
                cmd.Parameters.AddWithValue("@trangThai", hd.TrangThai);
                cmd.Parameters.AddWithValue("@ghiChu", hd.GhiChu ?? "");

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Sua(HoaDonDTO hd)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HoaDon_Sua", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idHoaDon", hd.IdHoaDon);
                cmd.Parameters.AddWithValue("@soTien", hd.SoTien);
                cmd.Parameters.AddWithValue("@trangThai", hd.TrangThai);
                cmd.Parameters.AddWithValue("@ghiChu", hd.GhiChu ?? "");

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HoaDon_Xoa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHoaDon", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<HoaDonDTO> GetAll()
        {
            List<HoaDonDTO> ds = new List<HoaDonDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HoaDon_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ds.Add(new HoaDonDTO
                    {
                        IdHoaDon = (int)dr["idHoaDon"],
                        IdHocVien = (int)dr["idHocVien"],
                        TenHocVien = dr["tenHocVien"].ToString(),
                        IdLopHoc = (int)dr["idLopHoc"],
                        TenLopHoc = dr["tenLopHoc"].ToString(),
                        SoTien = (decimal)dr["soTien"],
                        NgayLap = (DateTime)dr["ngayLap"],
                        TrangThai = dr["trangThai"].ToString(),
                        GhiChu = dr["ghiChu"].ToString()
                    });
                }
            }
            return ds;
        }


        public List<HoaDonDTO> GetByHocVien(int idHocVien)
        {
            List<HoaDonDTO> ds = new List<HoaDonDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HoaDon_GetByHocVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ds.Add(new HoaDonDTO
                    {
                        IdHoaDon = (int)rd["idHoaDon"],
                        IdHocVien = (int)rd["idHocVien"],
                        IdLopHoc = (int)rd["idLopHoc"],
                        TenLopHoc = rd["tenLopHoc"].ToString(),
                        SoTien = (decimal)rd["soTien"],
                        NgayLap = (DateTime)rd["ngayLap"],
                        TrangThai = rd["trangThai"].ToString(),
                        GhiChu = rd["ghiChu"] == DBNull.Value ? "" : rd["ghiChu"].ToString()
                    });
                }
            }
            return ds;
        }
    }

}
