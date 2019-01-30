#r "System.Drawing"

using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

private static readonly Size size = new SixLabors.Primitives.Size(EnvAsInt("ImageResize-Width"), EnvAsInt("ImageResize-Height"));

public static void Run(Stream original, Stream resized)
{
    using (Image<PixelFormats.Rgba32> image = Image.Load(original))
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