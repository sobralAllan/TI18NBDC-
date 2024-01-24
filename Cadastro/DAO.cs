using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Cadastro
{
    class DAO
    {
        public MySqlConnection conexao;//Conectar ao banco de dados
        public string dados;
        public string sql;
        public string resultado;
        public DAO()
        {
            conexao = new MySqlConnection("server=localhost;DataBase=ti18nPessoa;Uid=root;Password=");
            try
            {
                conexao.Open();//Abrindo a conexão com o BD
                Console.WriteLine("Conectado com sucesso!");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Algo deu errado! Verifique os dados de conexão!\n\n" + erro);
                conexao.Close();//Fechar a conexão com o BD
            }//fim do try catch
        }//fim do método

        //Método Inserir
        public void Inserir(string nome, string telefone, string cidade, string endereco)
        {
            try
            {
                dados = "('','" + nome + "','" + telefone + "','" + cidade + "','" + endereco + "')";
                sql = "insert into pessoa(codigo, nome, telefone, cidade, endereco) values" + dados;

                MySqlCommand conn = new MySqlCommand(sql, conexao);//Prepara a execução no banco
                resultado = "" + conn.ExecuteNonQuery();//Ctrl + Enter -> Executando o comando no bd
                Console.WriteLine(resultado + "Linha afetada");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro!!! Algo deu errado!\n\n\n" + erro);
            }
        }//fim do método inserir



    }//fim da classe
}//fim do projeto
