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
    public class LopHocDAL
    {
        private string chuoiKetNoi =
            "Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public List<LopHocDTO> GetAll()
        {
            List<LopHocDTO> ds = new List<LopHocDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_GetAll", conn);
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

        public LopHocDTO GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_GetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLopHoc", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    return Map(dr);
            }
            return null;
        }

        public bool Them(LopHocDTO lop)
        {
            return Execute("sp_LopHoc_Them", lop, false);
        }

        public bool Sua(LopHocDTO lop)
        {
            return Execute("sp_LopHoc_Sua", lop, true);
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_Xoa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLopHoc", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<LopHocDTO> TimKiem(string tuKhoa)
        {
            List<LopHocDTO> ds = new List<LopHocDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_TimKiem", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tuKhoa", tuKhoa);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(Map(dr));
                }
            }
            return ds;
        }

        private bool Execute(string sp, LopHocDTO lop, bool isUpdate)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand(sp, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (isUpdate)
                    cmd.Parameters.AddWithValue("@idLopHoc", lop.IdLopHoc);

                cmd.Parameters.AddWithValue("@idKhoaHoc", lop.IdKhoaHoc);
                cmd.Parameters.AddWithValue("@tenLopHoc", lop.TenLopHoc);
                cmd.Parameters.AddWithValue("@idGiangVien", lop.IdGiangVien);
                cmd.Parameters.AddWithValue("@hocPhi", lop.HocPhi);
                cmd.Parameters.AddWithValue("@ngayKhaiGiang", lop.NgayKhaiGiang);
                cmd.Parameters.AddWithValue("@ngayKetThuc", lop.NgayKetThuc);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private LopHocDTO Map(SqlDataReader dr)
        {
            return new LopHocDTO
            {
                IdLopHoc = (int)dr["idLopHoc"],
                IdKhoaHoc = (int)dr["idKhoaHoc"],
                TenLopHoc = dr["tenLopHoc"].ToString(),
                IdGiangVien = (int)dr["idGiangVien"],
                HocPhi = (decimal)dr["hocPhi"],
                NgayKhaiGiang = (DateTime)dr["ngayKhaiGiang"],
                NgayKetThuc = (DateTime)dr["ngayKetThuc"]
            };
        }

        //k cho xóa lơp khi lớp còn hv
        public int DemHocVienTrongLop(int idLopHoc)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM DangKyHoc WHERE idLopHoc=@id", conn);
                cmd.Parameters.AddWithValue("@id", idLopHoc);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }


        public List<LopHocDTO> GetByHocVien(int idHocVien)
        {
            List<LopHocDTO> ds = new List<LopHocDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_GetByHocVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new LopHocDTO
                    {
                        IdLopHoc = (int)dr["idLopHoc"],
                        TenLopHoc = dr["tenLopHoc"].ToString(),
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        TenGiangVien = dr["tenGiangVien"].ToString(),
                        NgayKhaiGiang = (DateTime)dr["ngayKhaiGiang"],
                        NgayKetThuc = (DateTime)dr["ngayKetThuc"]
                    });
                }
            }
            return ds;
        }

        public List<LopHocDTO> GetByKhoaHoc(int idKhoaHoc)
        {
            List<LopHocDTO> ds = new List<LopHocDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_GetByKhoaHoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKhoaHoc", idKhoaHoc);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new LopHocDTO
                    {
                        IdLopHoc = (int)dr["idLopHoc"],
                        TenLopHoc = dr["tenLopHoc"].ToString(),
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        TenGiangVien = dr["tenGiangVien"].ToString(),
                        HocPhi = (decimal)dr["hocPhi"],
                        NgayKhaiGiang = (DateTime)dr["ngayKhaiGiang"],
                        NgayKetThuc = (DateTime)dr["ngayKetThuc"]
                    });
                }
            }
            return ds;
        }
        public List<LopHocDTO> GetByGiangVien(int idGiangVien)
        {
            List<LopHocDTO> ds = new List<LopHocDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_GetByGiangVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idGiangVien", idGiangVien);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new LopHocDTO
                    {
                        IdLopHoc = (int)dr["idLopHoc"],
                        TenLopHoc = dr["tenLopHoc"].ToString(),
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        NgayKhaiGiang = (DateTime)dr["ngayKhaiGiang"],
                        NgayKetThuc = (DateTime)dr["ngayKetThuc"]
                    });
                }
            }
            return ds;
        }
        public List<HocVienDTO> GetHocVienByLop(int idLopHoc)
        {
            List<HocVienDTO> ds = new List<HocVienDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_HocVien_GetByLopHoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLopHoc", idLopHoc);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new HocVienDTO
                    {
                        TenHocVien = dr["tenHocVien"].ToString(),
                        NgaySinh = dr["ngaySinh"] == DBNull.Value ? (DateTime?)null : (DateTime)dr["ngaySinh"],
                        GioiTinh = (bool)dr["gioiTinh"],
                        Email = dr["email"].ToString(),
                        SoDienThoai = dr["soDienThoai"].ToString(),
                        DiaChi = dr["diaChi"].ToString()
                    });
                }
            }
            return ds;
        }

        public List<LopHocDTO> GetAllTen()
        {
            List<LopHocDTO> ds = new List<LopHocDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_LopHoc_GetAllTen", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ds.Add(new LopHocDTO
                    {
                        IdLopHoc = (int)dr["idLopHoc"],
                        TenLopHoc = dr["tenLopHoc"].ToString(),
                        TenKhoaHoc = dr["tenKhoaHoc"].ToString(),
                        TenGiangVien = dr["tenGiangVien"].ToString(),
                        HocPhi = (decimal)dr["hocPhi"],
                        NgayKhaiGiang = (DateTime)dr["ngayKhaiGiang"],
                        NgayKetThuc = (DateTime)dr["ngayKetThuc"]
                    });
                }
            }

            return ds;
        }
    }

}
