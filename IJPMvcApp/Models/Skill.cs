﻿using System.ComponentModel.DataAnnotations;

namespace IJPMvcApp.Models
{
    
    public class Skill
     {
        [Display(Name = "Skill Id")]
        [RegularExpression(@"\w{6}", ErrorMessage = "SkillId  must be 6 characters")]
        public string SkillId { get; set; }
        
        [Display(Name = "Skill Name")]
        public string SkillName { get; set; } = null!;
       
        [Display(Name = "Skill Level")]
        [Required(ErrorMessage = "Skill Level is required")]
        public char SkillLevel { get; set; }
        
        [Display(Name = "Category")]
        public int SkillCategory { get; set; }
    }
}
