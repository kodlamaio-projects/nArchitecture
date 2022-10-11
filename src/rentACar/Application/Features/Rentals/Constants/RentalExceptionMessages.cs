namespace Application.Features.Rentals.Constants
{
    public static class RentalExceptionMessages
    {
        public static string RentalNotExistsMessage => "Rental not exists.";
        public static string RentalAnotherRentedCarForTheDateMessage => "Rental can't be updated when there is another rented car for the date.";
        public static string RentalCanNotBeCreatedScorLowerMessage => "Rental can not be created when customer findeks credit score lower than car min findeks score.";
    }
}
