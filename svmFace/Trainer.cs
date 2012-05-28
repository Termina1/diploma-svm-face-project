using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SVM;
using svmFace.Providers;
using svmFace.Data;
using svmFace.Statistics;
using Send = System.Action<System.String>;

namespace svmFace
{
    public class Trainer
    {
        public static void build(IImageProvider provider) {
            provider.listen((data, command) => {
                var tr = new Trainer(data);
                TrainerCommand.create(command, tr).execute();
            });
        }

        protected TrainingData _data;

        protected Trainer(TrainingData data) {
            _data = data;
        }

        

        public void walk() {
            var totalSpan = Overseer.observe("Training");
            var problem = readProblem();
            var man = new Person {
                model = train(problem),
                name = _data.name
            };
            var span = Overseer.observe("Training.DB-Write");
            Redis.getInstance().registerPerson(man);
            _data.senback(_data.name);
            span.die();
            totalSpan.die();
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

        protected Problem readProblem() {
            var resultVector = _data.images.Select<TypedImage, Vector>((el) => {
                var vector = el.img.resize(50, 50).toGrayScaleVector();
                vector.type = el.type;
                return vector;
            });
            return vectorListToProblem(resultVector.ToList());
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

        public void predict() {
            var totalSpan = Overseer.observe("Prediction");
            var span = Overseer.observe("Prediction.Common-ops");
            var test = new List<Node[]>(); 
            _data.images.ToList().ForEach((map) => {
                var vc = map.img.resize(50, 50).toGrayScaleVector();
                test.Add(vc.Select((val, index) => new Node {
                    Index = index,
                    Value = val
                }).ToArray());
            });
            span.die();
            var models = Redis.getInstance().planes();
            var dbspan = Overseer.observe("Prediction.DB_Select");
            test.ForEach((nodes) => {
                var results = models.Select((model) => {
                    dbspan.die();
                    dbspan = Overseer.observe("Prediction.DB_Select");
                    var pspan = Overseer.observe("Prediction.Predict");
                    var d = Prediction.PredictRaw(model.model, nodes);
                    pspan.die();
                    dbspan = Overseer.observe("Prediction.DB_Select");
                    return new { d = d, name = model.name };
                });
                var amax = results.OrderByDescending(el => el.d).FirstOrDefault();
                if (amax != null && amax.d > 0) {
                    _data.senback(amax.name);
                } else {
                    _data.senback("not found");
                }
            });
            totalSpan.die();
            Overseer.log();
        }
    }
}
