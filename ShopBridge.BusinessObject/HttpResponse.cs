using System.Diagnostics.CodeAnalysis;

namespace ShopBridge.BusinessObject
{
    [ExcludeFromCodeCoverage]
    public class HttpResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

    }
}
