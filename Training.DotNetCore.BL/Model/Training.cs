using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Training.DotNetCore.BL.Model
{
    public class Training : IValidatableObject
    {
        public int Id { get; set; }

        public int TrainerId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult($"Start date cannot be greater than end date.", new[] { nameof(StartDate), nameof(EndDate) });
            }
        }
    }
}