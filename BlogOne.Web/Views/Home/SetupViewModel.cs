using System.ComponentModel.DataAnnotations;

namespace BlogOne.Web.Views.Home
{
    public class SetupViewModel
    {
        [Required]
        public string BlogTitle { get; set; }
        public string Name { get; set; }
    }
}