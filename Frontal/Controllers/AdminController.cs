using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CapaDTO.DataAccess.AccesoUsuario;
using CapaDTO.DataAccess.DTO;

namespace Frontal.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [Authorize]
    public class AdminController : ApiController
    {
        string data = "data:image/png;base64,";

        [HttpGet]
        [Route("api/Admin/getAllusers")]
        public IEnumerable<LoginResponse> allUsers()
        {
            //var identity = Thread.CurrentPrincipal.Identity;
            //return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
            var allusersDA = new UsuarioDataAccess();
            IEnumerable<LoginResponse> datosuser = allusersDA.GetAllUsers();
            return datosuser;
        }


        [HttpGet]
        [Route("api/Admin/getUser/{id}")]
        public LoginResponse GetUser(int id)
        {
            var getUserDA = new UsuarioDataAccess();
            LoginResponse datosuser = getUserDA.GetUser(id);
            datosuser.Imagen = data + datosuser.Imagen;
            return datosuser;
        }


        [HttpPost]
        [Route("api/Admin/uploadImage")]
        public HttpResponseMessage UploadImage(ImageRequest image)
        {
            ImageBinary newImg = null;
            ImageRequest response = null;
            if (image.File != null)
            {
                //eliminamos datos sobrantes
                string datosImg = image.File.Replace(data, "").ToString();
                //convertimos a byte[]
                byte[] imgBinary = Convert.FromBase64String(datosImg);

                var insertImgDA = new UsuarioDataAccess();
                //insertamos
                newImg = insertImgDA.InsertImg(new ImageBinary { Image = imgBinary, Name = image.Name, Email = image.Email });

                //convertimos a base64 para devolver imagen
                string imgStr = Convert.ToBase64String(newImg.Image);
                //cargamos los datos
                imgStr = data + imgStr;
                response = new ImageRequest()
                {
                    File = imgStr,
                    Name = newImg.Name,
                    Email = newImg.Email
                };
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}
