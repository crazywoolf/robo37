using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Course
    {
        [HiddenInput(DisplayValue=false)]
        [Display(Name = "ID")]
        public int CourseId { get; set; }

        [Display(Name="Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название курса")]
        public string Name { get; set; }

        [Display(Name = "Учитель")]
        [Required(ErrorMessage = "Пожалуйста, укажите преподавателя")]
        public string Teacher { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание курса")]
        public string Description { get; set; }

        [Display(Name = "Платформа")]
        [Required(ErrorMessage = "Пожалуйста, укажите робототехническую платформу")]
        public string Platform { get; set; }

        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительную цену курса")]
        public decimal Price { get; set; }

        [Display(Name = "Категория")]
        public string Genre { get; set; } 
    }
}
