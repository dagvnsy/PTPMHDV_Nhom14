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
    [RoutePrefix("api/GiangVien")]
    public class GiangVienController : ApiController
    {
        GiangVienBUS bus = new GiangVienBUS();

        [HttpGet]
        [Route("GetAll")]
        public List<GiangVienDTO> GetAll()
        {
            return bus.GetAll();
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public GiangVienDTO GetById(int id)
        {
            return bus.GetById(id);
        }

        [HttpGet]
        [Route("Search")]
        public List<GiangVienDTO> Search(string keyword)
        {
            return bus.TimKiem(keyword);
        }

        [HttpPost]
        [Route("Create")]
        public bool Create(GiangVienDTO gv)
        {
            return bus.Them(gv);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public bool Update(int id, GiangVienDTO gv)
        {
            gv.IdGiangVien = id;
            return bus.Sua(gv);
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
            return Ok(bus.GetByTaiKhoan(idTaiKhoan));
        }

    }
}
