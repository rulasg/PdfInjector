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

        // create user
        var student = StudentService.GetStudent(studentHandle);
        var pdfService = new PdfService();

        // Create pdfPathOutput
        var pdfPathOutput = Path.Combine(Path.GetDirectoryName(pdfFilePath), $"{Path.GetFileNameWithoutExtension(pdfFilePath)}_{student.Handle}{Path.GetExtension(pdfFilePath)}");

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