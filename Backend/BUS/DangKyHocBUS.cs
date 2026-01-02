using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class DangKyHocBUS
    {
        DangKyHocDAL dal = new DangKyHocDAL();
        LopHocDAL lopHocDAL = new LopHocDAL();
        HoaDonDAL hoaDonDAL = new HoaDonDAL();

        public bool DangKyVaTaoHoaDon(DangKyHocDTO dk)
        {
            // 1. Kiểm tra đã đăng ký chưa
            if (dal.DaDangKy(dk.IdHocVien, dk.IdLopHoc))
                return false;

            // 2. Đăng ký lớp
            bool dkThanhCong = dal.Them(dk);
            if (!dkThanhCong) return false;

            // 3. Lấy thông tin lớp 
            LopHocDTO lop = lopHocDAL.GetById(dk.IdLopHoc);
            if (lop == null) return false;

            // 4. Tạo hóa đơn
            HoaDonDTO hd = new HoaDonDTO
            {
                IdHocVien = dk.IdHocVien,
                IdLopHoc = dk.IdLopHoc,
                SoTien = lop.HocPhi,
                TrangThai = "CHUA_THANH_TOAN",
                GhiChu = "Hóa đơn đăng ký lớp"
            };

            hoaDonDAL.Them(hd);

            return true;
        }



        public bool Xoa(int id) => dal.Xoa(id);
        public List<DangKyHocDTO> GetAll() => dal.GetAll();
        public DangKyHocDTO GetById(int id) => dal.GetById(id);
    }

}
