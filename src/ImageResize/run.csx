#r "Microsoft.WindowsAzure.Storage"

using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.ImageSharp.PixelFormats;
using Microsoft.WindowsAzure.Storage.Blob;


private static readonly Size size = new Size(EnvAsInt("ImageResize-Width"), EnvAsInt("ImageResize-Height"));

public static void Run(Stream original, CloudBlockBlob resized)
{
     System.IO.Stream stream = new System.IO.MemoryStream();
    using (Image<Rgba32> image = Image.Load(original))
    {
        image.Mutate(x => x
                .Resize(new ResizeOptions()
                {
                    Size = size,
                    Mode = ResizeMode.Crop
                }));
        image.SaveAsJpeg(stream);                
        stream.Position = 0;

        resized.Properties.ContentType = "image/jpeg";
        resized.UploadFromStreamAsync(stream);
    }

}


private static int EnvAsInt(string name) => int.Parse(Env(name));
private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);