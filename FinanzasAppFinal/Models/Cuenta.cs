namespace FinanzasAppFinal.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Categoria { get; set; }
        public decimal SaldoInicial { get; set; }
        public string? Moneda { get; set; }
        public bool EsCredito { get; set; }
    }
}
