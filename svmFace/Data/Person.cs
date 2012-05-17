using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SVM;

namespace svmFace.Data
{
    [Serializable]
    public class Person
    {
        public Model model { get; set; }

        public String name { get; set; }
    }
}
