using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace BancoDeDadosTI14T
{
    class DAO
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public string resultado;
        public int i;
        public int contador;
        public string msg;
        public int[] codigo;
        public string[] nome; // vetor de nome
        public string[] telefone; //vetro de telefone
        public string[] endereco; // vetor de endereco
        public DateTime[] data; // vetor data


        public DAO()
        {
            conexao = new MySqlConnection("server=localhost;DataBase=turma14;Uid=root;Password=;Convert Zero DateTime=True");

            try
            {
                conexao.Open();//TENTANDO CONECTAR COM O BD
                Console.WriteLine("Conectado com Sucesso!"); 
               

            }catch(Exception e)
            {
                Console.WriteLine("Algo deu errado!\n\n" + e); // MOSTRAR O ERRO EM TELA                      
                conexao.Close();
            }
        } // FIM DO METODO CONSTRUTOR

        public void Inserir(string nome, string telefone, string endereco, DateTime dtNascimento)
        {
            try
            {
                //modificar a estrutura de dados
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@Date";
                parameter.MySqlDbType = MySqlDbType.Date;
                parameter.Value = dtNascimento.Year + "-" + dtNascimento.Month + "-" + dtNascimento.Day; 

                dados = "('', '" + nome + "', '" + telefone + "', '" + endereco + "', '" + parameter.Value + "')";
                comando = "Insert into Pessoa(codigo, nome, telefone, endereco, dataDeNascimento) values" + dados;

                // EXECUTAR O COMANDO DE INSERÇÃO NO BANCO DE DADOS
                MySqlCommand sql = new MySqlCommand(comando, conexao);
                resultado = "" + sql.ExecuteNonQuery(); // executa o insert no BD
                Console.WriteLine(resultado + "\nLinhas Afetadas");
            } catch(Exception e)
            {
                Console.WriteLine("Algo deu errado!\n\n" + e); // MOSTRAR O ERRO EM TELA
                Console.ReadLine(); // MANTER O PROMPT ABERTO

            }
        } // FIM DO METODO INSERIR   


        public void PreencherVetor()
        {
            string query = "select * from pessoa"; // coletar dados do BD 

            //instanciar 
            codigo   = new int[100];
            nome     = new string[100];
            telefone = new string[100];
            endereco = new string[100];
            data     = new DateTime[100];

            // preencher com valores iniciais

            for( i=0; i < 100; i++)
            {
                codigo[i]   = 0;
                nome[i]     = "";
                telefone[i] = "";
                endereco[i] = "";
                data[i]     = new DateTime();
            }

            // Criando o comando para consulta no BD 
            MySqlCommand coletar = new MySqlCommand(query, conexao);
            //Leitura de dados no banco 
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            contador = 0;
            while(leitura.Read())
            {
                codigo[i]   = Convert.ToInt32(leitura["codigo"]);
                nome[i]     = leitura["nome"] + "";
                telefone[i] = leitura["telefone"] + "";
                endereco[i] = leitura["endereco"] + "";
                data[i]     = Convert.ToDateTime(leitura["dataDeNascimento"]);
                i++;
                contador++;
            }

            // fechar a leitura no BD
            leitura.Close();

        } // fim do preenchimento do vetor 



        //Metodo que CONSULTA TODOS do BD
        public string ConsultarTudo()
        {
            PreencherVetor();
            msg = "";
            for( i = 0; i < contador; i++ )
            {
                msg += "Código: "               + codigo[i]   +
                       ", Nome: "               + nome[i]     +
                       ", Telefone: "           + telefone[i] +
                       ", Endereço: "           + endereco[i] +
                       ", Data de Nascimento: " + data[i]     +
                       "\n\n";

            } // fim for
            
            return msg;

        }//  fim do metodo CONSULTAR TUDO 

        public string ConsultarTudo(int cod)
        {
            PreencherVetor();

            for( i = 0; i < contador; i++)
            {
                if(codigo[i] == cod)
                {
                     msg += "Código: " + codigo[i]                   +
                                  ", Nome: " + nome[i]               +
                                  ", Telefone: " + telefone[i]       +
                                  ", Endereço: " + endereco[i]       +
                                  ", Data de Nascimento: " + data[i] +
                                  "\n\n";
                    return msg;
                }
            } // fim for
            return "Código Informado não encontrado!"; 
        } // fim do consultar tudo 

        public string Atualizar(int codigo, string campo, string novoDado)
        {
            try
            {
                string query = "update pessoa set " + campo + " = '" + novoDado + "' where codigo = '" + codigo + "'";
                // executar o comando 
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                return resultado + " Linha Afetada";
            }
            catch (Exception e)
            {
                return "Algo deu errado!\n\n" + e;
            }
        } //fim atualizar

        public string Deletar(int codigo)
        {
            try
            {
                string query = "delete from pessoa where codigo = '" + codigo + "'";
                // executar o comando 
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                return resultado + " Linha Afetada";
            }
            catch (Exception e)
            {
                return "Algo deu errado!\n\n" + e;
            }
        }// fim deletar
    
















    }// fim da classe
}// fim do prijeto
