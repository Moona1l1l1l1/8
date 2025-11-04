using System;

public interface IReport
{
    string Generate();
}

public class SalesReport : IReport
{
    public string Generate() => "Sales Report: 5 sales, total 2500$";
}

public class UserReport : IReport
{
    public string Generate() => "User Report: 120 active users";
}

public abstract class ReportDecorator : IReport
{
    protected IReport report;
    public ReportDecorator(IReport r) { report = r; }
    public virtual string Generate() => report.Generate();
}

public class DateFilterDecorator : ReportDecorator
{
    public DateFilterDecorator(IReport r) : base(r) { }
    public override string Generate() => report.Generate() + " | Filtered by date";
}

public class SortingDecorator : ReportDecorator
{
    public SortingDecorator(IReport r) : base(r) { }
    public override string Generate() => report.Generate() + " | Sorted by amount";
}

public class CsvExportDecorator : ReportDecorator
{
    public CsvExportDecorator(IReport r) : base(r) { }
    public override string Generate() => report.Generate() + " | Exported to CSV";
}

public class PdfExportDecorator : ReportDecorator
{
    public PdfExportDecorator(IReport r) : base(r) { }
    public override string Generate() => report.Generate() + " | Exported to PDF";
}

class Program
{
    static void Main()
    {
        IReport report = new SalesReport();
        report = new DateFilterDecorator(report);
        report = new SortingDecorator(report);
        report = new CsvExportDecorator(report);
        Console.WriteLine(report.Generate());

        IReport userReport = new UserReport();
        userReport = new PdfExportDecorator(userReport);
        Console.WriteLine(userReport.Generate());
    }
}
