using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;

namespace DF2026.PluginTemplate
{
    internal static class IconLoader
    {
        private const string ResourceName =
            "DF2026.PluginTemplate.component_icon.png";

        private static readonly Lazy<Bitmap> CachedIcon =
            new Lazy<Bitmap>(CreateIcon);

        public static Bitmap ComponentIcon => CachedIcon.Value;

        private static Bitmap CreateIcon()
        {
            using Stream? stream =
                Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream(ResourceName);

            if (stream is null)
            {
                return CreateFallbackIcon();
            }

            using Image source = Image.FromStream(stream);
            var canvas = new Bitmap(24, 24);
            using Graphics graphics = Graphics.FromImage(canvas);
            graphics.Clear(Color.Transparent);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            float scale = Math.Min(
                24f / source.Width,
                24f / source.Height);
            int width = Math.Max(1, (int)Math.Round(source.Width * scale));
            int height = Math.Max(1, (int)Math.Round(source.Height * scale));
            int x = (24 - width) / 2;
            int y = (24 - height) / 2;
            graphics.DrawImage(source, x, y, width, height);
            return canvas;
        }

        private static Bitmap CreateFallbackIcon()
        {
            var icon = new Bitmap(24, 24);
            using Graphics graphics = Graphics.FromImage(icon);
            graphics.Clear(Color.FromArgb(25, 28, 38));
            using var brush = new SolidBrush(Color.White);
            using var font = new Font(
                FontFamily.GenericSansSerif,
                7f,
                FontStyle.Bold,
                GraphicsUnit.Pixel);
            graphics.DrawString("ML", font, brush, 6f, 7f);
            return icon;
        }
    }
}
