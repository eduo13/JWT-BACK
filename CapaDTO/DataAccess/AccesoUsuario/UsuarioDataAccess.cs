using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using CapaDTO.DataAccess.DTO;
using capaDAO.Usuario;

namespace CapaDTO.DataAccess.AccesoUsuario
{
    public class UsuarioDataAccess
    {

        public LoginResponse Login(LoginRequest login)
        {

            try
            {

                using (var context = new BDReservasEntities())
                {
                    ObjectParameter RETCODE = new ObjectParameter("RETCODE", typeof(int));
                    ObjectParameter MENSAJE = new ObjectParameter("MENSAJE", typeof(string));
                    ObjectParameter ID = new ObjectParameter("ID", typeof(int));

                    context.PA_LOGIN(login.Email, login.Password, ID, RETCODE, MENSAJE);

                    if ((int)RETCODE.Value < 0)
                    {
                        throw new Exception("Error no controlado");
                    }

                    if ((int)RETCODE.Value > 0)
                    {
                        throw new Exception(MENSAJE.Value.ToString());
                    }

                    //var consulta = context.usuarios.Where(datos => datos.id_usuario == (int)ID.Value).FirstOrDefault();
                    List<LoginResponse> consulta = (from u in context.usuarios
                                    join p in context.perfiles
                                    on u.id_perfil equals p.id_perfil
                                    where u.id_usuario == (int)ID.Value
                                    select new LoginResponse
                                    {
                                        Nombre = u.nombre.Trim(),
                                        Id_usuario = (int)u.id_usuario,
                                        Apellido1 = u.apellido1.Trim(),
                                        Apellido2 = u.apellido2.Trim(),
                                        Dni = u.dni.Trim(),
                                        Email = u.email.Trim(),
                                        Perfil = p.perfil.Trim(),
                                        RetCode = (int)RETCODE.Value,
                                        Mensaje = MENSAJE.Value.ToString().Trim()
                                    }).ToList<LoginResponse>();

                    
                    return consulta.ElementAt(0);

                }

            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    RetCode = -1,
                    Mensaje = ex.Message.Trim()
                };
            }


        }

        /*
        public NewUserResponse CreateUser(NewUserRequest newUser)
        {
            try
            {

                using (var context = new BDreservasEntities())
                {
                    ObjectParameter RETCODE = new ObjectParameter("RETCODE", typeof(int));
                    ObjectParameter MENSAJE = new ObjectParameter("MENSAJE", typeof(string));

                    context.PA_INSERT_USUARIO(
                        newUser.Nombre, newUser.Apellido1, newUser.Apellido2,
                        newUser.Dni, newUser.Email, newUser.Password, newUser.Id_perfil, RETCODE, MENSAJE);

                    if ((int)RETCODE.Value < 0)
                    {
                        throw new Exception("Error no controlado");
                    }

                    if ((int)RETCODE.Value > 0)
                    {
                        throw new Exception(MENSAJE.Value.ToString());
                    }

                    var consulta = context.usuarios.Where(datos => datos.dni == newUser.Dni).FirstOrDefault();

                    return new NewUserResponse()
                    {
                        Nombre = consulta.nombre,
                        Apellido1 = consulta.apellido1,
                        Apellido2 = consulta.apellido2,
                        Mensaje = (string)MENSAJE.Value
                    };

                }

            }
            catch (Exception ex)
            {
                return new NewUserResponse()
                {
                    Mensaje = ex.Message
                };
            }

        }


        public DeleteResponse DeleteUser(int ID)
        {
            try
            {

                using (var context = new BDreservasEntities())
                {
                    ObjectParameter RETCODE = new ObjectParameter("RETCODE", typeof(int));
                    ObjectParameter MENSAJE = new ObjectParameter("MENSAJE", typeof(string));

                    context.PA_DELETE_USUARIO(ID, RETCODE, MENSAJE);

                    if ((int)RETCODE.Value < 0)
                    {
                        throw new Exception("Error no controlado");
                    }

                    if ((int)RETCODE.Value > 0)
                    {
                        throw new Exception(MENSAJE.Value.ToString());
                    }

                    return new DeleteResponse()
                    {
                        Id = ID,
                        Mensaje = (string)MENSAJE.Value + " con id: " + ID
                    };

                }

            }
            catch (Exception ex)
            {
                return new DeleteResponse()
                {
                    Id = ID,
                    Mensaje = ex.Message
                };
            }

        }*/

        public LoginResponse GetUser(int id)
        {
            try
            {
                using (var context = new BDReservasEntities())
                {
                    var usuario = context.usuarios.Where(user => user.id_usuario == id).FirstOrDefault();

                    string imgB64 = Convert.ToBase64String(usuario.imagen);

                    if (usuario != null)
                    {
                        return new LoginResponse()
                        {
                            Id_usuario = usuario.id_usuario,
                            Nombre = usuario.nombre,
                            Apellido1 = usuario.apellido1,
                            Apellido2 = usuario.apellido2,
                            Dni = usuario.dni,
                            Imagen = imgB64,
                            Mensaje = "usuario solicitado"
                        };
                    }
                    else
                    {
                        return new LoginResponse()
                        {
                            Mensaje = "no se encontró el usuario solicitado"
                        };

                    }

                }

            }catch (Exception ex)
            {
                return null;
            }

        }


            public ImageBinary InsertImg(ImageBinary image)
        {
            try
            {
                using(var context = new BDReservasEntities())
                {
                    var usuario = context.usuarios.Where(user => user.email == image.Email).FirstOrDefault();

                        usuario.imagen = image.Image;

                    context.SaveChanges();

                    usuario = context.usuarios.Where(user => user.email == image.Email).FirstOrDefault(); ;

                    ImageBinary img = new ImageBinary()
                    {
                        Name = image.Name,
                        Image = usuario.imagen,
                        Email = image.Email
                    };

                    return img;
                }

            }catch(Exception ex)
            {
                return  new ImageBinary()
                {
                    Name = ex.Message,
                    Image = null,
                    Email = null
                };
            }
        }

        public IEnumerable<LoginResponse> GetAllUsers()
        {
            try
            {
                using (var context = new BDReservasEntities())
                {
                    List<LoginResponse> listaUsuarios = (from u in context.usuarios
                                                         join p in context.perfiles
                                                         on u.id_perfil equals p.id_perfil
                                                         select new LoginResponse
                                                         {
                                                             Nombre = u.nombre.Trim(),
                                                             Id_usuario = (int)u.id_usuario,
                                                             Apellido1 = u.apellido1.Trim(),
                                                             Apellido2 = u.apellido2.Trim(),
                                                             Dni = u.dni.Trim(),
                                                             Email = u.email.Trim(),
                                                             Perfil = p.perfil.Trim()
                                                         }).ToList<LoginResponse>();

                    return listaUsuarios;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
