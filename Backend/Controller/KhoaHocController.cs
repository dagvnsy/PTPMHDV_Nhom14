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
    [RoutePrefix("api/KhoaHoc")]
    public class KhoaHocController : ApiController
    {
        KhoaHocBUS bus = new KhoaHocBUS();

        [HttpGet]
        [Route("GetAll")]
        public List<KhoaHocDTO> GetAll()
        {
            return bus.GetAll();
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public KhoaHocDTO GetById(int id)
        {
            return bus.GetById(id);
        }

        
        [HttpGet]
        [Route("Search")]
        public List<KhoaHocDTO> Search(string keyword)
        {
            return bus.TimKiem(keyword);
        }

        [HttpPost]
        [Route("Create")]
        public bool Create(KhoaHocDTO kh)
        {
            return bus.Them(kh);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public bool Update(int id, KhoaHocDTO kh)
        {
            kh.IdKhoaHoc = id;
            return bus.Sua(kh);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public bool Delete(int id)
        {
            return bus.Xoa(id);
        }

        [HttpGet]
        [Route("GetByLoaiTieng")]
        public IHttpActionResult GetByLoaiTieng(string loaiTieng)
        {
            return Ok(bus.LayTheoLoaiTieng(loaiTieng));
        }

    }
}
