using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedPanel : Panel
{
    private int _borderRadius = 10;
    private int _borderWidth = 1;
    private Color _borderColor = Color.Black;

    public RoundedPanel()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true);

        UpdateRegion();
    }

    [Category("Appearance")]
    [Description("The radius of the rounded corners in pixels.")]
    [DefaultValue(10)]
    public int BorderRadius
    {
        get => _borderRadius;
        set
        {
            value = Math.Max(0, value);

            if (_borderRadius == value)
                return;

            _borderRadius = value;

            UpdateRegion();
            Invalidate();
        }
    }

    [Category("Appearance")]
    [Description("The width of the border in pixels.")]
    [DefaultValue(1)]
    public int BorderWidth
    {
        get => _borderWidth;
        set
        {
            value = Math.Max(0, value);

            if (_borderWidth == value)
                return;

            _borderWidth = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    [Description("The color of the border.")]
    [DefaultValue(typeof(Color), "Black")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            if (_borderColor == value)
                return;

            _borderColor = value;
            Invalidate();
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateRegion();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_borderWidth <= 0)
            return;

        Rectangle bounds = ClientRectangle;

        float halfWidth = _borderWidth / 2f;

        bounds.Inflate(
            -(int)Math.Ceiling(halfWidth),
            -(int)Math.Ceiling(halfWidth));

        if (bounds.Width <= 0 || bounds.Height <= 0)
            return;

        int radius = Math.Min(
            _borderRadius,
            Math.Min(bounds.Width, bounds.Height) / 2);

        using (GraphicsPath path = CreateRoundedPath(bounds, radius))
        using (Pen pen = new Pen(_borderColor, _borderWidth))
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            e.Graphics.DrawPath(pen, path);
        }
    }

    private void UpdateRegion()
    {
        if (Width <= 0 || Height <= 0)
            return;

        using (GraphicsPath path = CreateRoundedPath(
            ClientRectangle,
            _borderRadius))
        {
            Region oldRegion = Region;

            Region = new Region(path);

            oldRegion?.Dispose();
        }
    }

    private static GraphicsPath CreateRoundedPath(
        Rectangle bounds,
        int radius)
    {
        GraphicsPath path = new GraphicsPath();

        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        int diameter = radius * 2;

        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();

        return path;
    }
}