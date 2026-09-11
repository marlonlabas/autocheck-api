using System;
using System.Collections.Generic;
using System.Globalization;
using AutoCheck.Domain;
using AutoCheck.ConsoleApp.Services;

namespace AutoCheck.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            MotorVistoria motor = new MotorVistoria();
            List<Veiculo> vistorias = new List<Veiculo>();

            bool continuarExecutando = true;

            do
            {
                ExibirMenu();
                string opcaoEscolhida = Console.ReadLine();

                if (opcaoEscolhida == "1")
                {
                    RealizarNovaVistoria(vistorias);
                }
                else if (opcaoEscolhida == "2")
                {
                    ExibirRelatorioVistorias(vistorias, motor);
                }
                else if (opcaoEscolhida == "0")
                {
                    continuarExecutando = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Encerrando o AutoCheck. Até a próxima!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }

            } while (continuarExecutando);
        }

        static void ExibirMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(".......................................................................................................");
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine("==================                   AUTOCHECK .NET - MOTOR DE VISTORIA              ==================");
            Console.WriteLine("=======================================================================================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1 - Realizar Nova Vistoria");
            Console.WriteLine("2 - Exibir Relatório das Vistorias");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Escolha uma opção: ");
            Console.ResetColor();
        }

        static string SolicitarTipoVeiculo()
        {
            string tipo = "";
            bool tipoValido = false;

            while (!tipoValido)
            {
                Console.WriteLine("Qual o tipo de veículo? (1-Carro, 2-Moto, 3-Caminhão)");
                tipo = Console.ReadLine();

                if (tipo == "1" || tipo == "2" || tipo == "3")
                {
                    tipoValido = true;
                }
                else
                {
                    Console.WriteLine("Opção inválida. Digite 1, 2 ou 3.");
                }
            }

            return tipo;
        }
        static void RealizarNovaVistoria(List<Veiculo> vistorias)
        {
            Console.WriteLine();
            string tipoEscolhido = SolicitarTipoVeiculo();

            Console.Write("Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Ano: ");
            int ano = int.Parse(Console.ReadLine());

            Console.Write("Quilometragem: ");
            double quilometragem = double.Parse(Console.ReadLine());

            Veiculo veiculoNovo = null;

            if (tipoEscolhido == "1")
            {
                Console.Write("Quantidade de portas: ");
                int quantidadePortas = int.Parse(Console.ReadLine());
                veiculoNovo = new Carro(marca, modelo, ano, quilometragem, quantidadePortas);
            }
            else if (tipoEscolhido == "2")
            {
                Console.Write("Cilindradas: ");
                int cilindradas = int.Parse(Console.ReadLine());
                veiculoNovo = new Moto(marca, modelo, ano, quilometragem, cilindradas);
            }
            else if (tipoEscolhido == "3")
            {
                Console.Write("Quantidade de eixos: ");
                int quantidadeEixos = int.Parse(Console.ReadLine());
                Console.Write("Capacidade de carga (toneladas): ");
                double capacidadeCarga = double.Parse(Console.ReadLine());
                veiculoNovo = new Caminhao(marca, modelo, ano, quilometragem, quantidadeEixos, capacidadeCarga);
            }

            Console.WriteLine();
            Console.WriteLine("Agora vamos preencher o checklist de vistoria.");

            List<string> checklist = veiculoNovo.ObterChecklistObrigatorio();

            foreach (string nomeItem in checklist)
            {
                string status = SolicitarStatusItem(nomeItem);
                veiculoNovo.AdicionarItemVistoriado(nomeItem, status);
            }

            vistorias.Add(veiculoNovo);

            Console.WriteLine();
            Console.WriteLine("Vistoria registrada com sucesso!");
        }

        static string SolicitarStatusItem(string nomeItem)
        {
            while (true)
            {
                Console.Write($"Status do item '{nomeItem}' (BOM/REGULAR/RUIM/AUSENTE): ");
                string status = Console.ReadLine().ToUpper().Trim();

                if (status.Equals("BOM", StringComparison.OrdinalIgnoreCase) || status.Equals("REGULAR", StringComparison.OrdinalIgnoreCase) || status.Equals("RUIM", StringComparison.OrdinalIgnoreCase) || status.Equals("AUSENTE", StringComparison.OrdinalIgnoreCase))
                {
                    return status;
                }
                else
                {
                    Console.WriteLine("Status inválido. Por favor, digite 'BOM', 'REGULAR', 'RUIM' ou 'AUSENTE'.");
                }
            }
        }

        static void ExibirRelatorioVistorias(List<Veiculo> vistorias, MotorVistoria motor)
        {
            Console.WriteLine();

            if (vistorias.Count == 0)
            {
                Console.WriteLine("Nenhuma vistoria realizada até o momento.");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(".......................................................................................................");
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine("========================                   RELATÓRIO DE VISTORIA              =========================");
            Console.WriteLine("=======================================================================================================");
            Console.ResetColor();

            int numeroAtual = 1;

            foreach (Veiculo veiculo in vistorias)
            {
                ImprimirRelatorioVeiculo(veiculo, motor, numeroAtual, vistorias.Count);
                numeroAtual = numeroAtual + 1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine("====================                   FIM DO RELATÓRIO DE VISTORIAS               ====================");
            Console.WriteLine("=======================================================================================================");
            Console.WriteLine(".......................................................................................................");
            Console.ResetColor();
        }

        static void ImprimirRelatorioVeiculo(Veiculo veiculo, MotorVistoria motor, int numeroAtual, int totalVistorias)
        {
            int pontuacaoObtida = motor.CalcularPontuacaoObtida(veiculo.VistoriaRealizada);
            int pontuacaoMaxima = motor.CalcularPontuacaoMaxima(veiculo.VistoriaRealizada);
            double percentual = motor.CalcularPercentual(pontuacaoObtida, pontuacaoMaxima);
            string classificacao = motor.ClassificarVeiculo(percentual);

            List<ItemVistoria> itensCriticos = motor.ObterItensCriticos(veiculo.VistoriaRealizada);
            List<ItemVistoria> itensAtencao = motor.ObterItensAtencao(veiculo.VistoriaRealizada);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"[{numeroAtual}/{totalVistorias}] PROCESSANDO VISTORIA");
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("> DADOS DO VEÍCULO:");
            Console.WriteLine($"  - Tipo: {veiculo.ObterTipoDescricao()}");
            Console.WriteLine($"  - Modelo: {veiculo.Marca} {veiculo.Modelo}");
            Console.WriteLine($"  - Ano: {veiculo.Ano} | Quilometragem: {veiculo.Quilometragem:N0} km");
            Console.WriteLine($"  - Atributo Específico: {veiculo.ObterAtributoEspecifico()}");
            Console.WriteLine();
            Console.WriteLine($"> AVALIAÇÃO DOS ITENS INSPECIONADOS ({veiculo.VistoriaRealizada.Count} ITENS):");

            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                string marcador = "[ ? ]";
                int pontos = 0;

                if (item.Status == "BOM")
                {
                    marcador = "[ OK ]";
                    pontos = 10;
                }
                else if (item.Status == "REGULAR")
                {
                    marcador = "[ ! ]";
                    pontos = 5;
                }
                else if (item.Status == "RUIM" || item.Status == "AUSENTE")
                {
                    marcador = "[ X ]";
                    pontos = 0;
                }

                Console.WriteLine($"  {marcador} {item.Nome.PadRight(35, '-')} Status: {item.Status} ({pontos} pts)");
            }
            Console.WriteLine();
            Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
            Console.WriteLine($"  - Pontuação Atingida: {pontuacaoObtida} de {pontuacaoMaxima} pontos possíveis");
            Console.WriteLine($"  - Percentual de Aprovação: {percentual:N1}%");
            Console.WriteLine($"  - Classificação Final: [ {classificacao.ToUpper()} ]");
            Console.WriteLine();
            Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");

            if (itensCriticos.Count == 0 && itensAtencao.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
                Console.ResetColor();
            }
            else
            {
                if (itensCriticos.Count > 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ITENS CRÍTICOS / REPROVADOS (AÇÃO IMEDIATA):");
                    List<string> textosCriticos = motor.GerarTextoRecomendacoes(itensCriticos);
                    foreach (string texto in textosCriticos)
                    {
                        Console.WriteLine("     - " + texto);
                    }
                    Console.ResetColor();
                }

                if (itensAtencao.Count > 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("ITENS DE ATENÇÃO (REVISÃO PREVENTIVA):");
                    List<string> textosAtencao = motor.GerarTextoRecomendacoes(itensAtencao);
                    foreach (string texto in textosAtencao)
                    {
                        Console.WriteLine("     - " + texto);
                    }
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
        }
    }
}