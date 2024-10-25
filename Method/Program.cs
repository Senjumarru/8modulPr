using System;

namespace ReportTemplateMethod
{
    public abstract class ReportGenerator
    {
        public void GenerateReport()
        {
            CollectData();
            ProcessData();
            FormatReport();
            SaveReport();
            SendReport();
        }

        protected abstract void CollectData();
        protected abstract void ProcessData();
        protected abstract void FormatReport();
        protected abstract void SaveReport();

        protected virtual void SendReport() => Console.WriteLine("Отчет отправлен.");
    }

    public class PdfReport : ReportGenerator
    {
        protected override void CollectData() => Console.WriteLine("Сбор данных для PDF-отчета.");
        protected override void ProcessData() => Console.WriteLine("Обработка данных для PDF-отчета.");
        protected override void FormatReport() => Console.WriteLine("Форматирование PDF-отчета.");
        protected override void SaveReport() => Console.WriteLine("PDF-отчет сохранен.");
    }

    public class ExcelReport : ReportGenerator
    {
        protected override void CollectData() => Console.WriteLine("Сбор данных для Excel-отчета.");
        protected override void ProcessData() => Console.WriteLine("Обработка данных для Excel-отчета.");
        protected override void FormatReport() => Console.WriteLine("Форматирование Excel-отчета.");
        protected override void SaveReport() => Console.WriteLine("Excel-отчет сохранен.");
    }

    public class HtmlReport : ReportGenerator
    {
        protected override void CollectData() => Console.WriteLine("Сбор данных для HTML-отчета.");
        protected override void ProcessData() => Console.WriteLine("Обработка данных для HTML-отчета.");
        protected override void FormatReport() => Console.WriteLine("Форматирование HTML-отчета.");
        protected override void SaveReport() => Console.WriteLine("HTML-отчет сохранен.");
    }

    public class Program
    {
        public static void Main()
        {
            ReportGenerator pdfReport = new PdfReport();
            pdfReport.GenerateReport();

            ReportGenerator excelReport = new ExcelReport();
            excelReport.GenerateReport();

            ReportGenerator htmlReport = new HtmlReport();
            htmlReport.GenerateReport();
        }
    }
}
