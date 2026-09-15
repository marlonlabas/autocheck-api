namespace AutoCheck.Domain
{
    public class ItemVistoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (value != "BOM" && value != "REGULAR" && value != "RUIM" && value != "AUSENTE")
                {
                    throw new ArgumentException("Status deve ser 'BOM', 'REGULAR', 'RUIM' ou 'AUSENTE'");
                }
                _status = value;
            }
        }

    public ItemVistoria(string nome, string status)
        {
            Nome = nome;
            Status = status;
        }
    }
}