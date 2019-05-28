using Film.TypeOf;
using Newtonsoft.Json;

namespace Film.Models
{
    public class Images
    {
        public string Id { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] Img { get; set; }

    }
}