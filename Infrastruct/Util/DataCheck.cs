using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Util
{
    public static class DataCheck
    {
        public static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isTel(string inputTel)
        {
            //区号+座机号码+分机号码："^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$" 
            //所有手机号码："^((\(\d{3}\))|(\d{3}\-))?13[0-9]\d{8}|15[89]\d{8}"
            string strRegex = @"^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$";
            Regex re = new Regex(strRegex);
            strRegex = @"^((\(\d{3}\))|(\d{3}\-))?13[0-9]\d{8}|15[89]\d{8}";
            Regex reMobil = new Regex(strRegex);
            if (re.IsMatch(inputTel) || reMobil.IsMatch(inputTel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isUrl(string inputUrl)
        {
           string strRegex = "^((https|http|ftp|rtsp|mms)?://)"
                + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?"//ftp的user@ 
                + "(([0-9]{1,3}\\.){3}[0-9]{1,3}"// IP形式的URL- 199.194.52.184 
                + "|"// 允许IP和DOMAIN（域名）
                + "([0-9a-z_!~*'()-]+\\.)*" // 域名- www. 
                + "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\\."// 二级域名 
                + "[a-z]{2,6})"// first level domain- .com or .museum 
                + "(:[0-9]{1,4})?"// 端口- :80 
                + "((/?)|"// a slash isn't required if there is no file name 
                + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$"; 
            //string strRegex = @"^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            //^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^\"\"])*$/
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputUrl))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
