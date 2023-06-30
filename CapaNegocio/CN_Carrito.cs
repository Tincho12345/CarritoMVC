using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Carrito
    {
        private CD_Carrito objCapaDatos = new CD_Carrito();

        public bool ExisteCarrito(int idcliente, int idproducto)
        {
            return objCapaDatos.ExisteCarrito(idcliente, idproducto);
        }

        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string Mensaje)
        {
            return objCapaDatos.OperacionCarrito(idcliente, idproducto, sumar, out Mensaje);
        }

        public int CantidadEnCarrito(int idcliente)
        {
            return objCapaDatos.CantidadEnCarrito(idcliente);
        }

        public List<Carrito> ListarProducto(int idcliente)
        {
            return objCapaDatos.ListarProducto(idcliente);
        }

        public bool EliminarCarrito(int idcliente, int idproducto)
        {
            return objCapaDatos.EliminarCarrito(idcliente, idproducto);
        }
    }
}
