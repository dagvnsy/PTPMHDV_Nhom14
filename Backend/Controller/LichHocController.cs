using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrungTamNgoaiNgu.BUS;
using TrungTamNgoaiNgu.DTO;

namespace TrungTamNgoaiNgu.Controllers
{
    [RoutePrefix("api/LichHoc")]
    public class LichHocController : ApiController
    {
        LichHocBUS bus = new LichHocBUS();

        [HttpGet]
        [Route("GetAllFull")]
        public List<LichHocDTO> GetAllFull()
        {
            return bus.GetAllFull();
        }


        [HttpGet, Route("GetById/{id}")]
        public LichHocDTO GetById(int id) => bus.GetById(id);

        [HttpGet, Route("GetByLopHoc/{idLopHoc}")]
        public List<LichHocDTO> GetByLopHoc(int idLopHoc)
            => bus.GetByLopHoc(idLopHoc);

        [HttpPost, Route("Create")]
        public bool Create(LichHocDTO lh) => bus.Them(lh);

        [HttpPut, Route("Update/{id}")]
        public bool Update(int id, LichHocDTO lh)
        {
            lh.IdLichHoc = id;
            return bus.Sua(lh);
        }

        [HttpDelete, Route("Delete/{id}")]
        public bool Delete(int id) => bus.Xoa(id);
    }

}
