namespace Application.Features.Cars.Constants
{
    public static class CarExceptionMessages
    {
        public static string CarNotExistsMessage => "Car not exists.";
        public static string CarCanNotBeMaintainWhenIsRentedMessage => "Car can't be maintain when is rented.";
        public static string CarCanNotBeMaintainWhenIsMaintenanceMessage => "Car can not be rent when is in maintenance.";
        public static string CarCanNotBeRentWhenIsInsertedMessage => "Car can not be rent when is rented.";
    }
}
