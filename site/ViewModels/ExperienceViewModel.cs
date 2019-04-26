using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace site.ViewModels
{
    public class ExperienceViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Ссылка, если есть")]
        public string Link { get; set; }
        
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
        public string StartDateString => StartDate.ToString("yyyy-MM-dd");
        
        [Display(Name = "Дата окончания")]
        public DateTime FinishDate { get; set; }

        public string FinishDateString => FinishDate.ToString("yyyy-MM-dd");
        
        [Display(Name = "Работа")]
        public bool IsWork { get; set; }
        
        [Display(Name = "По настоящее время")]
        public bool IsNow { get; set; }
        
    }
}