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
    [RoutePrefix("api/DangKy")]
    public class DangKyHocVienController : ApiController
    {
        DangKyHocVienBUS bus = new DangKyHocVienBUS();

        [HttpPost]
        [Route("HocVien")]
        public IHttpActionResult DangKyHocVien(DangKyHocVienDTO dto)
        {
            return Ok(bus.DangKy(dto));
        }
    }
}
