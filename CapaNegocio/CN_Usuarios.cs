using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDatos = new CD_Usuarios();
        //Listar usuario
        public List<Usuario> Listar()
        {
            return objCapaDatos.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El Nombre del Usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellido del Usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del Usuario no puede ser vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Nueva Cuenta";
                string mensaje_correo = "<h3>Su Cuenta fué generada con éxito</h3></br><p>Su contraseña para acceder es: !clave!</prop>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);
                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);
                if (respuesta)
                {
                    obj.Clave = CN_Recursos.GetSHA256(clave);
                    return objCapaDatos.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puedo enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El Nombre del Usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellido del Usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del Usuario no puede ser vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            return objCapaDatos.CambiarClave(idusuario, nuevaclave, out Mensaje);
        }

        public bool ReestablecerClave(int idusuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDatos.ReestablecerClave(idusuario, CN_Recursos.GetSHA256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su Cuenta fué reestablecida correctamente</h3></br><p>" +
                    "Su contraseña para acceder es: !clave!</prop>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);
                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);
                if (!respuesta)
                {
                    Mensaje = "No Se puedo Enviar el Resultado";
                }
                return respuesta;
            }
            else
            {
                Mensaje = "No Se puedo Reestablecer la Contraseña";
                return resultado;
            }
        }

    }
}
