using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Infrastructure.Database.Att;
using Infrastructure.Database.ctrl;
using Infrastructure.Database.ents;
using Infrastructure.Database.Model;

namespace HOYIENTITY.BAS
{
	///<summary>
	/// 实体类 tuserInfo 属性说明自动提取数据库字段的描述信息.
	///</summary>
	[Serializable]
	[EntityAttr("tuser", "用户")]
	public class tuser : Entity
	{
		#region 静态字段
		/// <summary>
		/// 用户编号
		/// </summary>
        public static AttField userid = new AttField("userid");		
		/// <summary>
		/// 用户姓名
		/// </summary>
        public static AttField username = new AttField("username");		
		/// <summary>
		/// 用户类型
		/// </summary>
        public static AttField usertype = new AttField("usertype");		
		/// <summary>
		/// 角色编号
		/// </summary>
        public static AttField roleid = new AttField("roleid");		
		/// <summary>
		/// 备注
		/// </summary>
        public static AttField notes = new AttField("notes");		
		   
		#endregion 静态字段
		
		#region 字段
		/// <summary>
		/// 用户编号
		/// </summary>	
        [DbAttr(datatype.Bigint, 30, Comment = "用户编号", FieldName = "userid",isPK= true,Identity=true)]
        public Int64 Userid { get; set; }		
		/// <summary>
		/// 用户姓名
		/// </summary>	
        [DbAttr(datatype.Varchar, 50, Comment = "用户姓名", FieldName = "username")]
        public string Username { get; set; }		
		/// <summary>
		/// 用户类型
		/// </summary>	
        [DbAttr(datatype.Varchar, 50, Comment = "用户类型", FieldName = "usertype")]
        public string Usertype { get; set; }		
		/// <summary>
		/// 角色编号
		/// </summary>	
        [DbAttr(datatype.Bigint, 30, Comment = "角色编号", FieldName = "roleid")]
        public Int64 Roleid { get; set; }		
		/// <summary>
		/// 备注
		/// </summary>	
        [DbAttr(datatype.Varchar, 50, Comment = "备注", FieldName = "notes")]
        public string Notes { get; set; }		
		 
		#endregion 字段
			
		public static tuser NEW(){
			return new tuser();
		} 
		
		public tuser() {
		}
		
		public tuser( string _username ,  string _usertype ,  Int64 _roleid ,  string _notes ){
			 this.Username =  _username;
			 this.Usertype =  _usertype;
			 this.Roleid =  _roleid;
			 this.Notes =  _notes;
			
		}	
		
		public tuser( Int64 _userid ,  string _username ,  string _usertype ,  Int64 _roleid ,  string _notes ){
			 this.Userid =  _userid;
			 this.Username =  _username;
			 this.Usertype =  _usertype;
			 this.Roleid =  _roleid;
			 this.Notes =  _notes;
			
		}	
		
        #region 语法所迫, 初始化命令.

        public static HOYICMD E {
            get{
                return HOYI.E<tuser>();
            }
        }

        #endregion 语法所迫, 初始化命令.
	}
}