using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDTO.Helpers
{
    public class ImageConverter
    {
        string data = "data:image/png;base64,";

        public string byteTobase64(byte[] imagen)
        {
            string temp = Convert.ToBase64String(imagen);
            return data + temp;
        }

        public byte[] base64ToByte(string imagen)
        {
            string datosImg = imagen.Replace(data, "").ToString();
            byte[] temp = Convert.FromBase64String(datosImg);
            return temp;
        }
    }
}
