using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO.DataAccess.DTO
{
    public class LoginResponse
    {
        public int Id_usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public string Password { get; set; }
        public string Imagen { get; set; }
        public string Token { get; set; }
        public int RetCode { get; set; }
        public string Mensaje { get; set; }
    }
}
