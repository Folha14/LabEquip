using System;

namespace LabEquip.Models
{
    public class Conta
    {
        public Guid guidConta { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public int nivelAcesso { get; set; }
        public string senha { get; set; }
    }
}