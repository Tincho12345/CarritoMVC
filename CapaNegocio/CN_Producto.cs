using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCapaDatos = new CD_Producto();
        //Listar Producto
        public List<Producto> Listar()
        {
            return objCapaDatos.Listar();
        }

        public List<Producto> Listar1()
        {
            return objCapaDatos.Listar1();
        }
        //Registrar Producto
        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El Campo Nombre no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "El Campo Descripción no puede ser vacío";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe Seleccionar Una Marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Seleccionar Una Categoría";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Ingrese Precio del Producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Ingrese Stock del Producto";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
        //Editar Producto
        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El Campo Nombre no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "El Campo Descripción no puede ser vacío";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe Seleccionar Una Marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Seleccionar Una Categoría";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Ingrese Precio del Producto";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Ingrese Stock del Producto";
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
        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            return objCapaDatos.GuardarDatosImagen(obj, out Mensaje);
        }
        //Eliminar Producto
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }
    }
}
