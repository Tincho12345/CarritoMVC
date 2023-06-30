
using Firebase.Auth;
using Firebase.Storage;

using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xamarin.Essentials;
using System.Threading;

namespace CapaPresentacionTienda.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> SubirStorage( Stream archivo, string nombre) {

            string email = "codigo@gmail.com";
            string clave = "codigo123";
            string ruta = "pizzas-juan-bautista.appspot.com";
            string api_key = "AIzaSyC3n6OSo8-yyZgPG8ciZbTrC9sxEIqOgFI";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("Fotos_Perfil")
                .Child(nombre)
                .PutAsync(archivo, cancellation.Token);
            var downloadURL = await task;
            return downloadURL;
        }
    }
}