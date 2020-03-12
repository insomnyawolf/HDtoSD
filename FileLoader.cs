using System.Collections.Generic;
using System.IO;

namespace HDtoSD
{
    enum ImageFormat
    {
        Png,
        Jpg
    }
    class FileLoader
    {
        public string FolderPath { get; set; }
        public string FilterPattern { get; set; }
        public ImageFormat[] ImageFormat { get; set; }

        public FileLoader(string FolderPath, string FilterPattern, params ImageFormat[] ImageFormat)
        {
            this.FolderPath = FolderPath;
            this.FilterPattern = FilterPattern;
            this.ImageFormat = ImageFormat;
        }

        //Returns an array containing the HD image files in the skin folder
        public List<string> Load()
        {
            List<string> hdFiles = new List<string>();

            foreach (var filter in ImageFormat)
            {
                string[] filesFound = Directory.GetFiles(FolderPath, string.Format("*.{0}", filter), SearchOption.TopDirectoryOnly);
                for (int i = 0; i < filesFound.Length; i++)
                {
                    //Checks if it's an HD file
                    if (filesFound[i].Contains(FilterPattern))
                    {
                        hdFiles.Add(filesFound[i]);
                    }
                }
            }
            return hdFiles;
        }
    }
}
