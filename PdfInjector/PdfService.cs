using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class PdfService
{
    public void InjectNameIntoPdf(Student student, string pdfPath)
    {
        string tempFile = Path.GetTempFileName();
        using (PdfReader reader = new PdfReader(pdfPath))
        {
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(tempFile, FileMode.Create)))
            {
                PdfContentByte canvas = stamper.GetOverContent(1);
                ColumnText.ShowTextAligned(
                    canvas,
                    Element.ALIGN_MIDDLE,
                    new Phrase(student.Name),
                    36, 540, 0
                );
            }
        }
        File.Delete(pdfPath);
        File.Move(tempFile, pdfPath);
    }
}