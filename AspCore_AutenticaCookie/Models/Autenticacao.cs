using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//http://www.macoratti.net/18/03/aspcore_cookado1.htm

namespace AspCore_AutenticaCookie.Models
{
    public class Autenticacao : IAutenticacao
    {
        public IConfiguration Configuration { get; set; }
        //Le a string de conexão do arquivo de configuração
        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            return connectionString;
        }
        public string RegistrarUsuario(UsuarioViewModel usuario)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("RegistrarUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", usuario.Login);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                con.Open();
                string resultado = cmd.ExecuteScalar().ToString();
                con.Close();
                return resultado;
            }
        }

        public string ValidarLogin(UsuarioViewModel usuario)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ValidarUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", usuario.Login);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                con.Open();
                string resultado = cmd.ExecuteScalar().ToString();
                con.Close();
                return resultado;
            }
        }
    }
}
