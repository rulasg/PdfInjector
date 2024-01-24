using Xunit;
using System.IO;
using System;

namespace PdfInjectorTest
{
    public class UnitTestPdfInjectoMain
    {
        [Fact]
        public void TestPdfInjectorMain()
        {

            var userName = "rulasg";
            var pdfName = "Pdftemplate";

            // Arrange
            // string[] args = new string[] { "userName", "/Users/rulasg/code/PdfInjector/README.pdf" };
            string[] args = new string[] { userName, pdfName+".pdf" };
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            PdfInjector.Program.Main(args);

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains("Name injected successfully into the PDF.", output);

            var outputFile = pdfName+"_"+userName+".pdf";

            Assert.True(File.Exists(outputFile));
        }
    }
}