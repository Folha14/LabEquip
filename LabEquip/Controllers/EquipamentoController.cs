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
                    estadoListagem = Equipamento.EstadoEquipamento.Inativo;
                    break;
                case "3":
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
                ViewBag.Conta = _conta;
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
            // Apenas utilizadores autenticados podem criar equipamentos
            if (_conta.NivelAcesso > 0)
            {
                ViewBag.Conta = _conta;
                return View();
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpPost]
        public IActionResult Criar(Equipamento equipamento)
        {
            // Apenas utilizadores autenticados podem criar equipamentos
            if (_conta.NivelAcesso > 0)
            {
                if (ModelState.IsValid)
                {
                    HelperEquipamento helper = new HelperEquipamento();
                    helper.save(equipamento);
                    return RedirectToAction("Index", "Equipamento");
                }
                ViewBag.Conta = _conta;
                return View(equipamento);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            // Apenas utilizadores autenticados podem editar equipamentos
            if (_conta.NivelAcesso > 0)
            {
                HelperEquipamento helper = new HelperEquipamento();
                Equipamento? equipamento2Edit = helper.get(id);
                if (equipamento2Edit != null)
                {
                    ViewBag.Conta = _conta;
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
            if (_conta.NivelAcesso > 0)
            {
                if (ModelState.IsValid)
                {
                    HelperEquipamento helper = new HelperEquipamento();
                    helper.save(equipamentoPostado, id);
                    return RedirectToAction("Index", "Equipamento");
                }
                else
                {
                    ViewBag.Conta = _conta;
                    return View(equipamentoPostado);
                }
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        public IActionResult Eliminar(string id)
        {
            // Apenas administradores podem eliminar equipamentos
            if (_conta.NivelAcesso == 2)
            {
                HelperEquipamento helper = new HelperEquipamento();
                helper.delete(id);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpGet]
        public IActionResult Requisitar(string id)
        {
            // Apenas utilizadores autenticados podem requisitar equipamentos
            if (_conta.NivelAcesso > 0)
            {
                HelperEquipamento helper = new HelperEquipamento();
                Equipamento? equipamento = helper.get(id);
                if (equipamento != null && equipamento.Estado == Equipamento.EstadoEquipamento.Disponivel)
                {
                    ViewBag.Conta = _conta;
                    return View(equipamento);
                }
                else
                {
                    return RedirectToAction("Index", "Equipamento");
                }
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpPost]
        public IActionResult Requisitar(string id, Requisicao requisicao)
        {
            if (_conta.NivelAcesso > 0)
            {
                if (ModelState.IsValid)
                {
                    HelperRequisicao helperReq = new HelperRequisicao();
                    requisicao.IdEquipamento = id;
                    requisicao.IdUtilizador = _conta.Id;
                    requisicao.DataRequisicao = DateTime.Now;
                    requisicao.Estado = Requisicao.EstadoRequisicao.Ativa;

                    helperReq.save(requisicao);

                    // Atualizar estado do equipamento para "Em Uso"
                    HelperEquipamento helperEquip = new HelperEquipamento();
                    helperEquip.updateEstado(id, Equipamento.EstadoEquipamento.EmUso);

                    return RedirectToAction("Index", "Equipamento");
                }
                else
                {
                    HelperEquipamento helper = new HelperEquipamento();
                    Equipamento? equipamento = helper.get(id);
                    ViewBag.Conta = _conta;
                    return View(equipamento);
                }
            }
            return RedirectToAction("Index", "Equipamento");
        }

        public IActionResult MinhasRequisicoes()
        {
            if (_conta.NivelAcesso > 0)
            {
                HelperRequisicao helper = new HelperRequisicao();
                List<Requisicao> lista = helper.listByUser(_conta.Id);
                ViewBag.Conta = _conta;
                return View(lista);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        public IActionResult DevolverEquipamento(string id)
        {
            if (_conta.NivelAcesso > 0)
            {
                HelperRequisicao helperReq = new HelperRequisicao();
                HelperEquipamento helperEquip = new HelperEquipamento();

                // Finalizar requisição
                helperReq.finalizarRequisicao(id, _conta.Id);

                // Atualizar estado do equipamento para "Disponível"
                helperEquip.updateEstado(id, Equipamento.EstadoEquipamento.Disponivel);
            }
            return RedirectToAction("MinhasRequisicoes", "Equipamento");
        }
    }
}