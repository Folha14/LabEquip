using LabEquip.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabEquip.Controllers
{
    public class ContaController : Controller
    {
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
            HelperConta helper = new HelperConta();
            HttpContext.Session.SetString("contaAcesso", helper.serializeConta(helper.setGuest()));
            return RedirectToAction("Index", "Equipamento");
        }
    }
}