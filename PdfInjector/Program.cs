using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: my-dotnet-app <student name> <pdf file path>");
                return;
            }

            var studentName = args[0];
            var pdfFilePath = args[1];

            var student = new Student { Name = studentName };
            var pdfService = new PdfService();

            try
            {
                pdfService.InjectNameIntoPdf(student, pdfFilePath);
                Console.WriteLine("Name injected successfully into the PDF.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}