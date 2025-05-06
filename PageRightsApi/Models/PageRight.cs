using System.ComponentModel.DataAnnotations;

namespace PageRightsApi.Models
{
    public class PageRight
    {
        public int Id { get; set; }


        public string Username { get; set; }

        public string Name { get; set; }

        public bool Edit { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }
        public bool Add { get; set; }
    }
}
