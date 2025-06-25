using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LabEquip.Models;

namespace LabEquip.Controllers
{
    public class GenericBaseController : Controller
    {
        //protected string _nivelAcesso = "0";
        protected Conta? _conta;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //_nivelAcesso = HttpContext.Session.GetString("contaAcesso") ?? "0";
            HelperConta helperConta = new HelperConta();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("contaAcesso")))
            {
                //Se vier aqui estou a entrar no site pela primeira vez
                HttpContext.Session.SetString("contaAcesso", helperConta.serializeConta(helperConta.setGuest()));
            }
            // Havendo ou não uma sessão, aqui já tenho uma conta válida em Session
            _conta = helperConta.deserializeConta(HttpContext.Session.GetString("contaAcesso") ?? string.Empty);
        }
    }
}