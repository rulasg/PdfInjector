using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


public class PdfService
{
    public void InjectNameIntoPdf(Student student, string pdfPath, string pdfPathOutput)
    {

        string tempFile = InjectName2(student, pdfPath);

        if (File.Exists(pdfPathOutput))
        {
            File.Delete(pdfPathOutput);
        }

        File.Move(tempFile, pdfPathOutput);
    }

    string InjectName(Student student, string pdfPath){

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

        return tempFile;
    }

    string InjectName2(Student student, string pdfPath)
    {
        string tempFile = Path.GetTempFileName();

        using (PdfReader reader = new PdfReader(pdfPath))
        {
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(tempFile, FileMode.Create)))
            {
                PdfContentByte canvas = stamper.GetOverContent(1);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetFontAndSize(bf, 50);
                canvas.BeginText();

                Rectangle pageSize = reader.GetPageSize(1);
                float x = pageSize.Width / 2;
                float y = pageSize.Height / 2;

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, student.Name, x, y, 0);
                canvas.EndText();
            }
        }

        return tempFile;
    }
}