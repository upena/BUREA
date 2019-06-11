using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Question7.Models
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Title")]
        [Required(ErrorMessage ="Title is required")]
        public string DocumentName { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        [Required(ErrorMessage ="Content is required")]
        public string DocumentText { get; set; }

        public DateTime CreationDate { get; set; }
    }
}