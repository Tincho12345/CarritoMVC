using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objCapaDatos = new CD_Venta();
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objCapaDatos.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public List<DetalleVenta> ListarCompras(int idcliente)
        {
            return objCapaDatos.ListarCompras(idcliente);
        }

    }
}
