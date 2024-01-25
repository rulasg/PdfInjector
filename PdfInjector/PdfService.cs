using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Crypto.Prng;


public class PdfService
{
    public void InjectNameIntoPdf(Student student, string pdfPath, string pdfPathOutput)
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

    string InjectNameDefault(Student student, string pdfPath)
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

    string InjectNameV1(Student student, string pdfPath)
    {
        int fontSize = 30;
        var TextColorGreen1 = new BaseColor(64, 119, 142);
        var TextColorBlack = BaseColor.BLACK;

        BaseFont bfName = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont bfBold = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        string tempFile = Path.GetTempFileName();

        using (PdfReader reader = new PdfReader(pdfPath))
        {
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(tempFile, FileMode.Create)))
            {
                Rectangle pageSize = reader.GetPageSize(1);
                //Name
                float middle_X = pageSize.Width / 2;
                float middle_y = pageSize.Height / 2;

                PdfContentByte canvas = stamper.GetOverContent(1);
                canvas.BeginText();

                //Name
                var name_X = middle_X;
                var name_Y = 400;
                var name_fontSize = 35;

                canvas.SetFontAndSize(bfName, name_fontSize);
                canvas.SetColorFill(TextColorGreen1);
                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, student.Name, name_X, name_Y, 0);

                //Handle
                if(student.Handle != null)
                {
                    var handle_Y = name_Y - fontSize;
                    var handle_X = middle_X;
                    var handle_fontSize = fontSize - 10;

                    canvas.SetFontAndSize(bfName, handle_fontSize);
                    canvas.SetColorFill(TextColorGreen1);
                    canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "@" + student.Handle, handle_X, handle_Y, 0);
                }

                // Trainer
                var trainer_Text = "Raúl González";
                var trainer_handle = "rulasg";
                var trainer_Y = 195;
                var trainer_X = middle_y;
                var trainer_fontSize = 16;
                var trainer_Color = TextColorBlack;

                canvas.SetFontAndSize(bfBold, trainer_fontSize);
                canvas.SetColorFill(trainer_Color);
                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, trainer_Text, trainer_X, trainer_Y, 0);


                // Handle Trainer
                var trainer_handle_Y = trainer_Y - 12;
                var trainer_handle_X = middle_y;
                var trainer_handle_fontSize = 12;
                var trainer_handle_Color = TextColorBlack;

                canvas.SetFontAndSize(bfBold, trainer_handle_fontSize);
                canvas.SetColorFill(trainer_handle_Color);
                
                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "@" + trainer_handle, trainer_handle_X, trainer_handle_Y, 0);

                //Close canvas
                canvas.EndText();
            }
        }

        return tempFile;
    }
}