using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class DangKyHocVienBUS
    {
        DangKyHocVienDAL dal = new DangKyHocVienDAL();

        public bool DangKy(DangKyHocVienDTO dto)
        {
            // Có thể hash mật khẩu ở đây
            return dal.DangKy(dto);
        }
    }
}
