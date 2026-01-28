//-----------------------------------------------------------------------
// <copyright file="TextImageRenderer.cs" company="Lifeprojects.de">
//     Class: TextImageRenderer
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>Gerhard Ahrens@Lifeprojects.de</email>
// <date>12.01.2026 10:36:52</date>
//
// <summary>
// Klasse zum erstellen von Images mit einem Text in einem Rechteck oder Kreis
// </summary>
//-----------------------------------------------------------------------

namespace TextImageWPF.Core
{
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    public class RectangleTextOption
    {
        public RectangleTextOption(string filePath, string text,
            int width, int height, double fontSize = 32, FontWeight? fontWeight = null,
            Brush textColor = null, Brush backgroundColor = null, string fontFamily = "Segoe UI")
        {
            this.FilePath = filePath;
            this.Text = text;
            this.Width = width;
            this.Height = height;
            this.FontWeight = fontWeight;
            this.TextColor = textColor;
            this.BackgroundColor = backgroundColor;
            this.FontFamily = fontFamily;
        }

        public RectangleTextOption(string filePath, string text, int width, int height)
        {
            this.FilePath = filePath;
            this.Text = text;
            this.Width = width;
            this.Height = height;
            this.FontWeight = null;
            this.TextColor = null;
            this.BackgroundColor = null;
            this.FontFamily = "Segoe UI";
        }

        public string FilePath { get; private set; }
        public string Text { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double FontSize { get; private set; }
        public FontWeight? FontWeight { get; private set; }
        public Brush TextColor { get; private set; }
        public Brush BackgroundColor { get; private set; }
        public string FontFamily { get; private set; }
    }

    public class CircleTextOption
    {
        public CircleTextOption(string filePath,
        string text,
        int diameter,
        double maxFontSize = 64,
        double minFontSize = 10,
        Brush textBrush = null,
        Brush circleBrush = null,
        string fontFamily = "Segoe UI",
        FontWeight? fontWeight = null,
        double paddingRatio = 0.15)
        {
            this.FilePath = filePath;
            this.Text = text;
            this.Diameter = diameter;
            this.MaxFontSize = maxFontSize;
            this.MinFontSize = minFontSize;
        }

        public CircleTextOption(string filePath, string text, int diameter)
        {
            this.FilePath = filePath;
            this.Text = text;
            this.Diameter = diameter;
            this.MaxFontSize = 64;
            this.MinFontSize = 10;
        }

        public string FilePath { get; private set; }
        public string Text { get; private set; }

        public int Diameter { get; private set; }
        public double MaxFontSize { get; private set; }
        public double MinFontSize { get; private set; }
    }

    public static class TextImageRenderer
    {
        #region Text in einem Rechteck Image
        public static void RectangleTextToFile(RectangleTextOption textImageOption)
        {
            RectangleTextToFile(textImageOption.FilePath,
                textImageOption.Text,
                textImageOption.Width,
                textImageOption.Height,
                textImageOption.FontSize,
                textImageOption.FontWeight,
                textImageOption.TextColor,
                textImageOption.BackgroundColor,
                textImageOption.FontFamily);
        }

        public static void RectangleTextToFile(
            string filePath,
            string text,
            int width,
            int height,
            double fontSize = 32,
            FontWeight? fontWeight = null,
            Brush textColor = null,
            Brush backgroundColor = null,
            string fontFamily = "Segoe UI")
        {
            // Defaults
            textColor ??= Brushes.Black;
            backgroundColor ??= Brushes.White;
            fontWeight ??= FontWeights.Normal;

            // Root-Container
            var grid = new Grid
            {
                Width = width,
                Height = height,
                Background = backgroundColor
            };

            // Text
            var textBlock = new TextBlock
            {
                Text = text,
                FontSize = fontSize,
                FontWeight = fontWeight.Value,
                FontFamily = new FontFamily(fontFamily),
                Foreground = textColor,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };

            grid.Children.Add(textBlock);

            // Layout erzwingen
            grid.Measure(new Size(width, height));
            grid.Arrange(new Rect(0, 0, width, height));
            grid.UpdateLayout();

            // Rendern
            RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            bitmap.Render(grid);

            // PNG speichern
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
            }
        }

        public static byte[] RectangleTextToStream(RectangleTextOption textImageOption)
        {
            return RectangleTextToStream(textImageOption.Text,
                textImageOption.Width,
                textImageOption.Height,
                textImageOption.FontSize,
                textImageOption.FontWeight,
                textImageOption.TextColor,
                textImageOption.BackgroundColor,
                textImageOption.FontFamily);
        }

        public static byte[] RectangleTextToStream(
            string text,
            int width,
            int height,
            double fontSize = 32,
            FontWeight? fontWeight = null,
            Brush textColor = null,
            Brush backgroundColor = null,
            string fontFamily = "Segoe UI")
        {
            // Defaults
            textColor ??= Brushes.Black;
            backgroundColor ??= Brushes.White;
            fontWeight ??= FontWeights.Normal;

            // Root-Container
            var grid = new Grid
            {
                Width = width,
                Height = height,
                Background = backgroundColor
            };

            // Text
            var textBlock = new TextBlock
            {
                Text = text,
                FontSize = fontSize,
                FontWeight = fontWeight.Value,
                FontFamily = new FontFamily(fontFamily),
                Foreground = textColor,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            };

            grid.Children.Add(textBlock);

            // Layout erzwingen
            grid.Measure(new Size(width, height));
            grid.Arrange(new Rect(0, 0, width, height));
            grid.UpdateLayout();

            // Rendern
            RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            bitmap.Render(grid);

            // PNG speichern
            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
        #endregion Text in einem Rechteck Image

        #region Text in einem Kreis Image
        public static void CircleTextToFile(CircleTextOption circleTextOption)
        {
            CircleTextToFile(circleTextOption.FilePath,
                             circleTextOption.Text,
                             circleTextOption.Diameter,
                             circleTextOption.MaxFontSize,
                             circleTextOption.MinFontSize);
        }

        public static void CircleTextToFile(string filePath, string text,
            int diameter,
            double maxFontSize = 64,
            double minFontSize = 10,
            Brush textBrush = null,
            Brush circleBrush = null,
            string fontFamily = "Segoe UI",
            FontWeight? fontWeight = null,
            double paddingRatio = 0.15)
        {
            // --- Validierung ---
            if (string.IsNullOrWhiteSpace(text) == true|| text.Length > 3)
            {
                throw new ArgumentException("Text muss zwischen 1 und 3 Zeichen lang sein.");
            }

            textBrush ??= Brushes.White;
            circleBrush ??= Brushes.DodgerBlue;
            fontWeight ??= FontWeights.Bold;

            double padding = diameter * paddingRatio;
            double availableSize = diameter - (padding * 2);

            // Root
            var grid = new Grid
            {
                Width = diameter,
                Height = diameter,
                Background = Brushes.Transparent
            };

            // Kreis
            var ellipse = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Fill = circleBrush
            };

            // Text
            var textBlock = new TextBlock
            {
                Text = text,
                Foreground = textBrush,
                FontFamily = new FontFamily(fontFamily),
                FontWeight = fontWeight.Value,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.NoWrap
            };

            grid.Children.Add(ellipse);
            grid.Children.Add(textBlock);

            // 🔍 Auto Font-Scaling
            double fontSize = maxFontSize;

            while (fontSize >= minFontSize)
            {
                textBlock.FontSize = fontSize;

                textBlock.Measure(new Size(availableSize, availableSize));
                Size desired = textBlock.DesiredSize;

                if (desired.Width <= availableSize &&
                    desired.Height <= availableSize)
                {
                    break;
                }

                fontSize -= 1;
            }

            // Layout erzwingen
            grid.Measure(new Size(diameter, diameter));
            grid.Arrange(new Rect(0, 0, diameter, diameter));
            grid.UpdateLayout();

            // Rendern
            var bitmap = new RenderTargetBitmap(
                diameter,
                diameter,
                96,
                96,
                PixelFormats.Pbgra32);

            bitmap.Render(grid);

            // PNG speichern
            using FileStream stream = new FileStream(filePath, FileMode.Create);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
        }

        public static byte[] CircleTextToStream(CircleTextOption circleTextOption)
        {
            return CircleTextToStream(circleTextOption.Text,
                             circleTextOption.Diameter,
                             circleTextOption.MaxFontSize,
                             circleTextOption.MinFontSize);
        }

        public static byte[] CircleTextToStream(string text, int diameter,
            double maxFontSize = 64,
            double minFontSize = 10,
            Brush textBrush = null,
            Brush circleBrush = null,
            string fontFamily = "Segoe UI",
            FontWeight? fontWeight = null,
            double paddingRatio = 0.15)
        {
            // --- Validierung ---
            if (string.IsNullOrWhiteSpace(text) == true || text.Length > 3)
            {
                throw new ArgumentException("Text muss zwischen 1 und 3 Zeichen lang sein.");
            }

            textBrush ??= Brushes.White;
            circleBrush ??= Brushes.DodgerBlue;
            fontWeight ??= FontWeights.Bold;

            double padding = diameter * paddingRatio;
            double availableSize = diameter - (padding * 2);

            // Root
            var grid = new Grid
            {
                Width = diameter,
                Height = diameter,
                Background = Brushes.Transparent
            };

            // Kreis
            var ellipse = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Fill = circleBrush
            };

            // Text
            var textBlock = new TextBlock
            {
                Text = text,
                Foreground = textBrush,
                FontFamily = new FontFamily(fontFamily),
                FontWeight = fontWeight.Value,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.NoWrap
            };

            grid.Children.Add(ellipse);
            grid.Children.Add(textBlock);

            // 🔍 Auto Font-Scaling
            double fontSize = maxFontSize;

            while (fontSize >= minFontSize)
            {
                textBlock.FontSize = fontSize;

                textBlock.Measure(new Size(availableSize, availableSize));
                Size desired = textBlock.DesiredSize;

                if (desired.Width <= availableSize &&
                    desired.Height <= availableSize)
                {
                    break;
                }

                fontSize -= 1;
            }

            // Layout erzwingen
            grid.Measure(new Size(diameter, diameter));
            grid.Arrange(new Rect(0, 0, diameter, diameter));
            grid.UpdateLayout();

            // Rendern
            var bitmap = new RenderTargetBitmap(
                diameter,
                diameter,
                96,
                96,
                PixelFormats.Pbgra32);

            bitmap.Render(grid);

            // PNG als byte[] speichern
            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
        #endregion Text in einem Kreis Image
    }
}

