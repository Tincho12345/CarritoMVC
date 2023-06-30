using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDatos = new CD_Reporte();
        //Reporte de Ventas
        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objCapaDatos.Ventas(fechainicio, fechafin, idtransaccion);
        }

        //Actualiza Tarjetas del Dashboard
        public DashBoard VerDashBoard()
        {
            return objCapaDatos.VerDashBoard();
        }
    }
}
