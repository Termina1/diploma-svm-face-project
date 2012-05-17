using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace svmFace.Statistics
{
    public class Imp
    {
        public DateTime start;
        public LittleWatcher observable;

        public Imp(LittleWatcher watcher)
        {
            start = DateTime.Now;
            observable = watcher;
        }

        public void die()
        {
            var dif = DateTime.Now - start;
            observable.addTime(dif.TotalMilliseconds);
        }
    }
}
