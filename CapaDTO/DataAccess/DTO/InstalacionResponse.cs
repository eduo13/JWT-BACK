using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO.DataAccess.DTO
{
    public class InstalacionResponse
    {
        public int Id_instalacion { get; set; }
        public int Id_horario { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool Operativa { get; set; }
        public byte[] ImgTemp { get; set; }
        public string Imagen { get; set; }
        public string Mensaje { get; set; }

    }
}
