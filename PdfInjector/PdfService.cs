using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Crypto.Prng;


public class PdfService
{
    public void InjectNameIntoPdf(Person student, string pdfPath, string pdfPathOutput)
    {
        string templateName = Path.GetFileNameWithoutExtension(pdfPath);
        Console.WriteLine("Template PDF: " + templateName);

        string tempFile;

        switch (templateName)
        {
            case "solidify_certificate_v1":
                tempFile = InjectNameV1(student, pdfPath);
                break;
            default:
                tempFile = InjectNameDefault(student, pdfPath);
                break;
        }

        if (File.Exists(pdfPathOutput))
        {
            File.Delete(pdfPathOutput);
        }

        File.Move(tempFile, pdfPathOutput);
    }

    string InjectNameDefault(Person student, string pdfPath)
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

    string InjectNameV1(Person student, string pdfPath)
    {

        BaseFont bfName = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont bfBold = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        string tempFile = Path.GetTempFileName();

        using (PdfReader reader = new PdfReader(pdfPath))
        {
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(tempFile, FileMode.Create)))
            {
                // Get the size of the page
                // Rectangle pageSize = reader.GetPageSize(1);
                // float middle_X = pageSize.Width / 2;
                // float middle_y = pageSize.Height / 2;

                PdfContentByte canvas = stamper.GetOverContent(1);
                canvas.BeginText();

                // Paragraph 1 Base Stamp
                var stamp = new Stamp(
                    "empty",
                    new Position (297.75f,400,0),
                    35, // Font Size
                    BaseFont.TIMES_ITALIC,
                    new BaseColor(64, 119, 142) // Green elegant
                );

                //Name
                stamp.Text = student.Name;
                StampText(canvas, stamp);

                //Handle
                if(student.Handle != null)
                {
                    stamp.Text = "@" + student.Handle;
                    stamp.Position.Y-= stamp.FontSize; // Second line
                    stamp.FontSize -= 10;
                    StampText(canvas, stamp);
                }

                // Trainer signature SECTION
                stamp = new Stamp(
                    "empty",
                    new Position (0,0,0),
                    0, // Font Size
                    BaseFont.TIMES_BOLDITALIC,
                    BaseColor.BLACK
                );

                // Trainer Name
                stamp.FontSize = 16;
                stamp.Text = "Raul gonzalez";
                stamp.Position = new Position(421.125f, 200f, 0f);
                StampText(canvas,stamp);

                // Handle Trainer
                stamp.Text = "@rulasg25";
                stamp.Position.Y -= stamp.FontSize; // Second line
                stamp.FontSize = 12;
                StampText(canvas, stamp);


                //Close canvas
                canvas.EndText();
            }
        }

        return tempFile;
    }

    void StampText(PdfContentByte canvas, Stamp stamp)
    {
        BaseFont bfName = BaseFont.CreateFont(stamp.FontName, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        var color = new BaseColor(stamp.Color.R, stamp.Color.G, stamp.Color.B);


        canvas.SetFontAndSize(bfName, stamp.FontSize);
        canvas.SetColorFill(color);
        
        canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, stamp.Text, stamp.Position.X, stamp.Position.Y, stamp.Position.Rotation);
    }
}

