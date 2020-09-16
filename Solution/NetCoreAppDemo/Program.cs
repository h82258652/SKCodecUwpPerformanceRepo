using SkiaSharp;
using System;
using System.Diagnostics;
using System.IO;

namespace NetCoreAppDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stream = File.OpenRead("test.png"))
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

                                Console.WriteLine($"cost {stopwatch.ElapsedMilliseconds} ms");
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}