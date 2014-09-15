using System.ComponentModel.DataAnnotations;

namespace BlogOne.Web.Views.Home
{
    public class SetupViewModel
    {
        [Required]
        public string BlogTitle { get; set; }
        public string Name { get; set; }

        [Required]
        public string GoogleClientId { get; set; }

        [Required]
        public string GoogleClientSecret { get; set; }
    }
}