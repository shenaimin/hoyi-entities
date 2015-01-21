using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Util
{
    public class TreeNodeOBj
    {
        public TreeNodeOBj()
        { }
        public TreeNodeOBj( Object _obj)
        {
            this.obj = _obj;
        }
        public TreeNodeOBj(bool _loaded, Object _obj)
        {
            this.Loaded = _loaded;
            this.obj = _obj;
        }

        /*  是否已经加载过 */ 

        public bool Loaded = false;

        public bool needLoad = true;
        public Object obj { get; set; }

        /// <summary>
        /// 获取Value.
        /// </summary>
        /// <param name="_needLoad"></param>
        /// <param name="_loaded"></param>
        /// <param name="objID"></param>
        /// <returns></returns>
        public static string setRTUNodeO(bool _needLoad, bool _loaded, string objID)
        {
            string result = "";
            result += _needLoad ? "1:" : "0:";
            result += _loaded ? "1:" : "0:";
            result += objID;
            return result;
        }
        /// <summary>
        /// 获取Value.
        /// </summary>
        /// <param name="_needLoad">是否需要加载，如果否，则不检测加载时间.</param>
        /// <param name="_loaded">是否已经加载.</param>
        /// <param name="objID">一般存储加载对象的ID.</param>
        /// <returns></returns>
        public static string setRTUNodeO(bool _loaded, string objID)
        {
            return setRTUNodeO(true, _loaded, objID);
        }
        /// <summary>
        /// 根据Value获取
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TreeNodeOBj getDeliNodeO(string value)
        {
            try
            {
                TreeNodeOBj obj = new TreeNodeOBj();
                string[] vas = value.Split(':');
                obj.needLoad = vas[0].Equals("1");
                obj.Loaded = vas[1].Equals("1");
                obj.obj = vas[2] as object;
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
