using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Crypto.Prng;


public class PdfService
{
    public void InjectNameIntoPdf( DocInfo docInfo, string pdfPath, string pdfPathOutput)
    {
        Console.WriteLine("StampName: " + docInfo.StampName);

        string tempFile;

        switch (docInfo.StampName.ToLower())
        {
            case "solidify_training_v1":
                tempFile = InjectNameV1(docInfo, pdfPath);
                break;
            case "solidify_training_v2":
                tempFile = InjectNameV2(docInfo, pdfPath);
                break;
            default:
                tempFile = InjectNameDefault(docInfo, pdfPath);
                break;
        }

        if (File.Exists(pdfPathOutput))
        {
            File.Delete(pdfPathOutput);
        }

        File.Move(tempFile, pdfPathOutput);
    }

    string InjectNameDefault(DocInfo docInfo, string pdfPath)
    {
        var student = docInfo.Student;
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

    string InjectNameV1(DocInfo docInfo, string pdfPath)
    {
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
                    new Position (297.75f,380,0),
                    35, // Font Size
                    BaseFont.TIMES_ITALIC,
                    new BaseColor(64, 119, 142) // Green elegant
                );

                //Name
                var maxLength = "012345678901234567890123456789".Length;
                var text = docInfo.Student.Name;
                if (text.Length > maxLength)
                {
                    throw new ArgumentException($"Student name is longer than {maxLength} characters");
                }

                stamp.Text = text;
                StampText(canvas, stamp);

                //Handle and Company
                stamp.Text = $"@{docInfo.Student.Handle} ({docInfo.Student.Company})";
                stamp.FontSize -= 15;
                stamp.Position.Y-= stamp.FontSize + 3; // Second line
                StampText(canvas, stamp);

                // Training Name
                stamp.Color = BaseColor.DARK_GRAY;
                stamp.Text = $"{docInfo.CourseName}";
                stamp.Position.Y -= stamp.FontSize * 2; // Third line
                StampText(canvas, stamp);

                // Training Date
                stamp.Text = $"{docInfo.CourseDate}";
                stamp.FontSize -= 5;
                stamp.Position.Y -= stamp.FontSize + 3; // Fourth line
                StampText(canvas, stamp);

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
                stamp.Text = docInfo.Trainer.Name;
                stamp.Position = new Position(421.125f, 200f, 0f);
                StampText(canvas,stamp);

                // Handle Trainer
                stamp.Text = $"@{docInfo.Trainer.Handle}";
                stamp.Position.Y -= stamp.FontSize; // Second line
                stamp.FontSize = 12;
                StampText(canvas, stamp);

                // GUID
                stamp = new Stamp(
                    docInfo.Id.ToString(),
                    new Position (297.75f,0,0),
                    6, // Font Size
                    BaseFont.COURIER,
                    BaseColor.BLACK
                );
                stamp.Position.Y = 12;
                StampText(canvas, stamp);

                //Close canvas
                canvas.EndText();
            }
        }

        return tempFile;
    }

    string InjectNameV2(DocInfo docInfo, string pdfPath)
    {
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
                    new Position (297.75f,380,0),
                    35, // Font Size
                    BaseFont.TIMES_ITALIC,
                    new BaseColor(64, 119, 142) // Green elegant
                );

                //Name
                var maxLength = "012345678901234567890123456789".Length;
                var text = docInfo.Student.Name;
                if (text.Length > maxLength)
                {
                    throw new ArgumentException($"Student name is longer than {maxLength} characters");
                }

                stamp.Text = text;
                StampText(canvas, stamp);

                //Handle and Company
                stamp.Text = string.IsNullOrWhiteSpace(docInfo.Student.Handle) ? string.Empty : $"@{docInfo.Student.Handle}";
                stamp.Text += string.IsNullOrWhiteSpace(docInfo.Student.Company) ? string.Empty : $" ({docInfo.Student.Company})";
                // stamp.Text = $"@{docInfo.Student.Handle} ({docInfo.Student.Company})";
                stamp.FontSize -= 15;
                stamp.Position.Y-= stamp.FontSize + 3; // Second line
                StampText(canvas, stamp);

                // Training Name
                stamp.Color = BaseColor.DARK_GRAY;
                stamp.Text = $"{docInfo.CourseName}";
                stamp.Position.Y -= stamp.FontSize * 2; // Third line
                StampText(canvas, stamp);

                // Training Date
                stamp.Text = $"{docInfo.CourseDate}";
                stamp.FontSize -= 5;
                stamp.Position.Y -= stamp.FontSize + 3; // Fourth line
                StampText(canvas, stamp);

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
                stamp.Text = docInfo.Trainer.Name;
                stamp.Position = new Position(421.125f, 200f, 0f);
                StampText(canvas,stamp);

                // Handle Trainer
                stamp.Text = $"@{docInfo.Trainer.Handle}";
                stamp.Position.Y -= stamp.FontSize; // Second line
                stamp.FontSize = 12;
                StampText(canvas, stamp);

                // GUID
                stamp = new Stamp(
                    docInfo.Id.ToString(),
                    new Position (297.75f,0,0),
                    6, // Font Size
                    BaseFont.COURIER,
                    BaseColor.BLACK
                );
                stamp.Position.Y = 12;
                StampText(canvas, stamp);

                //Close canvas
                canvas.EndText();
            }
        }

        return tempFile;
    }

    void StampText(PdfContentByte canvas, Stamp stamp)
    {
        if(String.IsNullOrWhiteSpace(stamp.Text))
            return;

        BaseFont bfName = BaseFont.CreateFont(stamp.FontName, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        var color = new BaseColor(stamp.Color.R, stamp.Color.G, stamp.Color.B);


        canvas.SetFontAndSize(bfName, stamp.FontSize);
        canvas.SetColorFill(color);
        
        canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, stamp.Text, stamp.Position.X, stamp.Position.Y, stamp.Position.Rotation);
    }
}

