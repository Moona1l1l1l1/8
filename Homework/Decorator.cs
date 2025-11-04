using System;

public interface IBeverage
{
    string GetDescription();
    double Cost();
}

public class Espresso : IBeverage
{
    public string GetDescription() => "Espresso";
    public double Cost() => 3.0;
}

public class Tea : IBeverage
{
    public string GetDescription() => "Tea";
    public double Cost() => 2.5;
}

public abstract class BeverageDecorator : IBeverage
{
    protected IBeverage beverage;
    public BeverageDecorator(IBeverage b) { beverage = b; }
    public virtual string GetDescription() => beverage.GetDescription();
    public virtual double Cost() => beverage.Cost();
}

public class Milk : BeverageDecorator
{
    public Milk(IBeverage b) : base(b) { }
    public override string GetDescription() => beverage.GetDescription() + ", Milk";
    public override double Cost() => beverage.Cost() + 0.5;
}

public class Sugar : BeverageDecorator
{
    public Sugar(IBeverage b) : base(b) { }
    public override string GetDescription() => beverage.GetDescription() + ", Sugar";
    public override double Cost() => beverage.Cost() + 0.3;
}

public class WhippedCream : BeverageDecorator
{
    public WhippedCream(IBeverage b) : base(b) { }
    public override string GetDescription() => beverage.GetDescription() + ", Whipped Cream";
    public override double Cost() => beverage.Cost() + 0.7;
}

class Program
{
    static void Main()
    {
        IBeverage drink = new Espresso();
        drink = new Milk(drink);
        drink = new Sugar(drink);
        drink = new WhippedCream(drink);
        Console.WriteLine($"{drink.GetDescription()} - ${drink.Cost()}");
    }
}
