using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Ubicacion
    {
        public CD_Ubicacion objCapaDatos = new CD_Ubicacion();

        public List<Departamento> ObtenerDepartamento()
        {

            return objCapaDatos.ObtenerDepartamento();
        }

        public List<Provincia> ObtenerProvincia(string iddepartamento)
        {
            return objCapaDatos.ObtenerProvincia(iddepartamento);
        }

        public List<Distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            return objCapaDatos.ObtenerDistrito(iddepartamento, idprovincia);
        }
    }
}
