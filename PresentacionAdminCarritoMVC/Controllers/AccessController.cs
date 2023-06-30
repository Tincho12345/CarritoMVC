using CapaEntidad;
using CapaNegocio;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentacionAdminCarritoMVC.Controllers
{
    public class AccessController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave ==
            CN_Recursos.GetSHA256(clave)).FirstOrDefault();
            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o Contraseña Inválidos";
                return View();
            }
            else
            {
                if (oUsuario.Reestablecer)
                {
                    //Como el Método está en este controlador no es NECESARIO PONERLO
                    TempData["IdUsuario"] = oUsuario.IdUsuario;
                    return RedirectToAction("CambiarClave");
                }
                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false);
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(item => item.Correo == correo).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "No Se encontró un Usuario Relacionado a ese Correo";
                return View();
            }
            else
            {
                string mensaje = string.Empty;
                bool respuesta = new CN_Usuarios().ReestablecerClave(oUsuario.IdUsuario, correo, out mensaje);

                if (respuesta)
                {
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Access");
                }
                else
                {
                    ViewBag.Error = mensaje;
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.IdUsuario ==
            int.Parse(idusuario)).FirstOrDefault();
            if (oUsuario.Clave != CN_Recursos.GetSHA256(claveactual))
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La Contraseña Actual No es Correcta¡";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las Contraseñas no coinciden¡";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = CN_Recursos.GetSHA256(nuevaclave);
            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdUsuario"] = idusuario;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Access");
        }
    }
}