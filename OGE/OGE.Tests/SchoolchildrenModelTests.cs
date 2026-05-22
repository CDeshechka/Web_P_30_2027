using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OGE.Model;
using Xunit;

namespace OGE.Tests
{
    public class SchoolchildrenModelTests
    {
        [Fact]
        public void Validate_AgeDoesNotMatchBirthdate_ReturnsValidationError()
        {
            var student = new Schoolchildren
            {
                Firstname = "Иван",
                Lastname = "Иванов",
                Age = 3,                       
                Dateofbirthday = new DateTime(2007, 3, 28),
                Email = "ivan@example.com"
            };

            var validationContext = new ValidationContext(student);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(student, validationContext, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r =>
                r.MemberNames.Contains(nameof(Schoolchildren.Age)) &&
                r.ErrorMessage.Contains("не соответствует дате рождения"));
        }
    }
}