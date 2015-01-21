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
	/// 实体类 troleInfo 属性说明自动提取数据库字段的描述信息.
	///</summary>
	[Serializable]
	[EntityAttr("trole", "角色")]
	public class trole : Entity
	{
		#region 静态字段
		/// <summary>
		/// 角色编号
		/// </summary>
        public static AttField roleid = new AttField("roleid");		
		/// <summary>
		/// 角色姓名
		/// </summary>
        public static AttField rolename = new AttField("rolename");		
		/// <summary>
		/// 角色类型
		/// </summary>
        public static AttField roletype = new AttField("roletype");		
		/// <summary>
		/// 备注
		/// </summary>
        public static AttField notes = new AttField("notes");		
		   
		#endregion 静态字段
		
		#region 字段
		/// <summary>
		/// 角色编号
		/// </summary>	
        [DbAttr(datatype.Bigint, 30, Comment = "角色编号", FieldName = "roleid",isPK= true,Identity=true)]
        public Int64 Roleid { get; set; }		
		/// <summary>
		/// 角色姓名
		/// </summary>	
        [DbAttr(datatype.Varchar, 50, Comment = "角色姓名", FieldName = "rolename")]
        public string Rolename { get; set; }		
		/// <summary>
		/// 角色类型
		/// </summary>	
        [DbAttr(datatype.Varchar, 50, Comment = "角色类型", FieldName = "roletype")]
        public string Roletype { get; set; }		
		/// <summary>
		/// 备注
		/// </summary>	
        [DbAttr(datatype.Varchar, 50, Comment = "备注", FieldName = "notes")]
        public string Notes { get; set; }		
		 
		#endregion 字段
			
		public static trole NEW(){
			return new trole();
		} 
		
		public trole() {
		}
		
		public trole( string _rolename ,  string _roletype ,  string _notes ){
			 this.Rolename =  _rolename;
			 this.Roletype =  _roletype;
			 this.Notes =  _notes;
			
		}	
		
		public trole( Int64 _roleid ,  string _rolename ,  string _roletype ,  string _notes ){
			 this.Roleid =  _roleid;
			 this.Rolename =  _rolename;
			 this.Roletype =  _roletype;
			 this.Notes =  _notes;
			
		}	
		
        #region 语法所迫, 初始化命令.

        public static HOYICMD E {
            get{
                return HOYI.E<trole>();
            }
        }

        #endregion 语法所迫, 初始化命令.
	}
}