using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private string StringConexao = "Data Source=.\\sqlexpress;Initial Catalog=In_Lock_Games_Manha;Persist Security Info=True;User ID=sa;Password=132";

        public void Cadastrar(JogoDomain jogo)
        {

            string insert = "INSERT INTO JOGOS(NomeJogo, Descricao, DataLancamento, Valor, EstudioId) VALUES(@NomeJogo, @Descricao, @DataLancamento, @Valor, @EstudioId)";

            using (SqlConnection con = new SqlConnection(StringConexao)) {
                con.Open();
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.Parameters.AddWithValue("@NomeJogo", jogo.NomeJogo);
                cmd.Parameters.AddWithValue("@Descricao", jogo.Descricao);
                cmd.Parameters.AddWithValue("@DataLancamento", jogo.DataLancamento);
                cmd.Parameters.AddWithValue("@Valor", jogo.Valor);
                cmd.Parameters.AddWithValue("@EstudioId", jogo.EstudioId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<JogoDomain> ListarJogos()
        {
            string Select = "SELECT * FROM JOGOS_ESTUDIO_JOIN";
            List<JogoDomain> listaJogos = new List<JogoDomain>();
        using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand(Select, con))
                {
                    SqlDataReader sqr = cmd.ExecuteReader();
                    if (sqr.HasRows)
                    {
                        while (sqr.Read())
                        {
                            JogoDomain jogo = new JogoDomain()
                            {
                                JogoId = Convert.ToInt32(sqr["JogoId"]),
                                NomeJogo = sqr["NomeJogo"].ToString(),
                                Descricao = sqr["Descricao"].ToString(),
                                DataLancamento = Convert.ToDateTime(sqr["DataLancamento"]),
                                Valor = Convert.ToDecimal(sqr["Valor"]),
                                estudio = new EstudioDomain()
                                {
                                    Id = Convert.ToInt32(sqr["EstudioId"]),
                                    Nome = sqr["Nome_Estudio"].ToString()
                                }
                            };
                            listaJogos.Add(jogo);
                        }
                    }                    
                 }
            } return listaJogos;
        }
    }
}
