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

            var userHandle = "ghHandle";
            var pdfName = "Pdftemplate";

            // Arrange
            // string[] args = new string[] { "userName", "/Users/rulasg/code/PdfInjector/README.pdf" };
            string[] args = new string[] { userHandle, pdfName+".pdf" };
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            PdfInjector.Program.Main(args);

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains("Name injected successfully into the PDF.", output);

            var userNameNormalized = userHandle.Replace(" ", "_");
            var outputFile = pdfName+"_"+ userHandle+".pdf";
            Assert.True(File.Exists(outputFile));
        }
    }
}