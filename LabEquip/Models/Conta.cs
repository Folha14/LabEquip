namespace LabEquip.Models
{
    public class Conta
    {
        public Guid GuidConta { get; set; }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int NivelAcesso { get; set; }
        public string Senha { get; set; }

        public Conta()
        {
            GuidConta = Guid.NewGuid();
            Id = GuidConta.ToString();
            Nome = "";
            Email = "";
            NivelAcesso = 0;
            Senha = "";
        }
    }
}