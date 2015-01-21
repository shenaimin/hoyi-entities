using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace Infrastructure.Util
{
    public static class FileController
    {
        /// <summary>
        /// Gets the allowed file types.
        /// </summary>
        /// <value>The allowed file types.</value>
        private static string[] AllowedFileTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedFileTypes"].Split(spliter);
            }
        }

        private static string[] AllowedFileExtensions
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedFileExtensions"].Split(spliter);
            }
        }

        private static int FileMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["fileMaxSize"]);
            }
        }

        public static bool DeleteImage(string imgname)
        {
            if (imgname != "" && System.IO.File.Exists(imgname))
            {
                try
                {
                    System.IO.File.Delete(imgname);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static bool DeleteFile(string fileName)
        {
            if (fileName != "" && System.IO.File.Exists(fileName))
            {
                try
                {
                    System.IO.File.Delete(fileName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static string GetUpPath()
        {
            return DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Date + "/" + DateTime.Now.Hour + "/" + DateTime.Now.Minute;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="toFilePath">保存的文件的路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="errorMessage"></param>
        /// <returns>返回文件的全路径.文件名及后缀和错误信息</returns>
       public static bool UploadFile(FileUpload fileUpload, ref string toFilePath, ref string fileName, ref string errorMessage)
        {
            if (fileUpload.HasFile)
            {
                bool isFileOK = false;
                string fileType = fileUpload.PostedFile.ContentType.ToLower();
                string fileExtension = fileUpload.FileName.Substring(
                    fileUpload.FileName.LastIndexOf('.')
                    , fileUpload.FileName.Length - fileUpload.FileName.LastIndexOf('.'));

                //Check file size
                int uploadFileSize=fileUpload.PostedFile.ContentLength;
                uploadFileSize = uploadFileSize / (1024 * 1024);
                if (uploadFileSize> FileMaxSize)
                {
                    errorMessage = "文件不能大于:" + FileMaxSize + "MB" +"实际大小为:"+uploadFileSize+"MB";
                    return false;
                }
                //Check if file type is valid
                foreach (string s in AllowedFileTypes)
                {
                    if (s == fileType)
                        isFileOK = true;
                }
                if (isFileOK)
                {
                    if (!Directory.Exists(toFilePath))
                    {
                        Directory.CreateDirectory(toFilePath);
                    }
                    fileName += fileExtension;
                    toFilePath = Path.Combine(toFilePath, fileName);

                    fileUpload.PostedFile.SaveAs(toFilePath);
                    return true;
                }
                else
                {
                    errorMessage = "非允许文件类型" + fileType;
                    return false;
                }
                
                  
            }
            else
            {
                errorMessage = "文件为空";
                return false;
            }
        }
        
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="toFilePath">保存的文件的路径</param>
        /// <param name="contents"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool SaveFile(string toFilePath, string contents)
        {
            string path = toFilePath.Substring(0, toFilePath.LastIndexOf(Path.DirectorySeparatorChar));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(toFilePath, contents);
            return true;
        }
      
        /*
        /// <summary>
        /// 本地路径转换成URL相对路径
        /// </summary>
        /// <param name="imagesurl1"></param>
        /// <returns></returns>
        private static string urlconvertor(string imagesurl1)
        {
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //imagesurl2 = imagesurl2.Replace(@"Aspx_Uc/", @"");
            return imagesurl2;
        }
        /// <summary>
        /// 相对路径转换成服务器本地物理路径
        /// </summary>
        /// <param name="imagesurl1"></param>
        /// <returns></returns>
        private static string urlconvertorlocal(string imagesurl1)
        {
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = tmpRootDir + imagesurl1.Replace(@"/", @"\"); //转换成绝对路径
            return imagesurl2;
        }
         * */
    }
}
