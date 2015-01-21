using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace Infrastructure.Util
{
    public static class JSController
    {
        /// <summary>
        /// JS提示框
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="mess"></param>
        public static void Alert(Page pg,string mess)
        {
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "", "<script>alert('" + mess + "');</script>");
            //pg.ClientScript.RegisterStartupScript(pg.GetType(), "ALT", "<script>alert('" + mess + "');</script>");
            //pg.Response.Write("<script>alert('" + mess + "');</script>");
        }

        /// <summary>
        /// 弹出窗口.
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void NewWindow(Page pg,string url,string title,string height,string width,string top,string left)
        {
            pg.Response.Write("<script>window.open('" + url + "', '" + title + "', 'height=" + height + ", width=" + width + ", top=" + top + ", left=" + left + ", toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");
        }
        /// <summary>
        /// 执行Script
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="script"></param>
        public static void Execute(Page pg, string script)
        {
            pg.Response.Write("<script type='text/javascript'>" + script + "</script>");
        }
        /// <summary>
        ///  JS提示框
         /// </summary>
         /// <param name="mess"></param>
        public static void Alert(string mess)
        {   
            HttpContext.Current.Response.Write("<script>alert('" + mess + "');</script>");
        }
        /// <summary>
        /// 关闭当前窗口.
        /// </summary>
        public static void Close()
        {
            HttpContext.Current.Response.Write("<script>window.close();</script>");
        }
        /// <summary>
        /// 弹出窗口.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void NewWindow(string url, string title, string height, string width, string top, string left)
        {
            HttpContext.Current.Response.Write("<script>window.open('" + url + "', '" + title + "', 'height=" + height + ", width=" + width + ", top=" + top + ", left=" + left + ", toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');</script>");
        }
        /// <summary>
        /// 执行Script
        /// </summary>
        /// <param name="script"></param>
        public static void Execute(string script)
        {
            HttpContext.Current.Response.Write("<script type='text/javascript'>" + script + "</script>");
        }


        public static void RegisterScript(Page pg, string key, string value)
        {
            if (!pg.ClientScript.IsClientScriptIncludeRegistered(pg.GetType(), key))
            {
                pg.ClientScript.RegisterStartupScript(pg.GetType(), key, " <script language= 'javascript'> " + value + " </script> ");
            }
        }

        public static void RegisterAlert(Page pg, string key, string mess)
        {

            if (!pg.ClientScript.IsClientScriptIncludeRegistered(pg.GetType(), key))
            {
                pg.ClientScript.RegisterStartupScript(pg.GetType(), key, " <script language= 'javascript'> alert('" + mess + "'); </script> ");
            }
        }
        
    }
}
