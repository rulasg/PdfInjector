using Xunit;
using System.IO;
using System;

namespace PdfInjectorTest
{
    public class UnitTestPdfInjectoMain
    {
        [Fact]
        public void Test_Default_Stamp_success()
        {

           var pdftemplate= "Pdftemplate.pdf" ;
           var result = "result_Default.pdf";

            // Arrange
            string[] args = new string[] { 
                "--pdftemplate",  pdftemplate,
                "--pdfoutput", result,
                "--studentname", "John Doe Smith",
                "--studenthandle", "jdsmith",
                "--studentcompany", "Contoso",
                "--trainername", "Smart Guy",
                "--trainerhandle", "smartg",
                "--trainercompany", "Solidify",
                "--coursename", "Training GitHub for Gurus and Geniuses",
                "--coursedate", "December 2030",
                "--id", "1234567890",
                "--stampName", "OtraCosa"
            };


            if (File.Exists(result))
            {
                File.Delete(result);
            }

            // Arrange
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            PdfInjector.Program.Main(args);

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains("Name injected successfully into the PDF.", output);

            Assert.True(File.Exists(result));
        }
    }
}