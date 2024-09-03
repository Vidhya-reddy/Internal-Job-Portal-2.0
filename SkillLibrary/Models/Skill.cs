using System;
using System.Collections.Generic;

namespace SkillLibrary.Models;

public partial class Skill
{
    public string SkillId { get; set; } = null!;

    public string SkillName { get; set; } = null!;

    public char SkillLevel { get; set; } 

    public int SkillCategory { get; set; }
}
