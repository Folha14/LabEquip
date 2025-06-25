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
                NivelAcesso = 0,
                Senha = ""
            };
        }

        public Conta authUser(string email, string senha)
        {
            if (email == "admin@labequip.pt" && senha == "admin123")
            {
                var conta = new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Administrador",
                    Email = "admin@labequip.pt",
                    NivelAcesso = 2,
                    Senha = ""
                };
                conta.Id = conta.GuidConta.ToString();
                return conta;
            }
            if (email == "user@labequip.pt" && senha == "user123")
            {
                var conta = new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Utilizador",
                    Email = "user@labequip.pt",
                    NivelAcesso = 1,
                    Senha = ""
                };
                conta.Id = conta.GuidConta.ToString();
                return conta;
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
                if (c != null && string.IsNullOrEmpty(c.Id))
                {
                    c.Id = c.GuidConta.ToString();
                }
            }
            catch
            {
                c = null;
            }
            return c;
        }
    }
}