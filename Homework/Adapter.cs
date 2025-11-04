using System;

public interface IPaymentProcessor
{
    void ProcessPayment(double amount);
}

public class PayPalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата {amount}$ через PayPal выполнена.");
    }
}

public class StripePaymentService
{
    public void MakeTransaction(double totalAmount)
    {
        Console.WriteLine($"Транзакция на {totalAmount}$ через Stripe завершена.");
    }
}

public class StripePaymentAdapter : IPaymentProcessor
{
    private StripePaymentService stripeService;
    public StripePaymentAdapter(StripePaymentService service)
    {
        stripeService = service;
    }
    public void ProcessPayment(double amount)
    {
        stripeService.MakeTransaction(amount);
    }
}

class Program
{
    static void Main()
    {
        IPaymentProcessor paypal = new PayPalPaymentProcessor();
        IPaymentProcessor stripe = new StripePaymentAdapter(new StripePaymentService());
        paypal.ProcessPayment(50);
        stripe.ProcessPayment(75);
    }
}
