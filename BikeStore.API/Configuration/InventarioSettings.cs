namespace BikeStore.API.Configuration
{
    public class InventarioSettings
    {
        public const string SectionName = "Inventario";

        public int StockBajoUmbral { get; set; } = 5;
    }
}
