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
    [RoutePrefix("api/hocvien")]
    public class HocVienController : ApiController
    {
        HocVienBUS bus = new HocVienBUS();

        [HttpGet]
        [Route("GetAll")]
        public List<HocVienDTO> GetAll()
        {
            return bus.XemTatCa();
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public HocVienDTO GetById(int id)
        {
            return bus.LayTheoId(id);
        }

        [HttpPost]
        [Route("Create")]
        public bool Create(HocVienDTO hv)
        {
            return bus.Them(hv);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public bool Update(int id, HocVienDTO hv)
        {
            hv.IdHocVien = id;   
            return bus.Sua(hv);
        }



        [HttpDelete]
        [Route("Delete/{id}")]
        public bool Delete(int id)
        {
            return bus.Xoa(id);
        }

        [HttpGet]
        [Route("TheoTaiKhoan/{idTaiKhoan}")]
        public IHttpActionResult GetByTaiKhoan(int idTaiKhoan)
        {
            var hv = bus.GetByTaiKhoan(idTaiKhoan);
            if (hv == null)
                return NotFound();

            return Ok(hv);
        }

    }
}
