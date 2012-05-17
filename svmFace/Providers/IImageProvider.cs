using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using svmFace.Data;

namespace svmFace.Providers
{
    public interface IImageProvider
    {
        void listen(Action<TrainingData, String> callback);
    }
}
