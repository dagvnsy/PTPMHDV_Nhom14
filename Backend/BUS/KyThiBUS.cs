using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class KyThiBUS
    {
        KyThiDAL dal = new KyThiDAL();

        public List<KyThiDTO> GetAll()
        {
            return dal.GetAll();
        }
        public KyThiDTO GetById(int id)
        {
            return dal.GetById(id);
        }
        public List<KyThiDTO> GetByHocVien(int idHocVien)
        {
            return dal.GetByHocVien(idHocVien);
        }
        public bool Them(KyThiDTO kt)
        {
            return dal.Them(kt);
        }

        public bool Sua(KyThiDTO kt)
        {
            return dal.Sua(kt);
        }

        public bool Xoa(int id)
        {
            return dal.Xoa(id);
        }

        public bool CapNhatTrangThaiNhapDiem(int idKyThi, bool isMo)
        {
            return dal.CapNhatTrangThaiNhapDiem(idKyThi, isMo);
        }
        public List<KyThiDTO> GetByLopHoc(int idLopHoc)
        {
            return dal.GetByLopHoc(idLopHoc);
        }


    }
}
