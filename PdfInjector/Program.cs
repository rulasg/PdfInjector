using System;
using System.IO;

namespace PdfInjector;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: my-dotnet-app <student name> <pdf file path>");
            return;
        }

        var studentName = args[0];
        var pdfFilePath = args[1];

        // Display full path of pdfFilePath
        var fullPdfFilePath = Path.GetFullPath(pdfFilePath);
        Console.WriteLine($"Full path of pdfFilePath: {fullPdfFilePath}");

        // Validate studentName y pdfFilePath
        if (studentName == null) { Console.WriteLine("Error: Student name cannot be null."); return; }
        if (pdfFilePath == null || !File.Exists(pdfFilePath)) { Console.WriteLine("Error: PDF file path not found."); return; }

        // Create pdfPathOutput
        var pdfPathOutput = Path.Combine(Path.GetDirectoryName(pdfFilePath), $"{Path.GetFileNameWithoutExtension(pdfFilePath)}_{studentName}{Path.GetExtension(pdfFilePath)}");

        var student = new Student { Name = studentName };
        var pdfService = new PdfService();

        try
        {
            pdfService.InjectNameIntoPdf(student, pdfFilePath, pdfPathOutput);
            Console.WriteLine("Name injected successfully into the PDF.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}