using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Callback = System.Action<svmFace.Data.TrainingData, System.String>;

namespace svmFace.Providers
{
    public class ParallelLocalProvider : LocalProvider
    {
        public ParallelLocalProvider(String name, String command) : base(name, command) { }

        new protected void train(Callback callback) {
            var dirs = Directory.GetDirectories(_name);
            Action<String> send = (name) => Console.WriteLine("trained " + name);
            dirs.AsParallel().ForAll((name) => {
                trainRow(callback, name, dirs, send);
            });
        }
    }
}
