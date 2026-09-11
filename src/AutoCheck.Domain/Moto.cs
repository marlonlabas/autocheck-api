using System.Collections.Generic;

namespace AutoCheck.Domain
{
    public class Moto : Veiculo
    {
        public int Cilindradas { get; set; }

        public Moto(string marca, string modelo, int ano, double quilometragem, int cilindradas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.Cilindradas = cilindradas;
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            List<string> checklist = base.ObterChecklistObrigatorio();

            checklist.Add("Kit Transmissão/Corrente");
            checklist.Add("Manetes de Freio/Embreagem");
            checklist.Add("Pezinho Lateral");

            return checklist;
        }

        public override string ObterTipoDescricao()
        {
            return "Moto";
        }

        public override string ObterAtributoEspecifico()
        {
            return Cilindradas + " cc";
        }
    }
}