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
    [RoutePrefix("api/DangKyHoc")]
    public class DangKyHocController : ApiController
    {
        DangKyHocBUS bus = new DangKyHocBUS();

        [HttpGet, Route("GetAll")]
        public List<DangKyHocDTO> GetAll() => bus.GetAll();

        [HttpGet, Route("GetById/{id}")]
        public DangKyHocDTO GetById(int id) => bus.GetById(id);

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(DangKyHocDTO dk)
        {
            bool ketQua = bus.DangKyVaTaoHoaDon(dk);

            if (!ketQua)
                return BadRequest("Học viên đã đăng ký lớp này");

            return Ok("Đăng ký học thành công. Hóa đơn đã được tạo.");
        }




        [HttpDelete, Route("Delete/{id}")]
        public bool Delete(int id) => bus.Xoa(id);
    }

}
