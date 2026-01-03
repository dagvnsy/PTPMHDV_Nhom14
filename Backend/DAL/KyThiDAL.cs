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
    public class KyThiDAL
    {
        private string chuoiKetNoi =
            "Data Source=LAPTOP-S2AJ7CP6;Initial Catalog=QLTTNN;Integrated Security=True";

        public List<KyThiDTO> GetAll()
        {
            List<KyThiDTO> ds = new List<KyThiDTO>();
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KyThi_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ds.Add(new KyThiDTO
                    {
                        IdKyThi = (int)rd["idKyThi"],
                        TenKyThi = rd["tenKyThi"].ToString(),
                        LoaiKyThi = rd["loaiKyThi"].ToString(),
                        IdLopHoc = rd["idLopHoc"] == DBNull.Value ? null : (int?)rd["idLopHoc"],
                        IdKhoaHoc = rd["idKhoaHoc"] == DBNull.Value ? null : (int?)rd["idKhoaHoc"],
                        NgayThi = (DateTime)rd["ngayThi"],
                        ThangDiem = (int)rd["thangDiem"],
                        IsMoNhapDiem = rd["IsMoNhapDiem"] != DBNull.Value
                        && Convert.ToBoolean(rd["IsMoNhapDiem"])
                    });
                }
            }
            return ds;
        }

        public KyThiDTO GetById(int id)
        {
            KyThiDTO kt = null;
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KyThi_GetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKyThi", id);
                conn.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    kt = new KyThiDTO
                    {
                        IdKyThi = (int)rd["idKyThi"],
                        TenKyThi = rd["tenKyThi"].ToString(),
                        LoaiKyThi = rd["loaiKyThi"].ToString(),
                        IdLopHoc = rd["idLopHoc"] == DBNull.Value ? null : (int?)rd["idLopHoc"],
                        IdKhoaHoc = rd["idKhoaHoc"] == DBNull.Value ? null : (int?)rd["idKhoaHoc"],
                        NgayThi = (DateTime)rd["ngayThi"],
                        ThangDiem = (int)rd["thangDiem"]
                    };
                }
            }
            return kt;
        }


        public List<KyThiDTO> GetByHocVien(int idHocVien)
        {
            List<KyThiDTO> ds = new List<KyThiDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KyThi_GetByHocVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idHocVien", idHocVien);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ds.Add(new KyThiDTO
                    {
                        IdKyThi = (int)dr["idKyThi"],
                        TenKyThi = dr["tenKyThi"].ToString(),
                        LoaiKyThi = dr["loaiKyThi"].ToString(),
                        IdLopHoc = dr["idLopHoc"] == DBNull.Value ? null : (int?)dr["idLopHoc"],
                        IdKhoaHoc = dr["idKhoaHoc"] == DBNull.Value ? null : (int?)dr["idKhoaHoc"],
                        NgayThi = (DateTime)dr["ngayThi"],
                        ThangDiem = (int)dr["thangDiem"],
                        Diem = dr["diem"] == DBNull.Value ? null : (decimal?)dr["diem"],
                        KetQua = dr["ketQua"] == DBNull.Value ? null : dr["ketQua"].ToString()

                    });
                }
            }
            return ds;
        }



        public bool Them(KyThiDTO kt)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KyThi_Them", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tenKyThi", kt.TenKyThi);
                cmd.Parameters.AddWithValue("@loaiKyThi", kt.LoaiKyThi);
                cmd.Parameters.AddWithValue("@idLopHoc", (object)kt.IdLopHoc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@idKhoaHoc", (object)kt.IdKhoaHoc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ngayThi", kt.NgayThi);
                cmd.Parameters.AddWithValue("@thangDiem", kt.ThangDiem);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Sua(KyThiDTO kt)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KyThi_Sua", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idKyThi", kt.IdKyThi);
                cmd.Parameters.AddWithValue("@tenKyThi", kt.TenKyThi);
                cmd.Parameters.AddWithValue("@loaiKyThi", kt.LoaiKyThi);
                cmd.Parameters.AddWithValue("@idLopHoc", (object)kt.IdLopHoc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@idKhoaHoc", (object)kt.IdKhoaHoc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ngayThi", kt.NgayThi);
                cmd.Parameters.AddWithValue("@thangDiem", kt.ThangDiem);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(int id)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KyThi_Xoa", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idKyThi", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CapNhatTrangThaiNhapDiem(int idKyThi, bool isMo)
        {
            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE KyThi
                    SET IsMoNhapDiem = @isMo
                    WHERE IdKyThi = @id", conn);

                cmd.Parameters.AddWithValue("@isMo", isMo);
                cmd.Parameters.AddWithValue("@id", idKyThi);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<KyThiDTO> GetByLopHoc(int idLopHoc)
        {
            List<KyThiDTO> ds = new List<KyThiDTO>();

            using (SqlConnection conn = new SqlConnection(chuoiKetNoi))
            {
                SqlCommand cmd = new SqlCommand("sp_KyThi_GetByLopHoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idLopHoc", idLopHoc);

                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ds.Add(new KyThiDTO
                    {
                        IdKyThi = (int)rd["idKyThi"],
                        TenKyThi = rd["tenKyThi"].ToString(),
                        LoaiKyThi = rd["loaiKyThi"].ToString(),
                        NgayThi = (DateTime)rd["ngayThi"],
                        ThangDiem = (int)rd["thangDiem"],
                        IdLopHoc = rd["idLopHoc"] as int?,
                        IdKhoaHoc = rd["idKhoaHoc"] as int?,
                        IsMoNhapDiem = (bool)rd["IsMoNhapDiem"]
                    });
                }
            }

            return ds;
        }

    }
}
