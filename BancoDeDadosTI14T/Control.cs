using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDadosTI14T
{
    class Control
    {
        DAO conexao; // criando a variavel conexao
        public int opcao;
        public DateTime dtNascimento;
        public Control()
        {
            conexao = new DAO(); // Instanciando a variavel conexao 
            dtNascimento = new DateTime();
        } //  FIM DO METODO CONSTRUTUOR

        public void Menu()
        {
            Console.WriteLine("Escolha um das opções abaixo: \n\n"             +
                                       "1. Cdastrar\n"                         +
                                       "2. Consultar Tudo\n"                   +
                                       "3. Consultar por Código\n"             +
                                       "4. Atualizar\n"                        +
                                       "5. Excluir\n"                          + 
                                       "0. Sair");
            opcao = Convert.ToInt32(Console.ReadLine());
        }// fim do menu

        public void Executar()
        {
            do
            {
                Menu();

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Informe seu nome: ");
                        string nome = Console.ReadLine();
                        Console.WriteLine("Informe seu telefone: ");
                        string telefone = Console.ReadLine();
                        Console.WriteLine("Informe seu endereço: ");
                        string enreco = Console.ReadLine();
                        Console.WriteLine("Informe sua data de nascimento: ");
                        dtNascimento = Convert.ToDateTime(Console.ReadLine());
                        // utilizando esses dados no metodo inserir
                        conexao.Inserir(nome, telefone, enreco, dtNascimento);
                        break;
                    default:
                        Console.WriteLine("Código informado não é válido!");
                        break;

                    case 2:
                        Console.WriteLine(conexao.ConsultarTudo());
                        break;

                    case 3:
                        //coletando o codigo que sera pesquisado
                        Console.WriteLine("Informe o código do Cliente que deseja consultar: ");
                        int codigo = Convert.ToInt32(Console.ReadLine());
                        // mostrando o codigo em tela
                        Console.WriteLine(conexao.ConsultarTudo(codigo));
                        break;

                    case 4:
                        Console.WriteLine("Infome o campo que deseja atualizar: ");
                        string campo = Console.ReadLine();
                        Console.WriteLine("Infome o novo dado para este campo: ");
                        string novoDado = Console.ReadLine();
                        Console.WriteLine("Infome o Código do usuário que deseja atualizar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        // utilizando esses dados no metodo atualizar
                        Console.WriteLine(conexao.Atualizar(codigo, campo, novoDado));
                        break;

                    case 5:
                        // solicitr o codigo que sera apagado
                        Console.WriteLine("Infome o código que deseja deletar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        // mostrar o resultado em tela
                        Console.WriteLine(conexao.Deletar(codigo));
                        break;
                }// fim switch
                    Console.WriteLine("\n\n");
            } while (opcao != 0);
        } // fim do executar
    }// FIM DA CLASSE
}// FIM DO PROJETO
