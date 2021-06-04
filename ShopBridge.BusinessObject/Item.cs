using System.Diagnostics.CodeAnalysis;

namespace ShopBridge.BusinessObject
{
    [ExcludeFromCodeCoverage]
    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
