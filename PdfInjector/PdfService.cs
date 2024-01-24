using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Crypto.Prng;


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
        int fontSize = 30;
        var TextColor1 = new BaseColor(64, 119, 142);
        BaseFont bfName = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont bfHandle = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        string tempFile = Path.GetTempFileName();

        using (PdfReader reader = new PdfReader(pdfPath))
        {
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(tempFile, FileMode.Create)))
            {
                Rectangle pageSize = reader.GetPageSize(1);
                //Name
                float x = pageSize.Width / 2;
                float y = pageSize.Height / 2;

                PdfContentByte canvas = stamper.GetOverContent(1);
                canvas.BeginText();

                //Name
                canvas.SetFontAndSize(bfName, fontSize);
                canvas.SetColorFill(TextColor1);
                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, student.Name, x, y, 0);

                //Handle
                if(student.Handle != null)
                {
                    y -= fontSize;
                    canvas.SetFontAndSize(bfHandle, fontSize-10);
                    canvas.SetColorFill(TextColor1);
                    canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "@" + student.Handle, x, y, 0);
                }

                canvas.EndText();
            }
        }

        return tempFile;
    }
}