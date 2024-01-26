﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
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
        public int[] codigo;
        public string[] nome;
        public string[] telefone;
        public string[] cidade;
        public string[] endereco;
        public int i;
        public int contador;
        public string msg;
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

        //Método consultar
        public void PreencherVetor()
        {
            string query = "select * from pessoa";

            //Instaciar os vetores
            codigo   = new int[100];
            nome     = new string[100];
            telefone = new string[100];
            cidade   = new string[100];
            endereco = new string[100];

            //Preencher com valores genéricos
            for(i = 0; i < 100; i++)
            {
                codigo[i] = 0;
                nome[i] = "";
                telefone[i] = "";
                cidade[i] = "";
                endereco[i] = "";
            }//fim do for

            //Preparando o comando para o banco
            MySqlCommand coletar = new MySqlCommand(query, conexao);
            //Leitura do banco
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            contador = 0;
            while (leitura.Read())
            {
                codigo[i] = Convert.ToInt32(leitura["codigo"]);
                nome[i] = "" + leitura["nome"];
                telefone[i] = "" + leitura["telefone"];
                cidade[i] = "" + leitura["cidade"];
                endereco[i] = "" + leitura["endereco"];
                i++;
                contador++;
            }//Preenchendo o vetor com os dados do banco

            leitura.Close();//Encerrar o acesso ao Banco de Dados
        }//fim do preencher

        //Método para consultar TODOS os dados do banco de dados
        public string ConsultarTudo()
        {
            //Preencher o vetor
            PreencherVetor();
            msg = "";
            for(i=0;i < contador; i++)
            {
                msg += "\n\nCódigo: " + codigo[i]     +
                       ", Nome: "     + nome[i]       +
                       ", Telefone: " + telefone[i]   +
                       ", Cidade: "   + cidade[i]     +
                       ", Endereço: " + endereco[i];
            }//fim do for

            return msg;//Mostrar na tela o resultado da consulta
        }//fim do método

        public string ConsultarTudo(int cod)
        {
            PreencherVetor();

            for(i=0; i < contador; i++)
            {
                if (codigo[i] == cod)
                {
                    msg = "\n\nCódigo: "    + codigo[i]   +
                          ", Nome: "        + nome[i]     +
                          ", Telefone: "    + telefone[i] +
                          ", Cidade: "      + cidade[i]   +
                          ", Endereço: "    + endereco[i];
                    return msg;
                }//fim do if
            }//fim do for
            return "Código informado não encontrado!";
        }//fim do método

        public string Atualizar(int cod, string campo, string dado)
        {
            try
            {
                string query = "update pessoa set " + campo + " = '" + dado + "' where codigo = '" + cod + "'";
                //Preparar o comando do BD
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                return resultado + " linha afetada!";
            }catch(Exception erro)
            {
                return "Algo deu errado!\n\n" + erro;
            }
        }//fim do método

        public string Excluir(int cod)
        {
            string query = "delete from pessoa where codigo = '" + cod + "'";
            //Preparar o comando
            MySqlCommand sql = new MySqlCommand(query, conexao);
            string resultado = "" + sql.ExecuteNonQuery();
            //Mostrar o resultado
            return resultado + " Linha Afetada";
        }//fim do método
    }//fim da classe
}//fim do projeto
