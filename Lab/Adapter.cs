using System;

public interface IPaymentProcessor
{
    void ProcessPayment(double amount);
    void RefundPayment(double amount);
}

public class InternalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing payment of {amount} via Internal System.");
    }

    public void RefundPayment(double amount)
    {
        Console.WriteLine($"Refunding {amount} via Internal System.");
    }
}

public class ExternalPaymentSystemA
{
    public void MakePayment(double amount)
    {
        Console.WriteLine($"Making payment of {amount} via System A.");
    }

    public void MakeRefund(double amount)
    {
        Console.WriteLine($"Making refund of {amount} via System A.");
    }
}

public class PaymentAdapterA : IPaymentProcessor
{
    private ExternalPaymentSystemA systemA;
    public PaymentAdapterA(ExternalPaymentSystemA s) { systemA = s; }
    public void ProcessPayment(double amount) => systemA.MakePayment(amount);
    public void RefundPayment(double amount) => systemA.MakeRefund(amount);
}

class Program
{
    static void Main()
    {
        IPaymentProcessor internalProc = new InternalPaymentProcessor();
        internalProc.ProcessPayment(100);
        internalProc.RefundPayment(50);

        IPaymentProcessor externalProc = new PaymentAdapterA(new ExternalPaymentSystemA());
        externalProc.ProcessPayment(200);
        externalProc.RefundPayment(100);
    }
}
