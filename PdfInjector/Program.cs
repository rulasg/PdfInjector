using System;
using System.Diagnostics;
using System.IO;
using iTextSharp.text.pdf;

namespace PdfInjector;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2 || args[0] == "--help" || args[0] == "-h" || args[0] == "-?")
        {
            Console.WriteLine("Usage: pdfInjector <user handle> <pdf template path>");
            return;
        }

        var studentHandle = args[0];
        var pdfFilePath = args[1];

        // Validate studentHandle y pdfFilePath
        if (studentHandle == null) { Console.Error.WriteLine("Error: Student name cannot be null."); return; }
        if (pdfFilePath == null || !File.Exists(pdfFilePath)) { Console.Error.WriteLine("Error: PDF file path not found."); return; }

        // Training info
        var trainingName = "Training GitHub for Non-Developers";
        var trainingDate = "December 2023";

        // create user
        var student = new Person(){
            Name = "Raul (Dibildos) Gonzalez",
            Handle = "raulgeu",
            Company = "BiT21"
        };

        // create trainer
        var trainer = new Person(){
            Name = "Raul (Dibildos) Gonzalez Rodriguez",
            Handle = "rulasg",
            Company = "Solidify"
        };

        // Create pdfPathOutput
        var pdfPathOutput = Path.Combine(Path.GetDirectoryName(pdfFilePath), $"{Path.GetFileNameWithoutExtension(pdfFilePath)}_{student.Handle}{Path.GetExtension(pdfFilePath)}");

        // Doc ID
        var id = $"solidify_{ Guid.NewGuid().ToString()}";

        var docInfo = new DocInfo(student,trainer,trainingName,trainingDate, id);

        try
        {
            var pdfService = new PdfService();

            pdfService.InjectNameIntoPdf(docInfo, pdfFilePath, pdfPathOutput);

            Console.WriteLine("Name injected successfully into the PDF.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static string GetPdfPathOutput(string? pdfPath, string userNameNormalized)
    {
        if (pdfPath == null)
        {
            throw new ArgumentNullException(nameof(pdfPath));
        }

        var pdfPathOutput = Path.Combine(Path.GetDirectoryName(pdfPath), $"{Path.GetFileNameWithoutExtension(pdfPath)}_{userNameNormalized}{Path.GetExtension(pdfPath)}");
        return pdfPathOutput;
    }
}