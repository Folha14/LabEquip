using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LabEquip.Models;

namespace LabEquip.Controllers
{
    /// <summary>
    /// Controlador base que gere automaticamente a sessão do utilizador.
    /// Versão simplificada baseada no DemoPrendas.
    /// Todos os outros controladores herdam deste para ter acesso à conta atual.
    /// </summary>
    public class GenericBaseController : Controller
    {
        /// <summary>
        /// Conta do utilizador atual (disponível em todos os controladores filhos).
        /// Pode ser: Visitante (nível 0), Utilizador (nível 1), ou Admin (nível 2).
        /// </summary>
        protected Conta? _conta;

        /// <summary>
        /// Executa automaticamente antes de cada action em controladores filhos.
        /// Garante que existe sempre uma sessão válida (visitante se necessário).
        /// VANTAGEM: Centraliza gestão de sessão num só local.
        /// </summary>
        /// <param name="context">Contexto da execução da action</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            HelperConta helperConta = new HelperConta();

            // Verifica se já existe sessão de conta
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("contaAcesso")))
            {
                // PRIMEIRA VISITA: Cria sessão de visitante automaticamente
                // BOM: Utilizador nunca fica sem sessão
                HttpContext.Session.SetString("contaAcesso",
                    helperConta.serializeConta(helperConta.setGuest()));
            }

            // Deserializa conta da sessão para usar nos controladores filhos
            // BOM: Conta sempre disponível via _conta em todos os controladores
            _conta = helperConta.deserializeConta(
                HttpContext.Session.GetString("contaAcesso") ?? string.Empty);
        }
    }
}