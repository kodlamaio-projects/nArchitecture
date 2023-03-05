using System.Text.RegularExpressions;

namespace Application.Features.Cars.Validations;

public static class CarCustomValidationRules
{
    public static bool IsTurkeyPlate(string plate)
    {
        Regex regex = new(@"(0[1-9]|[1-7][0-9]|8[01])(([A-Z])(\d{4,5})|([A-Z]{2})(\d{3,4})|([A-Z]{3})(\d{2}))");
        return regex.IsMatch(plate);
    }
}
