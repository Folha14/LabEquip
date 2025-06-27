using System.Text.Json;

namespace LabEquip.Models
{
    /// <summary>
    /// Classe responsável pela autenticação e serialização de contas.
    /// Versão simplificada - apenas contas hardcoded, sem base de dados.
    /// Baseado no modelo DemoPrendas para maior simplicidade.
    /// </summary>
    public class HelperConta
    {
        /// <summary>
        /// Cria uma conta de visitante para utilizadores não autenticados.
        /// Utilizada como fallback quando autenticação falha ou no logout.
        /// </summary>
        /// <returns>Conta com nível de acesso 0 (visitante)</returns>
        public Conta setGuest()
        {
            return new Conta
            {
                GuidConta = Guid.NewGuid(),
                Nome = "Visitante",
                Email = "visitante@labequip.pt",
                NivelAcesso = 0, // Visitante - só pode visualizar
                Senha = ""
            };
        }

        /// <summary>
        /// Autentica utilizador com base em email e senha.
        /// SIMPLIFICAÇÃO: Apenas contas hardcoded - sem complexidade de base de dados.
        /// </summary>
        /// <param name="email">Email do utilizador</param>
        /// <param name="senha">Senha em texto simples</param>
        /// <returns>Conta autenticada ou visitante se falhar</returns>
        public Conta authUser(string email, string senha)
        {
            // CONTA ADMINISTRADOR - Acesso total (criar/editar/eliminar equipamentos)
            if (email == "admin@labequip.pt" && senha == "admin123")
            {
                return new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Administrador",
                    Email = "admin@labequip.pt",
                    NivelAcesso = 2, // Admin - pode tudo
                    Senha = "" // Não retorna senha por segurança
                };
            }

            // CONTA UTILIZADOR NORMAL - Pode criar e editar, não pode eliminar
            if (email == "user@labequip.pt" && senha == "user123")
            {
                return new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Utilizador Demo",
                    Email = "user@labequip.pt",
                    NivelAcesso = 1, // Utilizador - criar/editar apenas
                    Senha = ""
                };
            }

            // Se não encontrou conta válida, retorna visitante
            // BOM: Falha de forma segura
            return setGuest();
        }

        /// <summary>
        /// Serializa conta para JSON para armazenamento em sessão.
        /// Utilizada para manter estado da conta durante a sessão do utilizador.
        /// </summary>
        /// <param name="conta">Conta a serializar</param>
        /// <returns>String JSON da conta</returns>
        public string serializeConta(Conta conta)
        {
            // BOM: Usa System.Text.Json que é mais eficiente e seguro
            return JsonSerializer.Serialize(conta);
        }

        /// <summary>
        /// Deserializa conta a partir de JSON da sessão.
        /// Inclui tratamento de erros para JSON inválido ou corrompido.
        /// </summary>
        /// <param name="json">String JSON da conta</param>
        /// <returns>Conta deserializada ou null se erro</returns>
        public Conta? deserializeConta(string json)
        {
            Conta? c;

            try
            {
                // Tenta deserializar JSON para objeto Conta
                c = JsonSerializer.Deserialize<Conta>(json);
            }
            catch
            {
                // Se der erro na deserialização, retorna null
                // MELHORIA: Poderia fazer log do erro
                c = null;
            }
            return c;
        }
    }
}