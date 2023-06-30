using CapaEntidad;
using CapaNegocio;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacionTienda.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Cliente objeto)
        {
            int resultado;
            string menssaje = string.Empty;
            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.Nombres) ? "" : objeto.Nombres;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.Apellidos) ? "" : objeto.Apellidos;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.Correo) ? "" : objeto.Correo;

            if (objeto.Clave != objeto.ConfirmarClave)
            {
                ViewBag.Error = "Las Constraseñas no Coinciden";
                return View();
            }
            resultado = new CN_Cliente().Registrar(objeto, out menssaje);
            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = menssaje;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Cliente oCliente = null;
            oCliente = new CN_Cliente().Listar().FirstOrDefault(item => item.Correo == correo &&
            item.Clave == CN_Recursos.GetSHA256(clave));

            if (oCliente == null)
            {
                ViewBag.Error = "Correo o Contraseña no son Correctos";
                return View();
            }
            else
            {
                if (oCliente.Reestablecer)
                {
                    TempData["IdCliente"] = oCliente.IdCliente;
                    return RedirectToAction("CambiarClave", "Acceso");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.Correo, false);
                    Session["Cliente"] = oCliente;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }

            }
        }
        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Cliente oCliente = new Cliente();
            oCliente = new CN_Cliente().Listar().Where(item => item.Correo == correo).FirstOrDefault();

            if (oCliente == null)
            {
                ViewBag.Error = "No Se encontró un Cliente Relacionado a ese Correo";
                return View();
            }
            else
            {
                string mensaje = string.Empty;
                bool respuesta = new CN_Cliente().ReestablecerClave(oCliente.IdCliente, correo, out mensaje);

                if (respuesta)
                {
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Acceso");
                }
                else
                {
                    ViewBag.Error = mensaje;
                    return View();
                }
            }
        }
        [HttpPost]
        public ActionResult CambiarClave(string idcliente, string claveactual, string nuevaclave, string confirmaclave)
        {
            Cliente oCliente = new Cliente();
            oCliente = new CN_Cliente().Listar().Where(u => u.IdCliente ==
            int.Parse(idcliente)).FirstOrDefault();
            if (oCliente.Clave != CN_Recursos.GetSHA256(claveactual))
            {
                TempData["Idcliente"] = idcliente;
                ViewData["vclave"] = "";
                ViewBag.Error = "La Contraseña Actual No es Correcta¡";
                return View();
            }
            else if (nuevaclave != confirmaclave)
            {
                TempData["Idcliente"] = idcliente;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las Contraseñas no coinciden¡";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = CN_Recursos.GetSHA256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().CambiarClave(int.Parse(idcliente), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Idcliente"] = idcliente;
                ViewBag.Error = mensaje;
                return View();
            }
        }
        //CERRAR SESION
        public ActionResult CerrarSesion()
        {
            Session["Cliente"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}