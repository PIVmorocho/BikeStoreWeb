namespace BikeStore.API.Models.Entities
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdCliente { get; set; }
        public decimal Total { get; set; }

        public Cliente? Cliente { get; set; }
        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
