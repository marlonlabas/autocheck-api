using System.Collections.Generic;

namespace AutoCheck.Domain
{
    public class Veiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Quilometragem { get; set; }
        public List<ItemVistoria> VistoriaRealizada { get; set; }

        public Veiculo(string marca, string modelo, int ano, double quilometragem)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
            this.VistoriaRealizada = new List<ItemVistoria>();
        }

        public void AdicionarItemVistoriado(string nome, string status)
        {
            ItemVistoria novoItem = new ItemVistoria(nome, status);
            VistoriaRealizada.Add(novoItem);
        }

        public virtual List<string> ObterChecklistObrigatorio()
        {
            return new List<string>
            {
                "Nível de Óleo do Motor",
                "Bateria e Sistema Elétrico",
                "Documentação Regularizada",
                "Condições dos Pneus"
            };
        }

        public virtual string ObterTipoDescricao()
        {
            return "Veículo";
        }

        public virtual string ObterAtributoEspecifico()
        {
            return "";
        }
    }
}