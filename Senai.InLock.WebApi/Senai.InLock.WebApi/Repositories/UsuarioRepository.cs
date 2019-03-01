using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string StringConexao = "Data Source=.\\sqlexpress;Initial Catalog=In_Lock_Games_Manha;Persist Security Info=True;User ID=sa;Password=132";

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Buscar = "SELECT * FROM USUARIOS WHERE USUARIOS.Email = @Email and USUARIOS.Senha = @Senha";
                using(SqlCommand cmd = new SqlCommand(Buscar, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);
                    con.Open();

                    SqlDataReader sqr = cmd.ExecuteReader();

                    if (sqr.HasRows)
                    {
                        UsuarioDomain usuario = new UsuarioDomain();
                        while (sqr.Read())
                        {
                            usuario.UsuarioId = Convert.ToInt32(sqr["UsuarioId"]);
                            usuario.Email = sqr["Email"].ToString();
                            usuario.Senha = sqr["Senha"].ToString();
                            usuario.TipoUsuario = sqr["TipoUsuario"].ToString();

                        }
                        return usuario;
                    }
                }
                return null;
            }
        }
    }
}
