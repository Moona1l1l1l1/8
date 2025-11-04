using System;

public interface IBeverage
{
    double GetCost();
    string GetDescription();
}

public class Coffee : IBeverage
{
    public double GetCost() => 50.0;
    public string GetDescription() => "Coffee";
}

public abstract class BeverageDecorator : IBeverage
{
    protected IBeverage beverage;
    public BeverageDecorator(IBeverage b) { beverage = b; }
    public virtual double GetCost() => beverage.GetCost();
    public virtual string GetDescription() => beverage.GetDescription();
}

public class MilkDecorator : BeverageDecorator
{
    public MilkDecorator(IBeverage b) : base(b) { }
    public override double GetCost() => base.GetCost() + 10.0;
    public override string GetDescription() => base.GetDescription() + ", Milk";
}

public class SugarDecorator : BeverageDecorator
{
    public SugarDecorator(IBeverage b) : base(b) { }
    public override double GetCost() => base.GetCost() + 5.0;
    public override string GetDescription() => base.GetDescription() + ", Sugar";
}

class Program
{
    static void Main()
    {
        IBeverage drink = new Coffee();
        drink = new MilkDecorator(drink);
        drink = new SugarDecorator(drink);
        Console.WriteLine($"{drink.GetDescription()} : {drink.GetCost()}");
    }
}
