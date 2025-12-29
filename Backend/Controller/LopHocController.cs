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
    [RoutePrefix("api/LopHoc")]
    public class LopHocController : ApiController
    {
        LopHocBUS bus = new LopHocBUS();

        [HttpGet, Route("GetAll")]
        public List<LopHocDTO> GetAll() => bus.GetAll();

        [HttpGet, Route("GetById/{id}")]
        public LopHocDTO GetById(int id) => bus.GetById(id);

        [HttpPost, Route("Create")]
        public bool Create(LopHocDTO lop) => bus.Them(lop);

        [HttpPut, Route("Update/{id}")]
        public bool Update(int id, LopHocDTO lop)
        {
            lop.IdLopHoc = id;
            return bus.Sua(lop);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            bool ketQua = bus.Xoa(id);

            if (!ketQua)
                return BadRequest("Không thể xóa lớp khi còn học viên");

            return Ok("Xóa lớp thành công");
        }

        [HttpGet]
        [Route("TheoHocVien/{idHocVien}")]
        public IHttpActionResult GetByHocVien(int idHocVien)
        {
            return Ok(bus.GetByHocVien(idHocVien));
        }

        [HttpGet]
        [Route("GetByKhoaHoc/{idKhoaHoc}")]
        public IHttpActionResult GetByKhoaHoc(int idKhoaHoc)
        {
            return Ok(bus.GetByKhoaHoc(idKhoaHoc));
        }

        [HttpGet]
        [Route("GetByGiangVien/{idGiangVien}")]
        public IHttpActionResult GetByGiangVien(int idGiangVien)
        {
            return Ok(bus.GetByGiangVien(idGiangVien));
        }

        [HttpGet]
        [Route("GetByLopHoc/{idLopHoc}")]
        public IHttpActionResult GetHocVienByLopHoc(int idLopHoc)
        {
            return Ok(bus.GetHocVienByLop(idLopHoc));
        }


        [HttpGet, Route("Search")]
        public List<LopHocDTO> Search(string keyword) => bus.TimKiem(keyword);



        [HttpGet]
        [Route("GetAllTen")]
        public List<LopHocDTO> GetAllTen()
        {
            return bus.GetAllTen();
        }
    }

}
