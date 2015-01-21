using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Util
{
    public static class WordOperate
    {
        /// <summary>
        /// 用来格式化的特殊字符.
        /// </summary>
        public static string[] arrayReg = { "'", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "-", "_", ";", "||", "[", "]", "&", "-", "|", " ", "，", "。", "：", "；", "‘", "“" };
        /// <summary>
        /// 将字符串格式化,按sprite分割.
        /// </summary>
        /// <param name="str">字符串.</param>
        /// <param name="sprite">分隔符.</param>
        /// <returns></returns>
        public static List<String> ToList(this string str, char sprite)
        {
            List<string> strs = new List<string>();
            string[] strArray = str.Split(sprite);
            foreach (string s in strArray)
            {
                strs.Add(s);
            }
            return strs;
        }
        /// <summary>
        /// 替换一个字符串内的特殊字符,特殊字符可在aryReg内追加.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string trimSpecialWord(this string str)
        {
            string[] aryReg = arrayReg;
            //string[] aryReg = { "'", ",", "\\", "/", " ", "-", "，", "。", "，" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                str = str.Replace(aryReg[i], string.Empty);
            }

            return str;
        }
        /// <summary>
        /// 替换一个字符串内的特殊字符为指定值,特殊字符可在aryReg内追加.
        /// </summary>
        /// <param name="str">字符串.</param>
        /// <param name="value">待替换的值.</param>
        /// <returns></returns>
        public static string trimSpecialWord(this string str,string value)
        {
            string[] aryReg = arrayReg;
            //string[] aryReg = { "'", ",", "\\", "/", " ", "-", "，", "。", "，" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                str = str.Replace(aryReg[i], value);
            }

            return str;
        }
        /// <summary>
        /// 根据规则，替换字符串内连续相同的字符串为一个字符.好难理解.
        /// 例如：将",,,"替换成",".
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string trimSameCharToString(this string str)
        {
            string[] aryReg = arrayReg;
            List<char> charList = str.ToList();

            List<string> returnValue = new List<string>();
            returnValue.Add(charList[0].ToString());

            int i = 1;
            while (i < str.Length)
            {
                if (aryReg.Contains(returnValue[returnValue.Count - 1]))
                {
                    if (returnValue[returnValue.Count - 1] != charList[i].ToString())//相同
                    {
                        returnValue.Add(charList[i].ToString());
                    }
                }
                else
                {
                    returnValue.Add(charList[i].ToString());
                }
                i++;
            }

            return returnValue.ToNormaloString();
        }
        /// <summary>
        /// 返回是否包含指定的值.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ContainSpecialChar(this string str)
        {
            bool re = false;
            string[] aryReg = arrayReg;
            List<char> charList = str.ToList();

            int i = 1;
            foreach (char c in charList)
            {
                if (aryReg.Contains(c.ToString()))
                {
                    re = true;
                    return re;
                }
            }

            return re;
        }
        /// <summary>
        /// 按条件获取List.
        /// </summary>
        /// <param name="strList">目标List.</param>
        /// <param name="startSize">从0开始的其实位置.</param>
        /// <param name="count">获取List的个数.</param>
        /// <returns></returns>
        public static List<string> ToListByCount(this List<string> strList, int resorceIndex, int stageIndex, int count)
        {
            string[] tempList = new string[strList.Count];
            if (strList.Count >= count)
            {
                strList.CopyTo(resorceIndex, tempList, stageIndex, count);
            }
            return tempList.ToList();
        }
        /// <summary>
        /// 将数组转换成格式化字符串.
        /// </summary>
        /// <param name="strList">数组.</param>
        /// <param name="sprite">分割字符.</param>
        /// <returns></returns>
        public static string ToString(this List<string> strList, char sprite)
        {
            string str = "";
            foreach (string s in strList)
            {
                str += s + sprite;
            }
            return str;
        }
        /// <summary>
        /// 将数组转换成格式化字符串.
        /// </summary>
        /// <param name="strList">数组.</param>
        /// <returns></returns>
        public static string ToNormaloString(this List<string> strList)
        {
            string str = "";
            foreach (string s in strList)
            {
                str += s;
            }
            return str;
        }
        /// <summary>
        /// 替换数组内某个位置的值.
        /// </summary>
        /// <param name="strList">数组.</param>
        /// <param name="index">待替换值的位置.</param>
        /// <param name="replaceValue">替换的值.</param>
        /// <returns></returns>
        public static List<string> Replace(this List<string> strList, int index, string replaceValue)
        {
            if (index >= strList.Count)
            {
                strList.Add(replaceValue);
            }
            else if (index >= 0)
            {
                strList[index] = replaceValue;
            }
            return strList;
        }
        /// <summary>
        /// 将数组转换成List
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static List<string> ToArrayList(this string[] strs)
        {
            List<string> Values = new List<string>();
            foreach (string str in strs)
            {
                Values.Add(str);
            }
            return Values;
        }
    }
}