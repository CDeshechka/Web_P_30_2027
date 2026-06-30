using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OGE.Model;
using Xunit;

namespace OGE.Tests
{
    public class SubjectModelTests
    {
        [Fact]
        public void Validate_SubjectWithoutName_ReturnsValidationError()
        {
            
            var subject = new Subject
            {
                Assessmentfortheoge = 4,
                Academicyearassessment = 5,
                Finalassessment = 4
          
            };

            var validationContext = new ValidationContext(subject);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(subject, validationContext, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Subject.Name)));
        }
    }
}