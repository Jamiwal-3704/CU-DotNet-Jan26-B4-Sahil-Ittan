using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System.ComponentModel.DataAnnotations;


namespace VoltGearSystems.Models
{
    public class Laptop
    {
        [BsonId]
        [BsonSerializer(typeof(StringIdSerializer))]
        public string? Id { get; set; }

        [Required]
        public string? ModelName { get; set; }

        [Required]
        public string? SerialNumber { get; set; }

        [Range(0,double.MaxValue, ErrorMessage = "Price must be Positive")]
        public decimal Price { get; set; }
    }
}
