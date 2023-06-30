using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCapaDatos = new CD_Categoria();
        //Listar Categorías
        public List<Categoria> Listar()
        {
            return objCapaDatos.Listar();
        }
        //Registrar Categorías
        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje = "El Campo Descripción no puede ser vacío";
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
        //Editar Categorías
        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje = "El Campo Descripción no puede ser vacío";
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
        //Editar Categorías
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }
    }
}
