using Microsoft.EntityFrameworkCore;
using Training.DotNetCore.DA.Model;
using System;

namespace Training.DotNetCore.DA
{
    public class DotNetCoreTrainingContext : DbContext
    {
        public DbSet<Model.Training> Trainings { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        public DotNetCoreTrainingContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var trainingAttendee = modelBuilder.Entity<TrainingAttendee>();
            trainingAttendee.HasKey(ta => new { ta.TrainingId, ta.AttendeeId });

            trainingAttendee
                .HasOne(ta => ta.Training)
                .WithMany(tr => tr.TrainingAttendees)
                .HasForeignKey(ta => ta.TrainingId);

            trainingAttendee
                .HasOne(ta => ta.Attendee)
                .WithMany(a => a.TrainingAttendees)
                .HasForeignKey(ta => ta.AttendeeId);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, Name = "Test trainer" },
                new Trainer { Id = 2, Name = "Test trainer 2"});

            modelBuilder.Entity<Attendee>().HasData(
                new Attendee { Id = 1, Name = "Test attendee" },
                new Attendee { Id = 2, Name = "Test attendee 2"});

            modelBuilder.Entity<Model.Training>().HasData(
                new Model.Training
                { 
                    Id = 1,
                    TrainerId = 1,
                    Name = "Test training",
                    Description = "Description",
                    StartDate = new DateTime(2018, 4, 7),
                    EndDate = new DateTime(2018, 4, 8)
                },
                new Model.Training
                { 
                    Id = 2,
                    TrainerId = 2,
                    Name = "Test training 2",
                    Description = "Description",
                    StartDate = new DateTime(2018, 4, 7),
                    EndDate = new DateTime(2018, 4, 8)
                }
            );

            modelBuilder.Entity<TrainingAttendee>().HasData(
                new TrainingAttendee { TrainingId = 1, AttendeeId = 1 },
                new TrainingAttendee { TrainingId = 1, AttendeeId = 2 }
            );
        }
    }
}