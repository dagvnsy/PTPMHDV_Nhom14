using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class BaoCaoBUS
    {
        BaoCaoDAL dal = new BaoCaoDAL();

        public List<BaoCaoSoLopDTO> SoLopTheoKhoaHoc()
        {
            return dal.SoLopTheoKhoaHoc();
        }

        public List<BaoCaoHocVienDTO> HocVienTheoLop(int id)
        {
            return dal.HocVienTheoLop(id);
        }

        public List<BaoCaoKetQuaThiDTO> KetQuaThi(int idKyThi)
        {
            return dal.KetQuaThi(idKyThi);
        }
    }

}
