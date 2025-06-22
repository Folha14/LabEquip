namespace LabEquip.Models
{
    public class Conta
    {
        public Guid GuidConta { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int NivelAcesso { get; set; } // 0=Anónimo, 1=Autor, 2=Editor
        public string Senha { get; set; }
    }
}