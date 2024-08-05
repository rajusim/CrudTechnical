namespace Logic.TechnicalAssement.App.Services
{
    public interface IValidationRule
    {
        string IsValidDates(DateTime startDate, DateTime endDate, bool halfDay);
        bool IsValidEmail(string email);
    }
}
