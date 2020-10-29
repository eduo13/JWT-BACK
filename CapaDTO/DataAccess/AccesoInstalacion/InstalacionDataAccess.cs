using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaDAO.Usuario;
using CapaDTO.DataAccess.DTO;
using CapaDTO.Helpers;

namespace CapaDTO.DataAccess.AccesoInstalaciones
{
    public class InstalacionDataAccess
    {
         public IEnumerable<InstalacionResponse> GetAllInstalaciones()
        {
            try
            {
                using (var context = new BDReservasEntities())
                {
                    
                    List<InstalacionResponse> listaInstalaciones = (from i in context.instalaciones
                                                                   select new InstalacionResponse
                                                                   {
                                                                       Nombre = i.nombre.Trim(),
                                                                       Id_instalacion = (int)i.id_instalacion,
                                                                       Id_horario = (int)i.id_horario,
                                                                       Direccion = i.direccion.Trim(),
                                                                       Operativa = (bool)i.operativa,
                                                                       ImgTemp = i.imagen
                                                                   }).ToList<InstalacionResponse>();

                    if (listaInstalaciones.Count < 1)
                    {
                        listaInstalaciones.Add(new InstalacionResponse
                        {
                            Mensaje = "No existen instalaciones"
                        });

                    }
                    else
                    {   //parche para convertir cada imagen el array de instalaciones
                        foreach(InstalacionResponse instalacion in listaInstalaciones)
                        {
                            instalacion.Imagen = (instalacion.ImgTemp != null) ? new ImageConverter().byteTobase64(instalacion.ImgTemp) : null;
                            instalacion.ImgTemp = null;
                        }
                    }

                    return listaInstalaciones;

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddInstalacion(InstalacionRequest request)
        {
           int resultado;

            try
            {
                using (var context = new BDReservasEntities())
                {
                    instalaciones instalacion = new instalaciones();

                    instalacion.id_horario = request.Id_horario;
                    instalacion.nombre = request.Nombre;
                    instalacion.direccion = request.Direccion;
                    instalacion.operativa = request.Operativa;
                    //si llega imagen hay que convertirla
                    if(request.Imagen != null)
                    {
                        instalacion.imagen = new ImageConverter().base64ToByte(request.Imagen);
                    }

                    context.instalaciones.Add(instalacion);

                    context.SaveChanges();

                    //comprobamos que se ha insertado en bbdd correctamente
                    return resultado = (instalacion.id_instalacion > 0) ? instalacion.id_instalacion : -1;

                }


            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public InstalacionResponse GetInstalacion(int id)
        {
            try
            {
                using (var context = new BDReservasEntities())
                {

                    InstalacionResponse instalacion = (from i in context.instalaciones
                                                       where i.id_instalacion == id
                                                       select new InstalacionResponse
                                                       {
                                                           Nombre = i.nombre.Trim(),
                                                           Id_instalacion = (int)i.id_instalacion,
                                                           Id_horario = (int)i.id_horario,
                                                           Direccion = i.direccion.Trim(),
                                                           Operativa = (bool)i.operativa,
                                                           ImgTemp = i.imagen
                                                       }).FirstOrDefault();

                    if (instalacion == null)
                    {
                        return new InstalacionResponse
                        {
                            Mensaje = "No existe la instalacion solicitada"
                        };

                    }
                    else
                    {   
                        instalacion.Imagen = (instalacion.ImgTemp != null) ? new ImageConverter().byteTobase64(instalacion.ImgTemp) : null;
                        instalacion.ImgTemp = null;
                    }

                    return instalacion;

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
