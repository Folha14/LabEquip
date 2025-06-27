using LabEquip.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabEquip.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação e gestão de sessões.
    /// Versão simplificada baseada no DemoPrendas - sem gestão de utilizadores.
    /// </summary>
    public class ContaController : Controller
    {
        /// <summary>
        /// Exibe o formulário de login.
        /// Método GET que apresenta a página de autenticação.
        /// </summary>
        /// <returns>View do formulário de login</returns>
        [HttpGet]
        public IActionResult Login()
        {
            // BOM: Método GET simples, apenas retorna a view
            return View();
        }

        /// <summary>
        /// Processa o formulário de login submetido.
        /// Autentica utilizador e cria sessão se credenciais válidas.
        /// </summary>
        /// <param name="contaEnviada">Dados de login (email/senha) do formulário</param>
        /// <returns>Redirecionamento para página principal após autenticação</returns>
        [HttpPost]
        public IActionResult Login(Conta contaEnviada)
        {
            HelperConta helper = new HelperConta();

            // Autentica utilizador e serializa resultado para sessão
            // BOM: Usa helper para encapsular lógica de autenticação
            // A autenticação pode retornar conta válida ou conta de visitante
            HttpContext.Session.SetString("contaAcesso",
                helper.serializeConta(helper.authUser(contaEnviada.Email, contaEnviada.Senha)));

            // BOM: Redireciona para página principal após login
            return RedirectToAction("Index", "Equipamento");
        }

        /// <summary>
        /// Termina a sessão do utilizador (logout).
        /// Define sessão como conta de visitante.
        /// </summary>
        /// <returns>Redirecionamento para página principal como visitante</returns>
        public IActionResult Logout()
        {
            HelperConta helper = new HelperConta();

            // Define sessão como conta de visitante (nível 0)
            // BOM: Logout seguro - não remove sessão, apenas define como visitante
            HttpContext.Session.SetString("contaAcesso",
                helper.serializeConta(helper.setGuest()));

            return RedirectToAction("Index", "Equipamento");
        }
    }
}