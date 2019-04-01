using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using site.Models;

namespace site.ViewModels
{
    public class ProjectViewModel
    {
        public string Title { get; set; }
        public IFormFile Cover { get; set; }
        public string Description { get; set; }
        public IFormFileCollection SliderImages { get; set; }
        public List<int> Specs { get; set; }
        public List<string> Members { get; set; }
    }
}