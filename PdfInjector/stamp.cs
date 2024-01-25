
using iTextSharp.text;


/// <summary>
/// Represents a stamp that can be applied to a PDF document.
/// </summary>
public class Stamp{
    public string? Text {get; set;}
    public Position Position {get; set;}
    public int FontSize {get; set;}
    public string FontName {get; set;}
    public BaseColor Color   {get; set;}

    public Stamp(string? text, Position position, int fontSize, string fontName, BaseColor color)
    {
        Text = text;
        Position = position;
        FontSize = fontSize;
        FontName = fontName;
        Color = color;
    }

}

/// <summary>
/// Represents the position of a stamp on a document.
/// </summary>
public class Position
{
    /// <summary>
    /// Gets or sets the X coordinate of the stamp position.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the stamp position.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Gets or sets the rotation angle of the stamp position.
    /// </summary>
    public float Rotation { get; set; }

    //ctor
    public Position(float x, float y, float rotation)
    {
        X = x;
        Y = y;
        Rotation = rotation;
    }
}
