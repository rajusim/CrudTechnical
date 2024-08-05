using System.Text.RegularExpressions;

namespace Logic.TechnicalAssement.App.Services
{
    public class ValidationRule : IValidationRule
    {
        public string IsValidDates(DateTime startDate, DateTime endDate, bool halfDay)
        {
            int result = DateTime.Compare(startDate, endDate);
            var validState = string.Empty;
            if (result < 0)
                validState = "Valid";
            else if (result == 0)
                validState = "Equal";
            else if (result > 0)
                validState = "InValid";
            if (halfDay && validState != "Equal")
                validState = "Half Day can be taken for same date";
            if (validState == "Invalid")
                validState = "Start date is greater than End date";
            if (halfDay && validState == "Equal")
                validState = "Valid";
            return validState;
        }

        public bool IsValidEmail(string email)
                        => Regex.IsMatch(email, @"^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");


    }
}
