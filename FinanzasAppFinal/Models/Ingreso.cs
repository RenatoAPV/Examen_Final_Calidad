namespace FinanzasAppFinal.Models
{
    public class Ingreso
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public Decimal MontoIngreso { get; set; }
        public string? CuentaNombre { get; set; }
    }
}
