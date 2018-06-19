namespace Training.DotNetCore.DA.Model
{
    public class TrainingAttendee
    {
        public int TrainingId { get; set; }
        public int AttendeeId { get; set; }

        public Training Training { get; set; }
        public Attendee Attendee { get; set; }
    }
}