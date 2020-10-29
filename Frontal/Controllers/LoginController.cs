using System;
using System.Net;
using System.Web.Http.Cors;
using System.Threading;
using System.Web.Http;
using CapaDTO.DataAccess.AccesoUsuario;
using CapaDTO.DataAccess.DTO;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Frontal.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        /*
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }
        */

        [HttpPost]
        [Route("authenticate")]
        public HttpResponseMessage Authenticate(LoginRequest login)
        {
            if (login.Email == null || login.Password == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error en la petición");
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            //instancia para acceso al modelo 
            var loginDA = new UsuarioDataAccess();
            var autenticado = loginDA.Login(login);

            //si la bbdd confirmó user y contraseña
            if (autenticado.RetCode == 0)
            {
                //Generamos token y lo cargamos en la respuesta
                var token = TokenGenerator.GenerateTokenJwt(autenticado.Email);
                autenticado.Token = token;
                //Enviamos la respuesta
                return Request.CreateResponse(HttpStatusCode.OK, autenticado);
            }
            else
            {
                //return Unauthorized();
                //return Request.CreateResponse(HttpStatusCode.Unauthorized, autenticado.Mensaje);
                return Request.CreateResponse(HttpStatusCode.OK, autenticado.Mensaje);
            }
        }

    }
}
