using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingDetail.App.Classs
{
    public class TrainingCompletion
    {
        public string name { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime? expires { get; set; }
    }
    class People
    {
        public string name { get; set; }
        public List<TrainingCompletion> completions { get; set; }
    }
    public class Task1Output
    {
        public string trainingName { get; set; }
        public int Count { get; set; }
    }
    public class Task2Output
    {
        public string trainingName { get; set; }
        public List<string> People { get; set; }
    }

    public class ExpiringTraining
    {
        public string trainingName { get; set; }
        public string expiresOn { get; set; }
        public string status { get; set; }
    }

    public class Task3Output
    {
        public string name { get; set; }
        public List<ExpiringTraining> expiredOrSoon { get; set; }
    }


}
