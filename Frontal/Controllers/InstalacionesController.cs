using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using CapaDTO.DataAccess.DTO;
using CapaDTO.DataAccess.AccesoInstalaciones;
using System.Net.Http;
using System.Net;

namespace Frontal.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class InstalacionesController : ApiController
    {
        [HttpGet]
        [Route("api/Instalaciones/getInstalaciones")]
        public HttpResponseMessage allInstalaciones()
        {
            var allInstDA = new InstalacionDataAccess();
            IEnumerable<InstalacionResponse> datosInst = allInstDA.GetAllInstalaciones();
            return Request.CreateResponse(HttpStatusCode.OK, datosInst);
        }

        [HttpGet]
        [Route("api/Instalaciones/getInstalacion/{id}")]
        public HttpResponseMessage getInstalacion(int id)
        {
            var getInstDA = new InstalacionDataAccess();
            InstalacionResponse datosInst = getInstDA.GetInstalacion(id);
            return Request.CreateResponse(HttpStatusCode.OK, datosInst);
        }

        [HttpPost]
        [Route("api/Instalaciones/addInstalacion")]
        public HttpResponseMessage addInstalacion(InstalacionRequest request)
        {
            var addInstDA = new InstalacionDataAccess();
            int datosInst = addInstDA.AddInstalacion(request);
            return Request.CreateResponse(HttpStatusCode.OK, datosInst);
        }

    }
}
