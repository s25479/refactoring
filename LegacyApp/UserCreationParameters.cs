using System;

namespace LegacyApp
{
    public class UserCreationParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CurrentTime { get; set; }

        public bool Valid()
        {
            return FirstAndLastNameValid() && EmailValid() && AgeValid();
        }

        private bool FirstAndLastNameValid()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }

        private bool EmailValid()
        {
            return EmailAddress.Contains("@") || EmailAddress.Contains(".");
        }
        
        private bool AgeValid()
        {
            return AgeValid(AgeFromDateOfBirth());
        }

        private int AgeFromDateOfBirth()
        {
            int age = CurrentTime.Year - DateOfBirth.Year;
            if (CurrentTime.Month < DateOfBirth.Month || (CurrentTime.Month == DateOfBirth.Month && CurrentTime.Day < DateOfBirth.Day))
                age--;
            return age; 
        }

        private static bool AgeValid(int age)
        {
            return age >= 21;
        }
    }
}
