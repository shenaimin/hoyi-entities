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
    public static class PhotoUploader
    {
        /// <summary>
        /// Gets the allowed photo types.
        /// </summary>
        /// <value>The allowed photo types.</value>
        public static string[] AllowedPhotoTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedPhotoTypes"].Split(spliter);
            }
        }
        public static string[] AllowedFileTypes
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedFileTypes"].Split(spliter);
            }
        }

        public static string[] AllowedFileExtensions
        {
            get
            {
                char[] spliter = { ',' };
                return ConfigurationManager.AppSettings["allowedFileExtensions"].Split(spliter);
            }
        }
        /// <summary>
        /// Gets the size of the photo max.
        /// </summary>
        /// <value>The size of the photo max.</value>
        public static int PhotoMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["photoMaxSize"]);
            }
        }

        public static int FileMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["FileMaxSize"]);
            }
        }
        /// <summary>
        /// Upload photo
        /// </summary>
        /// <param name="fileUpload">FileUpload control</param>
        /// <param name="createTime"></param>
        /// <param name="avatarId"></param>
        /// <param name="avatarFileName"></param>
        /// <param name="avatarFullName"></param>
        public static bool Upload(FileUpload fileUpload, DateTime createTime, Guid avatarId, ref string avatarFileName,
            ref string avatarFullName,ref string errorMessage)
        {
            if (fileUpload.HasFile)
            {

                bool isFileOK = false;

                string fileName = fileUpload.FileName;
                string fileExtension =
                    fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
                string fileType = fileUpload.PostedFile.ContentType.ToLower();

                //Check file size
                if (fileUpload.PostedFile.ContentLength > PhotoUploader.PhotoMaxSize)
                {
                    errorMessage = "图片不能大于:" + PhotoUploader.PhotoMaxSize;
                    return false;
                }
                //Check if file type is valid
                foreach (string s in PhotoUploader.AllowedPhotoTypes)
                {
                    if (s == fileType)
                        isFileOK = true;
                }

                if (isFileOK)
                {
                    //Build path
                    StringBuilder sb = new StringBuilder();
                    sb.Append(ConfigurationManager.AppSettings["avatarFolderPath"]);
                    sb.Append("/" + createTime.Year);
                    sb.Append("/" + createTime.Month);
                    sb.Append("/" + createTime.Day);
                    string folderName = sb.ToString();
                    sb.Append("/" + avatarId + fileExtension);
                    string finalFilename = sb.ToString();

                    //Create Directory
                    DirectoryInfo newDir = new DirectoryInfo(folderName);//如果用相对路径。请用下面的方法.
                    //DirectoryInfo newDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(folderName));
                    if (!newDir.Exists)
                        newDir.Create();

                    //Save file
                    FileInfo oldFile = new FileInfo(finalFilename);
                    //FileInfo oldFile = new FileInfo(HttpContext.Current.Server.MapPath(finalFilename));
                    if (oldFile.Exists)
                    {
                        oldFile.Attributes = FileAttributes.Normal;
                        oldFile.Delete();
                    }
                    fileUpload.PostedFile.SaveAs(finalFilename);
                    //fileUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(finalFilename));

                    //Update
                    avatarFileName = fileUpload.FileName;

                    avatarFullName = finalFilename;
                    return true;

                }
                else
                {
                    errorMessage = "非允许图片类型";
                    return false;
                }
            }
            else
            {
                errorMessage = "文件为空";
                return false;
            }
        }

        public static void Upload(FileUpload fileUpload, DateTime createTime, Guid avatarId, ref string avatarFileName,
            ref string avatarFullName)
        {
            if (fileUpload.HasFile)
            {

                bool isFileOK = false;

                string fileName = fileUpload.FileName;
                string fileExtension =
                    fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
                string fileType = fileUpload.PostedFile.ContentType.ToLower();

                //Check file size
                if (fileUpload.PostedFile.ContentLength > PhotoUploader.PhotoMaxSize)
                {
                    throw new ApplicationException(string.Format("ErrorMessage", PhotoUploader.PhotoMaxSize));
                }
                //Check if file type is valid
                foreach (string s in PhotoUploader.AllowedPhotoTypes)
                {
                    if (s == fileType)
                        isFileOK = true;
                }

                if (isFileOK)
                {
                    //Build path
                    StringBuilder sb = new StringBuilder();
                    sb.Append(ConfigurationManager.AppSettings["avatarFolderPath"]);
                    sb.Append("/" + createTime.Year);
                    sb.Append("/" + createTime.Month);
                    sb.Append("/" + createTime.Day);
                    string folderName = sb.ToString();
                    sb.Append("/" + avatarId + fileExtension);
                    string finalFilename = sb.ToString();

                    //Create Directory
                    DirectoryInfo newDir = new DirectoryInfo(folderName);//如果用相对路径。请用下面的方法.
                    //DirectoryInfo newDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(folderName));
                    if (!newDir.Exists)
                        newDir.Create();

                    //Save file
                    FileInfo oldFile = new FileInfo(finalFilename);
                    //FileInfo oldFile = new FileInfo(HttpContext.Current.Server.MapPath(finalFilename));
                    if (oldFile.Exists)
                    {
                        oldFile.Attributes = FileAttributes.Normal;
                        oldFile.Delete();
                    }
                    fileUpload.PostedFile.SaveAs(finalFilename);
                    //fileUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(finalFilename));

                    //Update
                    avatarFileName = fileUpload.FileName;

                    avatarFullName = finalFilename;

                }
                else
                {
                    throw new ApplicationException("ErrorMessage");
                }
            }
            else
            {
                throw new Exception("ErrorMessage");
            }
        }

        public static bool UploadFile(FileUpload fileUpload, DateTime createTime, Guid avatarId, ref string avatarFileName,
            ref string avatarFullName, ref string errorMessage)
        {
            if (fileUpload.HasFile)
            {

                bool isFileOK = false;

                string fileName = fileUpload.FileName;
                string fileExtension =
                    fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
                string fileType = fileUpload.PostedFile.ContentType.ToLower();

                //Check file size
                if (fileUpload.PostedFile.ContentLength > PhotoUploader.FileMaxSize)
                {
                    errorMessage = "文件不能大于:" + PhotoUploader.FileMaxSize;
                    return false;
                }
                //Check if file type is valid
                foreach (string s in PhotoUploader.AllowedFileTypes)//AllowedFileTypes
                {
                    if (s == fileType)
                        isFileOK = true;
                }

                if (isFileOK)
                {
                    //Build path
                    StringBuilder sb = new StringBuilder();
                    sb.Append(ConfigurationManager.AppSettings["avatarFolderPath"]);
                    sb.Append("/" + createTime.Year);
                    sb.Append("/" + createTime.Month);
                    sb.Append("/" + createTime.Day);
                    string folderName = sb.ToString();
                    sb.Append("/" + avatarId + fileExtension);
                    string finalFilename = sb.ToString();

                    //Create Directory
                    DirectoryInfo newDir = new DirectoryInfo(folderName);//如果用相对路径。请用下面的方法.
                    //DirectoryInfo newDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(folderName));
                    if (!newDir.Exists)
                        newDir.Create();

                    //Save file
                    FileInfo oldFile = new FileInfo(finalFilename);
                    //FileInfo oldFile = new FileInfo(HttpContext.Current.Server.MapPath(finalFilename));
                    if (oldFile.Exists)
                    {
                        oldFile.Attributes = FileAttributes.Normal;
                        oldFile.Delete();
                    }
                    fileUpload.PostedFile.SaveAs(finalFilename);
                    //fileUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(finalFilename));

                    //Update
                    avatarFileName = fileUpload.FileName;

                    avatarFullName = finalFilename;
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