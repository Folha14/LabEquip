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
            ViewBag.MediaIdadeDisponiveis = helper.getMediaIdadeDisponiveis();
            ViewBag.EquipamentoMaisAntigo = helper.getEquipamentoMaisAntigo();
            ViewBag.FabricanteTop = helper.getFabricanteTop();
            ViewBag.LocalizacaoTop = helper.getLocalizacaoTop();

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
            if (_conta.NivelAcesso > 0)
            {
                return View();
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpPost]
        public IActionResult Criar(Equipamento equipamento)
        {
            if (_conta.NivelAcesso > 0)
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

        // MÉTODO PARA ATUALIZAR SESSÃO
        private void AtualizarSessao(string guidUtilizador)
        {
            HelperUtilizador helper = new HelperUtilizador();
            Utilizador? utilizadorAtualizado = helper.get(guidUtilizador);

            if (utilizadorAtualizado != null)
            {
                Conta contaAtualizada = new Conta
                {
                    GuidConta = _conta.GuidConta,
                    Nome = utilizadorAtualizado.Nome,
                    Email = utilizadorAtualizado.Email,
                    NivelAcesso = utilizadorAtualizado.NivelAcesso,
                    Senha = ""
                };

                HelperConta helperConta = new HelperConta();
                HttpContext.Session.SetString("contaAcesso", helperConta.serializeConta(contaAtualizada));
            }
        }

        // GESTÃO DE UTILIZADORES
        public IActionResult GestaoUtilizadores()
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                List<Utilizador> lista = helper.list();

                try
                {
                    // ESTATÍSTICAS DE UTILIZADORES com tratamento de erro
                    ViewBag.TotalUtilizadoresAtivos = helper.getTotalUtilizadoresAtivos();
                    ViewBag.UtilizadorMaisRecente = helper.getUtilizadorMaisRecente();
                    ViewBag.ContagemNiveis = helper.getContagemNiveis();
                }
                catch (Exception ex)
                {
                    // Se der erro, usa valores por defeito
                    ViewBag.TotalUtilizadoresAtivos = lista.Count(u => u.Ativo);
                    ViewBag.UtilizadorMaisRecente = "Erro ao carregar";
                    ViewBag.ContagemNiveis = "Erro ao carregar";
                }

                ViewBag.Conta = _conta;
                return View(lista);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        public IActionResult DetalheUtilizador(string id)
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador = helper.get(id);
                if (utilizador != null)
                {
                    return View(utilizador);
                }
                else
                {
                    return RedirectToAction("GestaoUtilizadores", "Equipamento");
                }
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpGet]
        public IActionResult CriarUtilizador()
        {
            if (_conta.NivelAcesso == 2)
            {
                return View();
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpPost]
        public IActionResult CriarUtilizador(Utilizador utilizador)
        {
            if (_conta.NivelAcesso == 2)
            {
                if (ModelState.IsValid)
                {
                    HelperUtilizador helper = new HelperUtilizador();
                    helper.save(utilizador);
                    return RedirectToAction("GestaoUtilizadores", "Equipamento");
                }
                return View(utilizador);
            }
            return RedirectToAction("Index", "Equipamento");
        }

        [HttpGet]
        public IActionResult EditarUtilizador(string id)
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador2Edit = helper.get(id);
                if (utilizador2Edit != null)
                {
                    return View(utilizador2Edit);
                }
                else
                {
                    return RedirectToAction("GestaoUtilizadores", "Equipamento");
                }
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        [HttpPost]
        public IActionResult EditarUtilizador(string id, Utilizador utilizadorPostado)
        {
            if (_conta.NivelAcesso == 2)
            {
                if (ModelState.IsValid)
                {
                    HelperUtilizador helper = new HelperUtilizador();
                    helper.save(utilizadorPostado, id);

                    // Se editou a si próprio, atualiza a sessão
                    if (_conta.Email == utilizadorPostado.Email)
                    {
                        AtualizarSessao(id);
                    }

                    return RedirectToAction("GestaoUtilizadores", "Equipamento");
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

        public IActionResult MatarUtilizador(string id)
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                helper.delete(id);
            }
            return RedirectToAction("GestaoUtilizadores", "Equipamento");
        }

        public IActionResult AtivarUtilizador(string id)
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                helper.alterarEstado(id, true);

                // Se ativou a si próprio, atualiza a sessão
                Utilizador? utilizador = helper.get(id);
                if (utilizador != null && _conta.Email == utilizador.Email)
                {
                    AtualizarSessao(id);
                }
            }
            return RedirectToAction("GestaoUtilizadores", "Equipamento");
        }

        public IActionResult DesativarUtilizador(string id)
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador = helper.get(id);

                // Verifica se está a tentar desativar a si próprio
                if (utilizador != null && _conta.Email == utilizador.Email)
                {
                    // Se desativar a si próprio, força logout
                    helper.alterarEstado(id, false);
                    return RedirectToAction("Logout", "Conta");
                }

                helper.alterarEstado(id, false);
            }
            return RedirectToAction("GestaoUtilizadores", "Equipamento");
        }

        public IActionResult PromoverUtilizador(string id)
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador = helper.get(id);
                if (utilizador != null && utilizador.NivelAcesso < 2)
                {
                    helper.alterarNivelAcesso(id, utilizador.NivelAcesso + 1);

                    // Se promoveu a si próprio, atualiza a sessão
                    if (_conta.Email == utilizador.Email)
                    {
                        AtualizarSessao(id);
                    }
                }
            }
            return RedirectToAction("GestaoUtilizadores", "Equipamento");
        }

        public IActionResult DespromoverUtilizador(string id)
        {
            if (_conta.NivelAcesso == 2)
            {
                HelperUtilizador helper = new HelperUtilizador();
                Utilizador? utilizador = helper.get(id);
                if (utilizador != null && utilizador.NivelAcesso > 0)
                {
                    // Verifica se está a tentar despromover a si próprio de admin
                    if (_conta.Email == utilizador.Email && utilizador.NivelAcesso == 2)
                    {
                        // Se despromover a si próprio de admin, força logout
                        helper.alterarNivelAcesso(id, utilizador.NivelAcesso - 1);
                        return RedirectToAction("Logout", "Conta");
                    }

                    helper.alterarNivelAcesso(id, utilizador.NivelAcesso - 1);

                    // Se despromoveu a si próprio (mas não de admin), atualiza a sessão
                    if (_conta.Email == utilizador.Email)
                    {
                        AtualizarSessao(id);
                    }
                }
            }
            return RedirectToAction("GestaoUtilizadores", "Equipamento");
        }

    }
}