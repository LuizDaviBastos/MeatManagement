using System.Text.Json.Serialization;

namespace MeatManager.Service.Models
{
    public class AwesomeApiResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("codein")]
        public string CodeIn { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("high")]
        public decimal High { get; set; }

        [JsonPropertyName("low")]
        public decimal Low { get; set; }

        [JsonPropertyName("varBid")]
        public decimal VarBid { get; set; }

        [JsonPropertyName("pctChange")]
        public decimal PctChange { get; set; }

        [JsonPropertyName("bid")]
        public decimal Bid { get; set; }

        [JsonPropertyName("ask")]
        public decimal Ask { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
    }
}
