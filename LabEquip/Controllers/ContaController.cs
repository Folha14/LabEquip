using LabEquip.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabEquip.Controllers
{
    public class ContaController : Controller
    {
        //public IActionResult Login() {
        //    //HttpContext.Session.SetString("nivelAcesso", "1");
        //    HelperConta helper = new HelperConta();
        //    HttpContext.Session.SetString("contaAcesso", helper.serializeConta(helper.authUser("user@labequip.pt", "user123")));
        //    return RedirectToAction("Index", "Equipamento");
        //}

        //public IActionResult LoginAdmin() {
        //    //HttpContext.Session.SetString("nivelAcesso", "2");
        //    HelperConta helper = new HelperConta();
        //    HttpContext.Session.SetString("contaAcesso", helper.serializeConta(helper.authUser("admin@labequip.pt", "admin123")));
        //    return RedirectToAction("Index", "Equipamento");
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Conta contaEnviada)
        {
            HelperConta helper = new HelperConta();
            HttpContext.Session.SetString("contaAcesso", helper.serializeConta(helper.authUser(contaEnviada.Email, contaEnviada.Senha)));
            return RedirectToAction("Index", "Equipamento");
        }

        public IActionResult Logout()
        {
            //HttpContext.Session.SetString("nivelAcesso", "0");
            HelperConta helper = new HelperConta();
            HttpContext.Session.SetString("contaAcesso", helper.serializeConta(helper.setGuest()));
            return RedirectToAction("Index", "Equipamento");
        }
    }
}