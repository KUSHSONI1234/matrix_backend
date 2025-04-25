using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    public class FormData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public bool SetAsDefault { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
