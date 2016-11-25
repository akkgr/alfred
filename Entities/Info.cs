using System.ComponentModel.DataAnnotations;

namespace teleRDV.Models
{
    public class Info
    {
        [Required]
        public InfoType InfoType { get; set; }

        [Required]
        public string Value { get; set; }
    }
}