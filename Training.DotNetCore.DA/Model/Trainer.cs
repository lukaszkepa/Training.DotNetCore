using System.Collections.Generic;

namespace Training.DotNetCore.DA.Model
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Training> Trainings { get; set; }
    }
}