using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCapaDatos = new CD_Cliente();

        //Registrar Cliente
        public int Registrar(Cliente obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                mensaje = "El Nombre del Cliente no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                mensaje = "El Apellido del Cliente no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                mensaje = "El Correo del Cliente no puede ser vacío";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                obj.Clave = CN_Recursos.GetSHA256(obj.Clave);
                return objCapaDatos.Registrar(obj, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        //Listar Cliente
        public List<Cliente> Listar()
        {
            return objCapaDatos.Listar();
        }

        //Cambiar CLAVE Cliente
        public bool CambiarClave(int idCliente, string nuevaclave, out string mensaje)
        {
            return objCapaDatos.CambiarClave(idCliente, nuevaclave, out mensaje);
        }

        //Reestablecer CLAVE Cliente
        public bool ReestablecerClave(int idCliente, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDatos.ReestablecerClave(idCliente, CN_Recursos.GetSHA256(nuevaclave), out mensaje);

            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su Cuenta fué reestablecida correctamente</h3></br><p>" +
                    "Su contraseña para acceder es: !clave!</prop>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);
                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);
                if (!respuesta)
                {
                    mensaje = "No Se puedo Enviar el Resultado";
                }
                return respuesta;
            }
            else
            {
                mensaje = "No Se puedo Reestablecer la Contraseña";
                return resultado;
            }
        }
    }
}
