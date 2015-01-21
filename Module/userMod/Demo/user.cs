using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Infrastructure.Database.Att;
using Infrastructure.Database.ctrl;
using Infrastructure.Database.ents;
using Infrastructure.Database.Model;

namespace HOYIENTITY.Demo
{
	///<summary>
	/// 实体类 userInfo 属性说明自动提取数据库字段的描述信息.
	///</summary>
	[Serializable]
	[EntityAttr("user", "用户")]
	public class user : Entity
	{
		#region 静态字段
		
        public static AttField userid = new AttField("userid");		
				
        public static AttField username = new AttField("username");		
				
        public static AttField pwd = new AttField("pwd");		
				
        public static AttField amount = new AttField("amount");		
				
        public static AttField notes = new AttField("notes");		
		   
		#endregion 静态字段
		
		#region 字段
		
        [DbAttr(datatype.Int, 10, Comment = "用户编号", FieldName = "userid",isPK= true,Identity=true)]
        public int Userid { get; set; }		
				
        [DbAttr(datatype.Varchar, 30, Comment = "用户名", FieldName = "username", NotNULL = true)]
        public string Username { get; set; }		
				
        [DbAttr(datatype.Varchar, 30, Comment = "密码", FieldName = "pwd")]
        public string Pwd { get; set; }		
				
        [DbAttr(datatype.Decimal, 15, Comment = "金额", FieldName = "amount", Prefix = 15, suffix = 2)]
        public decimal Amount { get; set; }		
				
        [DbAttr(datatype.Varchar, 50, Comment = "备注", FieldName = "notes")]
        public string Notes { get; set; }		
		 
		#endregion 字段
			
		public static user NEW(){
			return new user();
		} 
		
		public user() {
		}
		
		public user( string _pwd ,  decimal _amount ,  string _notes ){
			 this.Pwd =  _pwd;
			 this.Amount =  _amount;
			 this.Notes =  _notes;
			
		}	
		
		public user( string _username ,  string _pwd ,  decimal _amount ,  string _notes ){
			 this.Username =  _username;
			 this.Pwd =  _pwd;
			 this.Amount =  _amount;
			 this.Notes =  _notes;
			
		}	
		
		public user( int _userid ,  string _username ,  string _pwd ,  decimal _amount ,  string _notes ){
			 this.Userid =  _userid;
			 this.Username =  _username;
			 this.Pwd =  _pwd;
			 this.Amount =  _amount;
			 this.Notes =  _notes;
			
		}	
		
        #region 语法所迫, 初始化命令.

        public static HOYICMD E {
            get{
                return HOYI.E<user>();
            }
        }

        #endregion 语法所迫, 初始化命令.
	}
}