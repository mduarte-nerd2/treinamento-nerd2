﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorialsConsole
{
    public class Grade
    {
        public int GradeId { get; set; }

        public string GradeName { get; set; }

        public IList<Student> Students { get; set; }

    }
}
