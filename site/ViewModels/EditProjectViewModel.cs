using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace site.ViewModels
{
    public class EditProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Cover { get; set; }
        
        
        public List<int> Specialities { get; set; }
        public List<string> Members { get; set; }
        public string Leader { get; set; }
        public List<string> Links { get; set; }
    }
}