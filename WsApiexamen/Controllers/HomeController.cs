using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using WsApiexamen.Models;
using WsApiexamen.Models.BdiExamenModels;
using WsApiexamen.Models.Config;
using WsApiexamen.Services;

namespace WsApiexamen.Controllers
{
    public class HomeController : Controller
    {
        private IConnection _connection;
        private BdiExamenServices _Examenservices;

        public HomeController(IConnection connection)
        {
            _connection = connection;
            _Examenservices = new BdiExamenServices(_connection);
        }



        public IActionResult Index()
        {
            IEnumerable<tblExamen> lst = _Examenservices.GetAll();
            return View(lst);
        }



        public IActionResult AgregarExamen()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AgregarExamen(tblExamen oExamen)
        {

            if (oExamen.nombre != null && oExamen.descripcion != null)
            {
                _Examenservices.AgregarExamen(oExamen);
            }
            else
            {
                return View("AgregarExamen");
            }
            IEnumerable<tblExamen> lst = _Examenservices.GetAll();
            return View("Index", lst);
        }

        [HttpGet]
        public IActionResult ActualizarExamen(int id)
        {
            tblExamen otbl = new tblExamen();

            if (id != null)
            {
                otbl = _Examenservices.ConsultarExamen((int)id);
            }
            else
            {
                IEnumerable<tblExamen> lst = _Examenservices.GetAll();
                return View("Index", lst);
            }

            return View(otbl);
        }


        [HttpPost]
        public IActionResult ActualizarExamen(tblExamen otblExamen)
        {

            if (otblExamen.nombre != null && otblExamen.descripcion != null)
            {
                _Examenservices.ActualizarExamen(otblExamen);
            }
            else
            {
                return View("ActualizarExamen");
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ConsultarExamen(int id)
        {

            tblExamen otbl = new tblExamen();
            if(id != null)
            {
               otbl = _Examenservices.ConsultarExamen((int)id);
            }
            else
            {
                return View("ConsultarExamen");
            }

            return View(otbl);
        }


        [HttpGet]
        public IActionResult EliminarExamen(int id)
        {

            tblExamen otbl = new tblExamen();
          

            if (otbl.idExamen != null){
                otbl = _Examenservices.ConsultarExamen(id);
            }
            else
            {
                return RedirectToAction(nameof(Index)); 
            }

            return View(otbl);
        }


        [HttpPost]
        public IActionResult EliminarExamen(tblExamen otblExamen)
        {
            _Examenservices.EliminarExamen(otblExamen.idExamen);
            return RedirectToAction(nameof(Index));
        }




    }
}
