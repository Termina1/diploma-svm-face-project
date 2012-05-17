using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Send = System.Action<System.String>;

namespace svmFace.Data
{
    public class TrainingData
    {
        public String name;
        public Send senback;
        public List<TypedImage> images;
    }
}
