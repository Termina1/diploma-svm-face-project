using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;

namespace svmFace.Statistics
{
    public static class Overseer
    {
        private static ConcurrentDictionary<String, LittleWatcher> bank;

        public static Imp observe(String subject)
        {
            if (bank == null) {
                bank = new ConcurrentDictionary<String, LittleWatcher>();
            }
            LittleWatcher watcher;
            if (!bank.TryGetValue(subject, out watcher))
            {
                bank[subject] = new LittleWatcher(subject);
                watcher = bank[subject];
            }
            return new Imp(watcher);
        }

        public static void log()
        {
            using(var file = new System.IO.StreamWriter("log.txt")) {
                var div = "-----------------------";
                file.WriteLine(div);
                bank.ToList().ForEach((watcher) => {
                    file.WriteLine(watcher.Value.getAverageString());
                });
            }
        }
    }
}
