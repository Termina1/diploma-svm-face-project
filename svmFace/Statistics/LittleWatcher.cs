using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace svmFace.Statistics
{
    public class LittleWatcher
    {
        protected String subject;
        protected List<double> times;

        public LittleWatcher(String name)
        {
            subject = name;
            times = new List<double>();
        }

        public void addTime(double milliseconds)
        {
            times.Add(milliseconds);
        }

        public String getAverageString()
        {
            return subject + ": " + times.Average();
        }
  
    }
}
