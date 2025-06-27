using Microsoft.AspNetCore.Mvc;
using LabEquip.Models;

namespace LabEquip.Controllers
{
    /// <summary>
    /// Controlador principal para gestão de equipamentos de laboratório.
    /// Versão simplificada baseada no PrendaController do DemoPrendas.
    /// Remove toda a complexidade de gestão de utilizadores.
    /// Herda de GenericBaseController para ter acesso à conta atual via _conta.
    /// </summary>
    public class EquipamentoController : GenericBaseController
    {
        /// <summary>
        /// Página principal - lista equipamentos filtrados por estado.
        /// Simplificado: mantém estatísticas básicas mas remove gestão de utilizadores.
        /// </summary>
        /// <param name="id">Filtro: 0=Disponível, 1=EmUso, 2=Manutenção, 3=Inativo, 99=Todos</param>
        /// <returns>View com lista de equipamentos</returns>
        public IActionResult Index(string id)
        {
            // Determina filtro baseado no parâmetro ID
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

            // Busca lista de equipamentos filtrada
            HelperEquipamento helper = new HelperEquipamento();
            List<Equipamento> lista = helper.list(estadoListagem);

            // Busca estatísticas básicas para exibir na view
            // SIMPLIFICADO: Mantém estatísticas principais, remove gestão de utilizadores
            ViewBag.NumeroEquipamentos = helper.getNrEquipamentos();
            ViewBag.TotalDisponiveis = helper.getTotalDisponiveis();
            ViewBag.TotalEmUso = helper.getTotalEmUso();
            ViewBag.TotalInativos = helper.getTotalInativos();
            ViewBag.MediaIdadeDisponiveis = helper.getMediaIdadeDisponiveis();
            ViewBag.EquipamentoMaisAntigo = helper.getEquipamentoMaisAntigo();
            ViewBag.FabricanteTop = helper.getFabricanteTop();
            ViewBag.LocalizacaoTop = helper.getLocalizacaoTop();

            // Passa conta atual para a view (para controlar links/botões)
            ViewBag.Conta = _conta;

            return View(lista);
        }

        /// <summary>
        /// Exibe detalhes de um equipamento específico.
        /// Acessível a todos os utilizadores (incluindo visitantes).
        /// </summary>
        /// <param name="id">GUID do equipamento</param>
        /// <returns>View de detalhes ou redirecionamento se não encontrar</returns>
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
                // Se não encontrou, volta para lista principal
                return RedirectToAction("Index", "Equipamento");
            }
        }

        /// <summary>
        /// Exibe formulário para criar novo equipamento.
        /// Acesso restrito: apenas utilizadores autenticados (nível > 0).
        /// </summary>
        /// <returns>View de criação ou redirecionamento se sem permissão</returns>
        [HttpGet]
        public IActionResult Criar()
        {
            // CONTROLO DE ACESSO: Nível > 0 (utilizador ou admin)
            if (_conta.NivelAcesso > 0)
            {
                return View();
            }

            // Visitantes são redirecionados para lista principal
            return RedirectToAction("Index", "Equipamento");
        }

        /// <summary>
        /// Processa criação de novo equipamento.
        /// Valida dados e grava na base de dados se válido.
        /// </summary>
        /// <param name="equipamento">Dados do equipamento do formulário</param>
        /// <returns>Redirecionamento ou view com erros</returns>
        [HttpPost]
        public IActionResult Criar(Equipamento equipamento)
        {
            // CONTROLO DE ACESSO: Mesmo controlo do GET
            if (_conta.NivelAcesso > 0)
            {
                // BOM: Valida ModelState antes de gravar
                if (ModelState.IsValid)
                {
                    HelperEquipamento helper = new HelperEquipamento();
                    helper.save(equipamento); // save() sem ID = INSERT

                    // BOM: Pattern Post-Redirect-Get
                    return RedirectToAction("Index", "Equipamento");
                }

                // Se validação falhou, retorna view com dados preenchidos
                return View(equipamento);
            }

            return RedirectToAction("Index", "Equipamento");
        }

        /// <summary>
        /// Exibe formulário para editar equipamento existente.
        /// Acesso restrito: apenas utilizadores autenticados.
        /// </summary>
        /// <param name="id">GUID do equipamento a editar</param>
        /// <returns>View de edição ou redirecionamento</returns>
        [HttpGet]
        public IActionResult Editar(string id)
        {
            if (_conta.NivelAcesso > 0)
            {
                HelperEquipamento helper = new HelperEquipamento();
                Equipamento? equipamento2Edit = helper.get(id);

                if (equipamento2Edit != null)
                {
                    // Carrega dados existentes para o formulário
                    return View(equipamento2Edit);
                }
                else
                {
                    // Se não encontrou, volta para lista
                    return RedirectToAction("Index", "Equipamento");
                }
            }
            else
            {
                // Sem permissão
                return RedirectToAction("Index", "Equipamento");
            }
        }

        /// <summary>
        /// Processa edição de equipamento existente.
        /// Atualiza dados na base de dados se validação passar.
        /// </summary>
        /// <param name="id">GUID do equipamento</param>
        /// <param name="equipamentoPostado">Dados atualizados do formulário</param>
        /// <returns>Redirecionamento ou view com erros</returns>
        [HttpPost]
        public IActionResult Editar(string id, Equipamento equipamentoPostado)
        {
            if (_conta.NivelAcesso > 0)
            {
                if (ModelState.IsValid)
                {
                    HelperEquipamento helper = new HelperEquipamento();
                    helper.save(equipamentoPostado, id); // save() com ID = UPDATE

                    return RedirectToAction("Index", "Equipamento");
                }
                else
                {
                    // Mantém dados em caso de erro de validação
                    return View(equipamentoPostado);
                }
            }
            else
            {
                return RedirectToAction("Index", "Equipamento");
            }
        }

        /// <summary>
        /// Elimina permanentemente um equipamento.
        /// Acesso restrito: apenas administradores (nível 2).
        /// NOTA: Operação destrutiva sem confirmação.
        /// </summary>
        /// <param name="id">GUID do equipamento a eliminar</param>
        /// <returns>Redirecionamento para lista</returns>
        public IActionResult Matar(string id)
        {
            // CONTROLO DE ACESSO: Apenas administradores podem eliminar
            if (_conta.NivelAcesso == 2)
            {
                HelperEquipamento helper = new HelperEquipamento();
                helper.delete(id);
            }

            // Sempre redireciona, mesmo se não tiver permissão
            // BOM: Não dá erro, simplesmente ignora ação não autorizada
            return RedirectToAction("Index", "Equipamento");
        }
    }
}