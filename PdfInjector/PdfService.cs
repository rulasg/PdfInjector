using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.IO.Font.Constants;

public class PdfService
{
    public void InjectNameIntoPdf(Student student, string pdfPath, string pdfPathOutput)
    {
        string tempFile = System.IO.Path.GetTempFileName();

        using (PdfWriter writer = new PdfWriter(tempFile))
        {
            using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdfPath), writer))
            {
                Document doc = new Document(pdfDoc);

                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                Paragraph para = new Paragraph(student.Name)
                    .SetFont(font)
                    .SetFontSize(50)
                    .SetTextAlignment(TextAlignment.CENTER);

                Rectangle pageSize = pdfDoc.GetPage(1).GetPageSize();
                float x = pageSize.GetWidth() / 2;
                float y = pageSize.GetHeight() / 2;

                doc.ShowTextAligned(para, x, y, 1, TextAlignment.CENTER, VerticalAlignment.MIDDLE, 0);

                doc.Close();
            }
        }

        if (File.Exists(pdfPathOutput))
        {
            File.Delete(pdfPathOutput);
        }

        File.Move(tempFile, pdfPathOutput);
    }
}