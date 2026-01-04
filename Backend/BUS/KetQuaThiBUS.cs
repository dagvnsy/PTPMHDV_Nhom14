using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class KetQuaThiBUS
    {
        KetQuaThiDAL dal = new KetQuaThiDAL();

  
        public List<KetQuaThiDTO> GetByKyThi(int idKyThi)
        {
            return dal.GetByKyThi(idKyThi);
        }

        public bool Luu(KetQuaThiDTO dto)
        {
            return dal.Luu(dto);
        }

        public List<KetQuaThiDTO> GetByLopHoc(int idKyThi)
        {
            return dal.GetByLopHoc(idKyThi);
        }

        public bool Sua(KetQuaThiDTO kq)
        {
            return dal.Sua(kq);
        }

    }
}
