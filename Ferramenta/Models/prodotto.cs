namespace Ferramenta.Models
{
    public class Prodotto
    {
        public required string Cod { get; set; }
        public string? Nome { get; set; }
        public string? Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public string? Categoria { get; set; }
    }
}
