using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using System.Configuration;

namespace Infrastructure.Baser
{
    public static class EShopFileUploader
    {
        //企业入住.公司照片
        public static string[] AllowedCompanyPhotoTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedCompanyPhotoTypes"].Split(spliter);
            }
        }
        public static int CompanyPhotoMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["companyPhotoMaxSize"]);
            }
        }

        //<!-- product photo -->
        public static string[] AllowedProductPhotoTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedProductPhotoTypes"].Split(spliter);
            }
        }
        public static int ProductPhotoMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["productPhotoMaxSize"]);
            }
        }
        //<!-- product photo -->
        public static string[] AllowedEShopPhotoTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedEShopPhotoTypes"].Split(spliter);
            }
        }
        public static int EShopPhotoMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["eShopPhotoMaxSize"]);
            }
        }
        //<!-- flash AD -->
        public static string[] AllowedFlashADTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedFlashADTypes"].Split(spliter);
            }
        }
        public static int FlashADMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["flashADMaxSize"]);
            }
        }
        //<!-- 3v Photo -->
        public static string[] Allowed3VPhotoTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowed3VPhotoTypes"].Split(spliter);
            }
        }
        public static int ThreeVPhotoMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["3VPhotoMaxSize"]);
            }
        }
        //<!-- logo -->
        public static string[] AllowedLogoTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedLogoTypes"].Split(spliter);
            }
        }
        public static int LogoMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["logoMaxSize"]);
            }
        }
        //<!-- flash logo -->
        public static string[] AllowedFlashLogoTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedFlashLogoTypes"].Split(spliter);
            }
        }
        public static int FlashLogoMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["flashLogoMaxSize"]);
            }
        }

        //flash logo
        public static bool UploadFile(FileUpload fileUpload,
                                        string path,
                                        ref string saveName,
                                        ref string fileName,
                                        ref string errorMessage,string key)
        {
            int maxSize = 0;
            string[] allowedTypes = null;
            switch (key)
            {
                case "flashLogo"://flashLogo
                    maxSize = EShopFileUploader.FlashLogoMaxSize;
                    allowedTypes = EShopFileUploader.AllowedFlashLogoTypes;
                    break;
                case "eShopPhoto"://eshopPhoto
                    maxSize = EShopFileUploader.EShopPhotoMaxSize;
                    allowedTypes = EShopFileUploader.AllowedEShopPhotoTypes;
                    break;
                case "logo"://logo
                    maxSize = EShopFileUploader.LogoMaxSize;
                    allowedTypes = EShopFileUploader.AllowedLogoTypes;
                    break;
                case "3vPhoto"://3v Photo
                    maxSize = EShopFileUploader.ThreeVPhotoMaxSize;
                    allowedTypes = EShopFileUploader.Allowed3VPhotoTypes;
                    break;
                case "flashAD"://flash AD
                    maxSize = EShopFileUploader.FlashADMaxSize;
                    allowedTypes = EShopFileUploader.AllowedFlashADTypes;
                    break;
                case "productPhoto"://product photo
                    maxSize = EShopFileUploader.ProductPhotoMaxSize;
                    allowedTypes = EShopFileUploader.AllowedProductPhotoTypes;
                    break;
                //企业入住.公司照片.companyApply
                case "companyPhoto":
                    maxSize = EShopFileUploader.CompanyPhotoMaxSize;
                    allowedTypes = EShopFileUploader.AllowedCompanyPhotoTypes;
                    break;
                default:
                    break;
            }
            return UploadFiles(fileUpload, path, ref saveName, ref fileName, ref errorMessage,
                maxSize, allowedTypes);
        }
        private static bool UploadFiles(FileUpload fileUpload,
                                        string path,
                                        ref string saveName,
                                        ref string fileName,
                                        ref string errorMessage,
                                        int MaxSize,
                                        string[] AllowedTypes)
        {
            if (fileUpload.HasFile)
            {
                bool isFileOK = false;
                fileName = fileUpload.FileName;
                string fileExtension =
                    fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
                string fileType = fileUpload.PostedFile.ContentType.ToLower();
                //Check file size
                if (fileUpload.PostedFile.ContentLength > MaxSize)
                {
                    errorMessage = "文件不能大于:" + MaxSize;
                    return false;
                }
                //Check if file type is valid
                foreach (string s in AllowedTypes)
                {
                    if (s == fileType)
                        isFileOK = true;
                }
                if (isFileOK)
                {
                    //Build path
                    saveName = Guid.NewGuid() + fileExtension;
                    string finalFilename = path + saveName;

                    //Save file
                    FileInfo oldFile = new FileInfo(finalFilename);
                    if (oldFile.Exists)
                    {
                        oldFile.Attributes = FileAttributes.Normal;
                        oldFile.Delete();
                    }
                    fileUpload.PostedFile.SaveAs(finalFilename);
                    //sccuss
                    return true;
                }
                else
                {
                    errorMessage = "非允许文件类型";
                    return false;
                }
            }
            else
            {
                errorMessage = "文件为空";
                return false;
            }
        }

    }
}
