using System;
using System.Diagnostics;
using System.IO;
using iTextSharp.text.pdf;

namespace PdfInjector;

public class Program
{
    public static void Main(string[] args)
    {
        // Parse arguments
        (string? pdfFilePath, string? pdfPathOutput, DocInfo? docInfo) = ProcessArgs(args);

        // Exit if missing parameters
        if(pdfFilePath == null || pdfPathOutput == null || docInfo == null) {
            PrintHelp();
            return ;
        }

        // Confirm that the PDF file exists
        if (!File.Exists(pdfFilePath)) { 
            Console.Error.WriteLine("Error: PDF file path not found."); 
            return ;
        }

        try
        {
            var pdfService = new PdfService();

            pdfService.InjectNameIntoPdf(docInfo, pdfFilePath, pdfPathOutput);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return ;
        }

        Console.WriteLine("Name injected successfully into the PDF.");
        return ;
    }

    static (string?, string?, DocInfo?) ProcessArgs(string[] args)
    {
        var pointer = 0;

        var pdfTemplatePath = string.Empty;
        var pdfPathOutput = string.Empty;
        var docInfo = new DocInfo();

        while (pointer < args.Length)
        {
            var arg = args[pointer].ToLower();

            switch (arg)
            {
                case "-h":
                case "--help":
                case "-?":
                    return (null,null,null);
                case "-t":
                case "--pdftemplate":
                    pdfTemplatePath = args[pointer + 1];
                    pointer += 2;
                    break;
                case "-o":
                case "--pdfoutput":
                    pdfPathOutput = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--stampname":
                case "-s":
                    docInfo.StampName = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--studentname":
                case "-sn":
                    docInfo.Student.Name = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--studenthandle":
                case "-sh":
                    docInfo.Student.Handle = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--studentcompany":
                case "-sc":
                    docInfo.Student.Company = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--trainername":
                case "-tn":
                    docInfo.Trainer.Name = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--trainerhandle":
                case "-th":
                    docInfo.Trainer.Handle = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--trainercompany":
                case "-tc":
                    docInfo.Trainer.Company = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--coursename":
                case "-cn":
                    docInfo.CourseName = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--coursedate":
                case "-cd":
                    docInfo.CourseDate = args[pointer + 1];
                    pointer += 2;
                    break;
                case "--id":
                case "-i":
                    docInfo.Id = args[pointer + 1];
                    pointer += 2;
                    break;

                default:
                    Console.Error.WriteLine($"Error: Unknown argument [ {arg} ]");
                    return (null,null, null);
            } // switch
        } // While

        if (docInfo.Test() && pdfPathOutput != string.Empty && pdfTemplatePath != string.Empty){
            return (pdfTemplatePath, pdfPathOutput, docInfo);
        }

        Console.Error.WriteLine("Error: Missing arguments");
        return (null,null,null);
    }

    static void PrintHelp(){
        Console.WriteLine("Usage: pdfInjector <args> All arguments are required");
        Console.WriteLine("Args:");
        Console.WriteLine("    --help | -h      | -?   : Prints this help message");
        Console.WriteLine();
        Console.WriteLine("    --stampname      | -s   : Stamp name (e.g. 'solidify_training_v2')");
        Console.WriteLine("    --pdftemplate    | -t   : Path to the PDF template");
        Console.WriteLine("    --pdfoutput      | -o   : Path to the output PDF file");
        Console.WriteLine();
        Console.WriteLine("    --studentname    | -sn  : Student name");
        Console.WriteLine("    --studenthandle  | -sh  : Student handle");
        Console.WriteLine("    --studentcompany | -sc  : Student company");
        Console.WriteLine("    --trainername    | -tn  : Trainer name");
        Console.WriteLine("    --trainerhandle  | -th  : Trainer handle");
        Console.WriteLine("    --trainercompany | -tc  : Trainer company");
        Console.WriteLine("    --coursename     | -cn  : Course name");
        Console.WriteLine("    --coursedate     | -cd  : Course date");
        Console.WriteLine("    --id             | -i   : Certificate identifier");
        Console.WriteLine();
    }
}