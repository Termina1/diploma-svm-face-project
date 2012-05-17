using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using svmFace.Data;

namespace svmFace.Providers
{
    public class LocalProvider : IImageProvider
    {
        private String _name;

        public LocalProvider(String dirName) {
            _name = dirName;
        }

        public void listen(Action<TrainingData, String> callback) {
            var dirs = Directory.GetDirectories(_name);

            dirs.ToList().ForEach((name) => {
                var images = getFromDir(name);
            });
        } 

        private List<TypedImage> getFromDir(String name) {
            return new List<TypedImage>();
        }
    }
}
