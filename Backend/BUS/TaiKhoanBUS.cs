using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;
using TrungTamNgoaiNgu.DTO.TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class TaiKhoanBUS
    {
        TaiKhoanDAL dal = new TaiKhoanDAL();

        public bool Them(TaiKhoanDTO tk)
        {
            // Có thể hash mật khẩu ở đây nếu muốn nâng cao
            return dal.Them(tk);
        }

        public bool DoiMatKhau(int idTaiKhoan, string matKhauMoi)
        {
            return dal.DoiMatKhau(idTaiKhoan, matKhauMoi);
        }

        public bool CapNhatTrangThai(int idTaiKhoan, bool trangThai)
        {
            return dal.CapNhatTrangThai(idTaiKhoan, trangThai);
        }


        public TaiKhoanDTO DangNhap(DangNhapDTO dto)
        {
            return dal.DangNhap(dto.TenDangNhap, dto.MatKhau);
        }
    }
}
