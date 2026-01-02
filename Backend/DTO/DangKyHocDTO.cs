using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungTamNgoaiNgu.DTO
{
    public class DangKyHocDTO
    {
        public int IdDangKy { get; set; }
        public int IdHocVien { get; set; }
        public int IdLopHoc { get; set; }
        public DateTime NgayDangKy { get; set; }
        public string TrangThai { get; set; }
    }

}
