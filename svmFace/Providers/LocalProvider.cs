using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using svmFace.Data;
using System.Drawing;
using Callback = System.Action<svmFace.Data.TrainingData, System.String>;
namespace svmFace.Providers
{
    public class LocalProvider : IImageProvider
    {
        protected String _name;
        protected String _cmd;

        public LocalProvider(String name, String command) {
            _name = name;
            _cmd = command;
        }

        public void listen(Callback callback) {
            switch (_cmd) { 
                case "train":
                    train(callback);
                    break;
                case "predict":
                    predict(callback);
                    break;
                case "multipredict":
                    multipredict(callback);
                    break;
            }
        }

        protected StreamWriter logForProvider(String name) {
            if (!File.Exists(name))
            {
                return new StreamWriter(name);
            }
            else
            {
                return File.AppendText(name);
            }
        }

        protected void multipredict(Callback callback) {
            var dirs = Directory.GetDirectories(_name);
            var overall = 0;
            var errors = 0;
            var file = logForProvider("error.rate.txt");
            var counter = 0;
            dirs.ToList().ForEach((name) => {
                var files =  Directory.GetFiles(name);
                overall += files.Count();
                var results = new List<string>();
                callback(new TrainingData {
                    images = files.Select<string, TypedImage>((nm) => new TypedImage { img = Image.FromFile(nm) as Bitmap}).ToList(),
                    senback = (res) => { results.Add(res); file.WriteLine("Done already: " + (++counter).ToString()); file.Flush(); }
                }, "predict");
                file.WriteLine("Done: " + name);
                errors += results.Where((knownName) => name != knownName).Count();
            });
            File.WriteAllText("error.rate.txt", "Error rate: " + (errors / overall).ToString());
        }

        protected void train(Callback callback) {
            var dirs = Directory.GetDirectories(_name);
            Action<String> send = (str) => {
                Console.WriteLine("done " + str);
            };
            for (var i = 0; i < dirs.Length; i++) {
                trainRow(callback, dirs[i], dirs, send);
            }
        }

        protected void trainRow(Callback callback, String imgName, string[] dirs, Action<String> send) {
            var gotImages = getFromDir(imgName, dirs);
            callback(new TrainingData {
                images = gotImages,
                name = imgName,
                senback = send
            }, "train");
        }

        public delegate void GotNameHandler(object sender, String name);

        public event GotNameHandler GotName;

        private void predict(Callback callback) {
            var list = new List<TypedImage>();
            list.Add(new TypedImage {
                img = Image.FromFile(_name) as Bitmap,
                type = 1
            });
            Action<String> senback = (name) => {
                GotName(this, name);
            };
            callback(new TrainingData { 
                images = list,
                senback = senback
            }, "predict");
        }

        private List<TypedImage> getFromDir(String name, string[] directories) {
            var list = new List<TypedImage>();
            var images = Directory.GetFiles(name);

            var iterations = images.Length - 4;

            for (int i = 0; i < iterations; i++) {
                list.Add(new TypedImage { 
                    img = Image.FromFile(images[i]) as Bitmap,
                    type = 1
                });
            }

            var rand = new Random();
            for (int i = 0; i < iterations * 2; i++) {
                int randDir = rand.Next(directories.Length - 1);
                if (directories[randDir] == name) {
                    i--;
                    continue;
                }
                var rfiles = Directory.GetFiles(directories[randDir]);
                int randFile = rand.Next(rfiles.Length - 1);
                list.Add(new TypedImage{
                    img = Image.FromFile(rfiles[randFile]) as Bitmap,
                    type = 2
                });
            }
            return list;

        }
    }
}
