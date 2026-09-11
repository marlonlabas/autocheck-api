using System.Collections.Generic;
using AutoCheck.Domain;

namespace AutoCheck.ConsoleApp.Services
{
    public class MotorVistoria
    {
        public int CalcularPontuacaoObtida(List<ItemVistoria> itens)
        {
            int pontuacaoTotal = 0;

            foreach (ItemVistoria item in itens)
            {
                if (item.Status == "BOM")
                {
                    pontuacaoTotal = pontuacaoTotal + 10;
                }
                else if (item.Status == "REGULAR")
                {
                    pontuacaoTotal = pontuacaoTotal + 5;
                }
                else if (item.Status == "RUIM" || item.Status == "AUSENTE")
                {
                    pontuacaoTotal = pontuacaoTotal + 0;
                }
            }

            return pontuacaoTotal;
        }

        public int CalcularPontuacaoMaxima(List<ItemVistoria> itens)
        {
            return itens.Count * 10;
        }

        public double CalcularPercentual(int pontuacaoObtida, int pontuacaoMaxima)
        {
            double percentual = (double)pontuacaoObtida / (double)pontuacaoMaxima * 100;
            return percentual;
        }

        public string ClassificarVeiculo(double percentual)
        {
            string classificacao;

            if (percentual >= 90)
            {
                classificacao = "Aprovado com Excelência";
            }
            else if (percentual >= 60)
            {
                classificacao = "Aprovado com Apontamentos";
            }
            else
            {
                classificacao = "Reprovado na Vistoria";
            }

            return classificacao;
        }

        public List<ItemVistoria> ObterItensCriticos(List<ItemVistoria> itens)
        {
            List<ItemVistoria> criticos = new List<ItemVistoria>();

            foreach (ItemVistoria item in itens)
            {
                if (item.Status == "RUIM" || item.Status == "AUSENTE")
                {
                    criticos.Add(item);
                }
            }
            return criticos;
        }

        public List<ItemVistoria> ObterItensAtencao(List<ItemVistoria> itens)
        {
            List<ItemVistoria> atencao = new List<ItemVistoria>();

            foreach (ItemVistoria item in itens)
            {
                if (item.Status == "REGULAR")
                {
                    atencao.Add(item);
                }
            }
            return atencao;
        }

        public string ObterRecomendacaoParaItem(string nomeItem)
        {
            string recomendacao;

            switch (nomeItem)
            {
                case "Nível de Óleo do Motor":
                    recomendacao = "Realizar troca de óleo e filtro.";
                    break;
                case "Bateria e Sistema Elétrico":
                    recomendacao = "Testar carga da bateria e revisar fiação elétrica.";
                    break;
                case "Documentação Regularizada":
                    recomendacao = "Regularizar documentação (licenciamento/IPVA) antes da venda.";
                    break;
                case "Condições dos Pneus":
                    recomendacao = "Verificar pressão e desgaste dos pneus.";
                    break;
                case "Estepe e Macaco":
                    recomendacao = "Calibrar pneu reserva e verificar funcionamento do macaco.";
                    break;
                case "Triângulo de Sinalização":
                    recomendacao = "Repor equipamento obrigatório ausente/danificado.";
                    break;
                case "Ar Condicionado Funcional":
                    recomendacao = "Realizar higienização e checagem do gás refrigerante.";
                    break;
                case "Kit Transmissão/Corrente":
                    recomendacao = "Trocar kit relação (corrente, coroa e pinhão).";
                    break;
                case "Manetes de Freio/Embreagem":
                    recomendacao = "Ajustar ou substituir manetes de freio e embreagem.";
                    break;
                case "Pezinho Lateral":
                    recomendacao = "Reparar ou substituir o pezinho lateral (cavalete).";
                    break;
                case "Tacógrafo":
                    recomendacao = "Calibrar e aferir o tacógrafo conforme legislação.";
                    break;
                case "Sistema de Freios a Ar":
                    recomendacao = "Revisar sistema pneumático de freios (compressor, válvulas, mangueiras).";
                    break;
                case "Trava e Lona da Caçamba":
                    recomendacao = "Reparar trava de basculamento e substituir lona da caçamba.";
                    break;
                default:
                    recomendacao = "Realizar inspeção detalhada e reparo do item.";
                    break;
            }

            return recomendacao;
        }

        public List<string> GerarTextoRecomendacoes(List<ItemVistoria> itens)
        {
            List<string> textosRecomendacoes = new List<string>();

            foreach (ItemVistoria item in itens)
            {
                string recomendacao = ObterRecomendacaoParaItem(item.Nome);
                textosRecomendacoes.Add(item.Nome + ": " + recomendacao);
            }

            return textosRecomendacoes;
        }
    }
}