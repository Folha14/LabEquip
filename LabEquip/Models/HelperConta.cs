using System.Text.Json;

namespace LabEquip.Models
{
    public class HelperConta
    {
        public Conta setGuest()
        {
            return new Conta
            {
                GuidConta = Guid.NewGuid(),
                Nome = "Visitante",
                Email = "visitante@labequip.pt",
                NivelAcesso = 0, // Anónimo
                Senha = ""
            };
        }

        public Conta authUser(string email, string senha)
        {
            // Hardcoded users para demonstração
            if (email == "admin@labequip.pt" && senha == "admin123")
            {
                return new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Administrador",
                    Email = "admin@labequip.pt",
                    NivelAcesso = 2, // Editor (Administrador)
                    Senha = ""
                };
            }
            if (email == "user@labequip.pt" && senha == "user123")
            {
                return new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Utilizador",
                    Email = "user@labequip.pt",
                    NivelAcesso = 1, // Autor (Utilizador registado)
                    Senha = ""
                };
            }
            return setGuest();
        }

        public string serializeConta(Conta conta)
        {
            return JsonSerializer.Serialize(conta);
        }

        public Conta? deserializeConta(string json)
        {
            Conta? c;

            try
            {
                c = JsonSerializer.Deserialize<Conta>(json);
            }
            catch
            {
                c = null;
            }
            return c;
        }
    }
}