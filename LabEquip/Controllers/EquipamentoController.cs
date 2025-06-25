using Microsoft.AspNetCore.Mvc;
using LabEquip.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabEquip.Controllers
{
    public class EquipamentoController : GenericBaseController
    {
        //private string _nivelAcesso = "0";

        //public override void OnActionExecuting(ActionExecutingContext context) {
        //    base.OnActionExecuting(context);
        //    _nivelAcesso = HttpContext.Session.GetString("nivelAcesso") ?? "0";
        //}

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
            //ViewBag.NivelAcesso = HttpContext.Session.GetString("nivelAcesso") ?? "0";
            ViewBag.Conta = _conta;
            return View(lista);
        }

        public IActionResult Detalhe(string id)
        {
            //var equipamento = Program._equipamentos.Find(p => p.GuidEquipamento == id);
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
            //string nivelAcesso = HttpContext.Session.GetString("nivelAcesso") ?? "0";
            //if (_nivelAcesso != "0") {
            if (_conta.NivelAcesso > 0)
            {
                return View();
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpPost]
        public IActionResult Criar(Equipamento equipamento)
        {
            //string nivelAcesso = HttpContext.Session.GetString("nivelAcesso") ?? "0";
            //if (_nivelAcesso != "0") {
            if (_conta.NivelAcesso > 0)
            {
                if (ModelState.IsValid)
                {
                    //Program._equipamentos.Add(equipamento);
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
            //var equipamento2Edit = Program._equipamentos.Find(p => p.GuidEquipamento == id);
            if (_conta.NivelAcesso > 0)
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
            if (_conta.NivelAcesso > 0)
            {
                HelperEquipamento helper = new HelperEquipamento();
                Equipamento? equipamento = helper.get(id);
                if (equipamento != null && equipamento.Estado == Equipamento.EstadoEquipamento.Disponivel)
                {
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
                    requisicao.IdUtilizador = _conta.GuidConta.ToString();
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
                List<Requisicao> lista = helper.listByUser(_conta.GuidConta.ToString());
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
                helperReq.finalizarRequisicao(id, _conta.GuidConta.ToString());

                // Atualizar estado do equipamento para "Disponível"
                helperEquip.updateEstado(id, Equipamento.EstadoEquipamento.Disponivel);
            }
            return RedirectToAction("MinhasRequisicoes", "Equipamento");
        }
    }
}