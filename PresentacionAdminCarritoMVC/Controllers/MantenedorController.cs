using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentacionAdminCarritoMVC.Controllers
{
    [Authorize]
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }
        //++++++++CATEGORIAS++++++++//

        #region CATEGORIAS
        // GET: Mantenedor Listar Categorias
        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        //POST: Mantenedor Guardar Categorias
        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        //POST: Mantenedor Eliminar Categorias
        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Categoria().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //++++++++MARCAS++++++++//

        #region MARCAS
        // GET: Mantenedor Listar Marcas
        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        //POST: Mantenedor Guardar Marcas
        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        //POST: Mantenedor Eliminar Marcas
        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Marca().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //++++++++PRODUCTO++++++++//
        #region PRODUCTO
        // GET: Mantenedor Listar Prodcutos
        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProducto1()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar1();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        //POST: Mantenedor Guardar Producto
        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);
            //decimal precio;
            //if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            //{
            //    oProducto.Precio = precio;
            //}
            //else
            //{
            //    return Json(new { operacion_exitosa = false, mensaje = "El Formato del Precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            //};
            string mensaje;
            if (oProducto.IdProducto == 0)
            {
                int idProductoGenerado = new CN_Producto().Registrar(oProducto, out mensaje);
                if (idProductoGenerado != 0)
                {
                    oProducto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                operacion_exitosa = new CN_Producto().Editar(oProducto, out mensaje);
            }
            //Si la operación fué exitosa y se modificó la imágen
            if (operacion_exitosa && archivoImagen != null)
            {
                //string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                string extension = Path.GetExtension(archivoImagen.FileName);
                //string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(), extension);

                MemoryStream ms = new MemoryStream();

                archivoImagen.InputStream.CopyTo(ms);
                byte[] data = ms.ToArray();

                //try
                //{
                //    archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                //}
                //catch (Exception ex)
                //{
                //    string msg = ex.Message;
                //    guardar_imagen_exito = false;
                //}
                if (guardar_imagen_exito)
                {
                    //oProducto.RutaImagen = ruta_guardar;
                    oProducto.Archivo = data;
                    //oProducto.Extension = extension;
                    //oProducto.NombreImagen = nombre_imagen;
                    oProducto.Extension = extension;
                    bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                }
                else
                {
                    mensaje = "Se guardó el Producto pero hubo problemas con la imagen";
                }
            }
            return Json(new { operacion_exitosa = operacion_exitosa, idGenerado = oProducto.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        //POST: Guardar Imágen
        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            Producto oproducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();
            string textoBase64 = Convert.ToBase64String(oproducto.Archivo);

            return Json(new
            {
                conversion = true,
                textobase64 = textoBase64,
                //extension = Path.GetExtension(oproducto.NombreImagen)
            },
            JsonRequestBehavior.AllowGet
            );
        }

        //POST: Mantenedor Eliminar Producto
        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Producto().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}