using System.Collections.Generic;

namespace Training.DotNetCore.DA.Model
{
    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TrainingAttendee> TrainingAttendees { get; set; }
    }
}