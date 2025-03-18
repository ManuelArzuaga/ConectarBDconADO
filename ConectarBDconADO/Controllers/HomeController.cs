using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using ConectarBDconADO.Models;


namespace ConectarBDconADO.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ADMarticulo admArticuloObj = new ADMarticulo();
            return View(admArticuloObj.TraerTodo());
        }

        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Alta(FormCollection collection) 
        {
            ADMarticulo objadmart = new ADMarticulo();
            
            Articulo articulo = new Articulo
            {
                Codigo = int.Parse(collection["Codigo"]),
                Descripcion = collection["Descripcion"],
                Precio = float.Parse(collection["Precio"].ToString())
            };

            objadmart.Alta(articulo);

            return RedirectToAction("Index");
        }

        public ActionResult Baja(int paramCOdigo)
        {
            ADMarticulo objadmart = new ADMarticulo();
            objadmart.Borrar(paramCOdigo);
            return RedirectToAction("Index");
        }

        public ActionResult Modificacion(int paramCodigo)
        {
            ADMarticulo objadmart = new ADMarticulo();
            Articulo articulo = objadmart.TraerArticulo(paramCodigo);

            return View(articulo);
        }


        [HttpPost]

        public ActionResult Modificacion(FormCollection collection)
        {
            ADMarticulo objadmart = new ADMarticulo();

            Articulo articulo = new Articulo
            {
                Codigo = int.Parse(collection["Codigo"].ToString()),
                Descripcion = collection["Descripcion"].ToString(),
                Precio = float.Parse(collection["Precio"].ToString())
            };

            objadmart.Modificar(articulo);

            return RedirectToAction("Index");
        }

    }
}