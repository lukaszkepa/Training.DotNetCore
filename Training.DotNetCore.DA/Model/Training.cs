using System;
using System.Collections.Generic;

namespace Training.DotNetCore.DA.Model
{
    public class Training
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Trainer Trainer { get; set; }
        public ICollection<TrainingAttendee> TrainingAttendees { get; set; }
    }
}