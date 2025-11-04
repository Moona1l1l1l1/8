using System;

public interface IInternalDeliveryService
{
    void DeliverOrder(string orderId);
    string GetDeliveryStatus(string orderId);
}

public class InternalDeliveryService : IInternalDeliveryService
{
    public void DeliverOrder(string orderId)
    {
        Console.WriteLine($"Delivering order {orderId} via internal service");
    }

    public string GetDeliveryStatus(string orderId)
    {
        return $"Order {orderId} delivered (internal)";
    }
}

public class ExternalLogisticsServiceA
{
    public void ShipItem(int itemId)
    {
        Console.WriteLine($"Shipping item {itemId} via External A");
    }
    public string TrackShipment(int shipmentId)
    {
        return $"External A tracking: shipment {shipmentId} in transit";
    }
}

public class ExternalLogisticsServiceB
{
    public void SendPackage(string packageInfo)
    {
        Console.WriteLine($"Sending package {packageInfo} via External B");
    }
    public string CheckPackageStatus(string trackingCode)
    {
        return $"External B status: {trackingCode} delivered";
    }
}

public class LogisticsAdapterA : IInternalDeliveryService
{
    private ExternalLogisticsServiceA service;
    public LogisticsAdapterA(ExternalLogisticsServiceA s) { service = s; }
    public void DeliverOrder(string orderId)
    {
        service.ShipItem(int.Parse(orderId));
    }
    public string GetDeliveryStatus(string orderId)
    {
        return service.TrackShipment(int.Parse(orderId));
    }
}

public class LogisticsAdapterB : IInternalDeliveryService
{
    private ExternalLogisticsServiceB service;
    public LogisticsAdapterB(ExternalLogisticsServiceB s) { service = s; }
    public void DeliverOrder(string orderId)
    {
        service.SendPackage(orderId);
    }
    public string GetDeliveryStatus(string orderId)
    {
        return service.CheckPackageStatus(orderId);
    }
}

public static class DeliveryServiceFactory
{
    public static IInternalDeliveryService GetService(string type)
    {
        return type switch
        {
            "internal" => new InternalDeliveryService(),
            "A" => new LogisticsAdapterA(new ExternalLogisticsServiceA()),
            "B" => new LogisticsAdapterB(new ExternalLogisticsServiceB()),
            _ => new InternalDeliveryService()
        };
    }
}

class Program
{
    static void Main()
    {
        IInternalDeliveryService service1 = DeliveryServiceFactory.GetService("internal");
        service1.DeliverOrder("101");
        Console.WriteLine(service1.GetDeliveryStatus("101"));

        IInternalDeliveryService service2 = DeliveryServiceFactory.GetService("A");
        service2.DeliverOrder("202");
        Console.WriteLine(service2.GetDeliveryStatus("202"));

        IInternalDeliveryService service3 = DeliveryServiceFactory.GetService("B");
        service3.DeliverOrder("PKG-303");
        Console.WriteLine(service3.GetDeliveryStatus("PKG-303"));
    }
}
