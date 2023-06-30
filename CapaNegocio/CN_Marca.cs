using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Marca
    {
        private CD_Marca objCapaDatos = new CD_Marca();
        //Listar Marca
        public List<Marca> Listar()
        {
            return objCapaDatos.Listar();
        }
        //Registrar Marca
        public int Registrar(Marca obj, out string Mensaje)
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
        //Editar Marca
        public bool Editar(Marca obj, out string Mensaje)
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
        //Eliminar Marca
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }
        //Listar Marca Por CATEGORIAS
        public List<Marca> ListarMarcaporCategoria(int idcategoria)
        {
            return objCapaDatos.ListarMarcaporCategoria(idcategoria);
        }
    }
}
