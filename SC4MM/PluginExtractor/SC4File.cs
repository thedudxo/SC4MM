using System;
using System.IO;

namespace SC4MM.ZipFileIntake
{
    public class SC4File
    {
        public enum Extension {
            SC4Desc,
            SC4Lot,
            SC4Model,
            bat,
            txt,
            html,
            jpg,
            gif
        }

        Extension extension;

        string path;

        public SC4File(string path)
        {
            this.path = path;
            SetFileExtension();
        }

        public Extension GetExtension()
        {
            return extension;
        }

        private void SetFileExtension()
        {
            string fileExtension = path.Substring(path.LastIndexOf('.'));
            switch (fileExtension)
            {
                case ".SC4Lot":
                    extension = Extension.SC4Lot;
                    break;

                case ".SC4Desc":
                    extension = Extension.SC4Desc;
                    break;

                case ".SC4Model":
                    extension = Extension.SC4Model;
                    break;

                case ".bat":
                    extension = Extension.bat;
                    break;

                case ".txt":
                    extension = Extension.txt;
                    break;

                case ".html":
                    extension = Extension.html;
                    break;

                case ".jpg":
                    extension = Extension.jpg;
                    break;

                case ".gif":
                    extension = Extension.gif;
                    break;

                default:
                    throw new ArgumentException($"{fileExtension} is not a recognised file type");
            }
        }


    }
}