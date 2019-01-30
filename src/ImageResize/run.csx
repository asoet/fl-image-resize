#r "System.Drawing"

using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.ImageSharp.PixelFormats;

private static readonly Size size = new Size(EnvAsInt("ImageResize-Width"), EnvAsInt("ImageResize-Height"));

public static void Run(Stream original, Stream resized)
{
    using (Image<Rgba32> image = Image.Load(original))
    {
        image.Mutate(x => x
                .Resize(new ResizeOptions()
                {
                    Size = size,
                    Mode = ResizeMode.Crop
                }));
        image.SaveAsPng(resized);
    }
}


private static int EnvAsInt(string name) => int.Parse(Env(name));
private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);