using SkiaSharp;
using System;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace UwpDemo
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///test.png"));
            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var bitmap = new SKBitmap(960, 720))
                {
                    using (var canvas = new SKCanvas(bitmap))
                    {
                        using (var codec = SKCodec.Create(stream))
                        {
                            using (var decodeBitmap = new SKBitmap(codec.Info))
                            {
                                var pointer = decodeBitmap.GetPixels();
                                codec.GetPixels(decodeBitmap.Info, pointer);

                                var dest = new SKRect(0, 0, 960, 720);

                                var stopwatch = new Stopwatch();
                                stopwatch.Start();

                                canvas.DrawBitmap(decodeBitmap, dest);

                                stopwatch.Stop();

                                await new MessageDialog($"cost {stopwatch.ElapsedMilliseconds} ms").ShowAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}