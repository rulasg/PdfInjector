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
            // Arrange
            // string[] args = new string[] { "userName", "/Users/rulasg/code/PdfInjector/README.pdf" };
            string[] args = new string[] { "userName", "Pdftemplate.pdf" };
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            PdfInjector.Program.Main(args);

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains("Name injected successfully into the PDF.", output);

            Assert.True(File.Exists("Pdftemplate_userName.pdf"));
        }
    }
}