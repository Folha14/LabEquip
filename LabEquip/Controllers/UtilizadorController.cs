using Microsoft.AspNetCore.Mvc;
using LabEquip.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabEquip.Controllers
{
    public class UtilizadorController : GenericBaseController
    {
        public IActionResult Index()
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                List<Utilizador> lista = helper.list();
                ViewBag.Conta = _conta;
                return View(lista);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        public IActionResult Detalhe(string id)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador = helper.get(id);
                if (utilizador != null)
                {
                    return View(utilizador);
                }
                else
                {
                    return RedirectToAction("Index", "Utilizador");
                }
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpGet]
        public IActionResult Criar()
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                return View();
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpPost]
        public IActionResult Criar(Utilizador utilizador)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                if (ModelState.IsValid)
                {
                    HelperUtilizador helper = new HelperUtilizador();
                    helper.save(utilizador);
                    return RedirectToAction("Index", "Utilizador");
                }
                return View(utilizador);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador2Edit = helper.get(id);
                if (utilizador2Edit != null)
                {
                    return View(utilizador2Edit);
                }
                else
                {
                    return RedirectToAction("Index", "Utilizador");
                }
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        [HttpPost]
        public IActionResult Editar(string id, Utilizador utilizadorPostado)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                if (ModelState.IsValid)
                {
                    HelperUtilizador helper = new HelperUtilizador();
                    helper.save(utilizadorPostado, id);
                    return RedirectToAction("Index", "Utilizador");
                }
                else
                {
                    return View(utilizadorPostado);
                }
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        public IActionResult Matar(string id)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                helper.delete(id);
            }
            return RedirectToAction("Index", "Utilizador");
        }

        public IActionResult Ativar(string id)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                helper.alterarEstado(id, true);
            }
            return RedirectToAction("Index", "Utilizador");
        }

        public IActionResult Desativar(string id)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                helper.alterarEstado(id, false);
            }
            return RedirectToAction("Index", "Utilizador");
        }

        public IActionResult Promover(string id)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador = helper.get(id);
                if (utilizador != null && utilizador.NivelAcesso < 2)
                {
                    helper.alterarNivelAcesso(id, utilizador.NivelAcesso + 1);
                }
            }
            return RedirectToAction("Index", "Utilizador");
        }

        public IActionResult Despromover(string id)
        {
            if (_conta.NivelAcesso == 2) // Só administrador
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador = helper.get(id);
                if (utilizador != null && utilizador.NivelAcesso > 0)
                {
                    helper.alterarNivelAcesso(id, utilizador.NivelAcesso - 1);
                }
            }
            return RedirectToAction("Index", "Utilizador");
        }
    }
}