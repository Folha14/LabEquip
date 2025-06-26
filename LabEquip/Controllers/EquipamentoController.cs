using Microsoft.AspNetCore.Mvc;
using LabEquip.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabEquip.Controllers
{
    public class EquipamentoController : GenericBaseController
    {
        public IActionResult Index(string id)
        {
            Equipamento.EstadoEquipamento estadoListagem = Equipamento.EstadoEquipamento.Disponivel;
            switch (id)
            {
                case "0":
                    estadoListagem = Equipamento.EstadoEquipamento.Disponivel;
                    break;
                case "1":
                    estadoListagem = Equipamento.EstadoEquipamento.EmUso;
                    break;
                case "2":
                    estadoListagem = Equipamento.EstadoEquipamento.Manutencao;
                    break;
                case "3":
                    estadoListagem = Equipamento.EstadoEquipamento.Inativo;
                    break;
                case "99":
                    estadoListagem = Equipamento.EstadoEquipamento.Todos;
                    break;
                default:
                    estadoListagem = Equipamento.EstadoEquipamento.Disponivel;
                    break;
            }

            HelperEquipamento helper = new HelperEquipamento();
            List<Equipamento> lista = helper.list(estadoListagem);

            ViewBag.NumeroEquipamentos = helper.getNrEquipamentos();
            ViewBag.TotalDisponiveis = helper.getTotalDisponiveis();
            ViewBag.TotalEmUso = helper.getTotalEmUso();
            ViewBag.TotalInativos = helper.getTotalInativos();
            ViewBag.Conta = _conta;
            return View(lista);
        }

        public IActionResult Detalhe(string id)
        {
            HelperEquipamento helper = new HelperEquipamento();
            Equipamento? equipamento = helper.get(id);
            if (equipamento != null)
            {
                return View(equipamento);
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        [HttpGet]
        public IActionResult Criar()
        {
            if (_conta.nivelAcesso > 0)
            {
                return View();
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpPost]
        public IActionResult Criar(Equipamento equipamento)
        {
            if (_conta.nivelAcesso > 0)
            {
                if (ModelState.IsValid)
                {
                    HelperEquipamento helper = new HelperEquipamento();
                    helper.save(equipamento);
                    return RedirectToAction("Index", "Equipamento");
                }
                return View(equipamento);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            if (_conta.nivelAcesso > 0)
            {
                HelperEquipamento helper = new HelperEquipamento();
                Equipamento? equipamento2Edit = helper.get(id);
                if (equipamento2Edit != null)
                {
                    return View(equipamento2Edit);
                }
                else
                {
                    return RedirectToAction("Index", "Equipamento");
                }
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        [HttpPost]
        public IActionResult Editar(string id, Equipamento equipamentoPostado)
        {
            if (_conta.nivelAcesso > 0)
            {
                if (ModelState.IsValid)
                {
                    HelperEquipamento helper = new HelperEquipamento();
                    helper.save(equipamentoPostado, id);
                    return RedirectToAction("Index", "Equipamento");
                }
                else
                {
                    return View(equipamentoPostado);
                }
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        public IActionResult Matar(string id)
        {
            if (_conta.nivelAcesso == 2)
            {
                HelperEquipamento helper = new HelperEquipamento();
                helper.delete(id);
            }
            return RedirectToAction("Index", "Equipamento");
        }
    }
}