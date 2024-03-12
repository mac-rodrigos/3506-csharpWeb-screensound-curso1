using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.DataBase
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM ARTISTAS";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader datareader = command.ExecuteReader();

            while (datareader.Read())
            {
                string nomeArtista = Convert.ToString(datareader["Nome"]);
                string bioArtista = Convert.ToString(datareader["Bio"]);
                int idArtista = Convert.ToInt32(datareader["id"]);

                Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };
                lista.Add(artista);

            }
            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"Linhas afetadas: {retorno}");
            Console.ResetColor();


        }
    }
}
