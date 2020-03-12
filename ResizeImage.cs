using System.Drawing;
using System.Drawing.Drawing2D;

namespace HDtoSD
{
    class ResizeImage
    {
        //Resizes a image given its path
        public static void Resize(string inputPath, string outputFile, float scale)
        {
            using var srcImage = Image.FromFile(inputPath);

            //Checks if the image will have a valid resolution after resizing
            int newWidth = (int)(srcImage.Width * scale);
            int newHeight = (int)(srcImage.Height * scale);
            if (newWidth < 1 || newHeight < 1)
            {
                //If the image is a 1x1 it will create a SD version
                //Creates a copy of the file with sd naming
                srcImage.Save(outputFile);
            }
            else
            {
                using var newImage = new Bitmap(newWidth, newHeight);
                using var graphics = Graphics.FromImage(newImage);
                //Sets quality parameters
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //Resizes the image
                graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));

                //Saves the resized image
                newImage.Save(outputFile);
            }
        }
    }
}
