using LabEquip.Models;
using Microsoft.AspNetCore.Builder;

namespace LabEquip
{
    public class Program
    {
        public static string Conetor = "";
        public static string SmtpIP = "";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(20));
            builder.Services.AddMvc();

            var config = builder.Configuration.GetSection("Configuracao").Get<Configuracao>();
            Conetor = config.Conexao;
            SmtpIP = config.SmtpIP;

            var app = builder.Build();

            app.UseSession();
            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Equipamento}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}