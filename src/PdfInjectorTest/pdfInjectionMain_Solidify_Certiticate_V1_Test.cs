using Xunit;
using System.IO;
using System;

namespace PdfInjectorTest
{
    public class PdfInjectionMain_Solidify_Certiticate_V1
    {
        [Fact]
        public void Test_solidify_certificate_v1_SUCCESS()
        {
        //    var pdftemplate= "/Users/rulasg/code/PdfInjector/README.pdf" ;
        //    var pdftemplate= "solidify_certificate_v1.pdf";
           var pdftemplate= "Pdftemplate.pdf";
           
           var result = "result_V1.pdf";

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
                "--stampName", "solidify_training_v1"
            };

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            PdfInjector.Program.Main(args);

            // Assert
            Assert.True(File.Exists(result));
            
            string output = stringWriter.ToString();
            Assert.Contains("Name injected successfully into the PDF.", output);
        }
    }
}