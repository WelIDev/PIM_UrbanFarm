using System.Text.Json.Serialization;

namespace MobileApp.Models
{
    public class JsonResponse<T>
    {
        [JsonPropertyName("$values")]
        public List<T> Values { get; set; }
    }
}
