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
    public class BaoCaoDAL
    {
        string chuoiKetNoi = @"Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public List<BaoCaoSoLopDTO> SoLopTheoKhoaHoc()
        {
            List<BaoCaoSoLopDTO> ds = new List<BaoCaoSoLopDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCao_SoLopTheoKhoaHoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new BaoCaoSoLopDTO
                    {
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        SoLop = (int)dr["soLop"]
                    });
                }
            }
            return ds;
        }

        public List<BaoCaoHocVienDTO> HocVienTheoLop(int idLopHoc)
        {
            List<BaoCaoHocVienDTO> ds = new List<BaoCaoHocVienDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCao_HocVienTheoLop", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLopHoc", idLopHoc);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new BaoCaoHocVienDTO
                    {
                        IdHocVien = (int)dr["idHocVien"],
                        TenHocVien = dr["tenHocVien"].ToString(),
                        Email = dr["email"].ToString(),
                        SoDienThoai = dr["soDienThoai"].ToString()
                    });
                }
            }
            return ds;
        }

        public List<BaoCaoKetQuaThiDTO> KetQuaThi(int idKyThi)
        {
            List<BaoCaoKetQuaThiDTO> ds = new List<BaoCaoKetQuaThiDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCao_KetQuaThi", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKyThi", idKyThi);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new BaoCaoKetQuaThiDTO
                    {
                        TenHocVien = dr["tenHocVien"].ToString(),
                        Diem = (decimal)dr["diem"],
                        KetQua = dr["ketQua"].ToString()
                    });
                }
            }
            return ds;
        }

        
    }

}
