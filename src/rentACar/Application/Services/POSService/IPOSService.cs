namespace Application.Services.POSService;

public interface IPOSService
{
    Task Pay(string invoiceNo, decimal price); //todo: credit card etc.
}