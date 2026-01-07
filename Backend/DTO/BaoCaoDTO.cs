using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class BaoCaoSoLopDTO
    {
        public string TenKhoaHoc { get; set; }
        public int SoLop { get; set; }
    }

    public class BaoCaoHocVienDTO
    {
        public int IdHocVien { get; set; }
        public string TenHocVien { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
    }

    public class BaoCaoKetQuaThiDTO
    {
        public string TenHocVien { get; set; }
        public decimal Diem { get; set; }
        public string KetQua { get; set; }
    }


}
