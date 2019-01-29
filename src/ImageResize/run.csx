#r "System.Drawing"

using System;
using System.Drawing;
using ImageProcessor;
using ImageProcessor.Imaging;

private static readonly Size size = new Size(EnvAsInt("ImageResize-Width"), EnvAsInt("ImageResize-Height"));

public static void Run(Stream original, Stream resized)
{
    using (var imageFactory = new ImageFactory())
    {
        imageFactory
            .Load(original)
            .Resize(new ResizeLayer(size, ResizeMode.Crop))
            .Save(resized);
    }
}


private static int EnvAsInt(string name) => int.Parse(Env(name));
private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);