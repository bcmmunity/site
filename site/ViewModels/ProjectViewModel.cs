using System.Collections.Generic;
using site.Models;

namespace site.ViewModels
{
    public class ProjectViewModel
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }
        public string SliderImages { get; set; }
        public List<int> Specs { get; set; }
        public List<string> Members { get; set; }
    }
}