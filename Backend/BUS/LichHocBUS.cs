using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamNgoaiNgu.DAL;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.BUS
{
    public class LichHocBUS
    {
        LichHocDAL dal = new LichHocDAL();

        public bool Them(LichHocDTO lh) => dal.Them(lh);
        public bool Sua(LichHocDTO lh) => dal.Sua(lh);
        public bool Xoa(int id) => dal.Xoa(id);
        public List<LichHocDTO> GetAllFull()
        {
            return dal.GetAllFull();
        }

        public LichHocDTO GetById(int id) => dal.GetById(id);
        public List<LichHocDTO> GetByLopHoc(int idLopHoc) => dal.GetByLopHoc(idLopHoc);
    }

}
