namespace MegaDesk_Razor.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryType { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int DrawerCount { get; set; }
        public string Material { get; set; }
        public int Cost { get; set; }

    }
}
