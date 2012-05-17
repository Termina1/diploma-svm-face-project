using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace svmFace.Providers
{
    class TrainerCommand
    {

        private String _name;
        private Trainer _tr;

        public static TrainerCommand create(String name, Trainer trainer) {
            return new TrainerCommand(name, trainer);
        }

        protected TrainerCommand(String name, Trainer trainer) {
            _tr = trainer;
            _name = name;
        }

        public void execute() {
            switch (_name) { 
                case "train":
                    _tr.walk();
                    break;
            }
        }
    }
}
