using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SVM;
using MongoDB;

namespace svmFace
{
    public class Trainer
    {
        public static Trainer build(String path) {
            var tr = new Trainer();
            tr.walk(path);
            return tr;
        }



        protected void walk(String path) { 
            var directories = Directory.GetDirectories(path);
            for (int num = 0; num < 2; num++)
            {
                var totalSpan = Overseer.observe("Training");
                var problem = readProblem(num, directories);
                var man = new Person
                {
                    model = train(problem),
                    name = "man " + num
                };
                var span = Overseer.observe("Training.DB-Write");
                Mongoid.getInstance().registerPerson(man);
                span.die();
                totalSpan.die();
            }
            Overseer.log();
        }

        public Model train(Problem issue) {
            var span = Overseer.observe("Training.Parameter-Choosing");
            Parameter parameters = new Parameter();
            parameters.KernelType = KernelType.RBF;
            double C;
            double Gamma;

            ParameterSelection.Grid(issue, parameters, null, out C, out Gamma);
            parameters.C = C;
            parameters.Gamma = Gamma;
            span.die();
            span = Overseer.observe("Training.Training");
            var result = Training.Train(issue, parameters);
            span.die();
            return result;

        }

        protected Problem readProblem(int num, String[] directories) {
            var friends = new List<Vector>();
            var foes = new List<Vector>();
            var dir = directories[num];
            var files = Directory.GetFiles(dir);
            int iterations = files.Length - 4;
            for (int i = 0; i < iterations; i++)
            {
                Bitmap map = Bitmap.FromFile(files[i]) as Bitmap;
                friends.Add(map.resize(50, 50).toGrayScaleVector());
            }

            for (int i = 0; i < iterations * 2; i++)
            {
                var rand = new Random();
                int randDir = rand.Next(directories.Length - 1);
                if (directories[randDir] == dir)
                {
                    i--;
                    continue;
                }
                var rfiles = Directory.GetFiles(directories[randDir]);
                int randFile = rand.Next(rfiles.Length - 1);
                Bitmap rmap = Bitmap.FromFile(rfiles[randFile]) as Bitmap;
                var foeVector = rmap.resize(50, 50).toGrayScaleVector();
                foeVector.type = 2;
                foes.Add(foeVector);
            }
            return vectorListToProblem(friends.Concat(foes).ToList());
        }

        public Problem vectorListToProblem(List<Vector> vlist) {
            List<double> vy = new List<double>();
            List<Node[]> vx = new List<Node[]>();
            vlist.ForEach((sv) => {
                vy.Add(sv.type);
                vx.Add(sv.Select((val, i) => new Node(i, val)).ToArray());
            });
            int maxCount = vlist.Max((v) => v.Count);
            return new Problem(vlist.Count, vy.ToArray(), vx.ToArray(), maxCount);
        }

        public String predict(String path) {
            var totalSpan = Overseer.observe("Prediction");
            var span = Overseer.observe("Prediction.Common-ops");
            Bitmap map = Bitmap.FromFile(path) as Bitmap;
            var vc = map.resize(50, 50).toGrayScaleVector();
            var test = vc.Select((val, index) => new Node {
                Index = index,
                Value = val
            }).ToArray();
            span.die();
            var models = Mongoid.getInstance().planes();
            var dbspan = Overseer.observe("Prediction.DB_Select");
            var results = models.FindAllAs<Person>().Select((model) =>
            {
                dbspan.die();
                var pspan = Overseer.observe("Prediction.Predict");
                var d = Prediction.PredictRaw(model.model, test);
                pspan.die();
                dbspan = Overseer.observe("Prediction.DB_Select");
                return new { d = d, name = model.name };
            });
            var amax = results.OrderByDescending(el => el.d).FirstOrDefault();
            totalSpan.die();
            Overseer.log();
            if (amax != null && amax.d > 0)
            {
                return amax.name;
            }
            else { 
                return "not found";
            }
        }
    }
}
