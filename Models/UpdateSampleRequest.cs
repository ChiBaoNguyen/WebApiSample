using System.ComponentModel.DataAnnotations;

namespace WebAPISample.Models
{
    public class UpdateSampleRequest
    {
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
