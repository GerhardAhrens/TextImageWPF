
# TextImageWPF

![NET](https://img.shields.io/badge/NET-10-red.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2022](https://img.shields.io/badge/Visual%20Studio-2022-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.1-yellow.svg)

Das Beispiel zeigt, wie ein Bitmap mit einem Custom Text (z.B. png) als Datei oder als Byte-Array erstellt werden kann.

## Erstellen eines Text-Bitmaps (<r>als png Datei</r>), zentriert in einem Rechteck.
```csharp
TextImageRenderer.RectangleTextToFile(@"C:\Temp\Bild_RectangleText.png", 
                                      text: "Hallo WPF!\nZentrierter Text",
                                      width: 600, 
                                      height: 300, 
                                      fontSize: 36, 
                                      fontWeight: FontWeights.Bold, 
                                      textColor: Brushes.DarkBlue, 
                                      backgroundColor: Brushes.LightGray);
```

<img src="DemoA.png" style="width:250px;"/>

## Erstellen eines Text-Bitmaps (<r>als png Datei</r>), mit einem Text als Kreis.
```csharp
TextImageRenderer.CircleTextToFile(@"C:\Temp\Bild_CircleText.png", 
                                   text: "123", 
                                   diameter: 160,
                                   circleBrush: Brushes.DarkSlateBlue);
```

<img src="DemoB.png" style="width:150px;"/>

# Versionshistorie
![Version](https://img.shields.io/badge/Version-1.0.2026.1-yellow.svg)
- Migration auf NET 10
