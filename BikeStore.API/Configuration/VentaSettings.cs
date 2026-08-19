namespace BikeStore.API.Configuration
{
    public class VentaSettings
    {
        public const string SectionName = "Venta";

        public decimal PorcentajeIva { get; set; } = 0.15m;
    }
}
