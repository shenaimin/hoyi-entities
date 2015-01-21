/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infrastructure.Database.ctrl;
using Infrastructure.Database.Att;
using System.Linq.Expressions;
using System.Reflection;
using HOYIENTITY.Demo;

namespace HOYIEntityApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //user user1 = new user();
            //user1.UserName = tx_username.Text;
            //user1.Pwd = tx_password.Text;
            //int ret = user1.Insert();
            //MessageBox.Show(ret > 0 ? "插入成功!" : "插入失败,请检查.");
            //button2_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //List<user> users = user.LoadAll<user>();
            //dataGridView1.DataSource = users;
            this.dataGridView1.DataSource = user.E.Select();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //int ret = user.DeleteBy<user>("userid", 3, "username", "Microsoft");

            //user user = new user();

            //user.UserId = 2;
            //user.UserName = "项目";

            ////user.DeleteBy({"userid", "username"}, {1, "21"});

            //int ret = user.DeleteA<user>(new string[] { "userid", "username" });

            //MessageBox.Show(ret > 0 ? "删除成功!" : "删除失败,请检查.");
            //button2_Click(null, null);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //List<user> users = user.GetBy<user>("userid", "4");
            //dataGridView1.DataSource = users;
            //List<user> users = user.GetBy<user>("userid", 4, "username", "999");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //string field = "userid, username";
            ////string[] fields  {"userid", "username"};
            //string[] names = { "userid", "username" };
            //object[] values = { 4, "999" };

            //List<user> users = user.Get<user>(field, names, values, "", "");
            //dataGridView1.DataSource = users;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Get<T>(string[] fields, string[] names, object[] values, string orderby, string limits)
            //var array = {"userid", "username"};
            //var sss=  {1,2,3,4};

            //DataTable dt = user.Get<user>( new string[]{"userid", "username"}, new string[]{"userid"}, new object[]{6}, "", "");
            //dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //int count = user.GetCount(new string[] { "userid", "username" }, new string[] { "userid" }, new object[] { 6 }, "", "");
            ////MessageBox.Show(count.ToString());
            //string[] ppp = user.Z_STR("userid", "username");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //List<user> users = user.Select("userid", "username").Where("userid", 4, "username", "qq").Limit(0, 1).Order("AAA", "desc").Execute();

            user us = new user();
            //HOYICMD cmd = user.Select<user>().Where("userid", 4, "username", "qq");
            //HOYICMD cmd = user.Select<user>("userid", "username").Where("userid", 4, "username", "qq");

            //this.dataGridView1.DataSource = user.I<user>().Where("userid", 4).Select("userid", "username");

            //this.dataGridView1.DataSource = user.I<user>().Where("userid", 4).Select();

            //this.dataGridView1.DataSource = HOYI.I<user>().Select HOYI.I<user>;// user.I<user>().Select();

            //this.dataGridView1.DataSource = HOYI.I<user>().Where("userid", 4).Select();

            var ids = new[] { 4, 5 };

            //this.dataGridView1.DataSource = HOYI.I<user>().In("userid", ids).Select();

            //this.dataGridView1.DataSource = user.E.Where(user.userid, 4).Select(user.userid, 5, user.username);

            //this.dataGridView1.DataSource = user.E.In(user.userid, ids).Select();

            //user.E.Where(Equals(user.username, "3"));

            //user.E.Where(user.username , "3", &&, user.userid ,4);

            //user.E.Where(user.username, "3");

            //user.E.Where(user.username.Equals("11"));
            //user.E.Where(user.username.Equals(3));
            //user.E.Where()
            //string cmd = (user.username == "3" & user.userid != 3);
            //HOYICMD cmd = user.username.EQ("3") && user.userid.IndexOf();
            //user.userid.EQ(3) && ;

            //user.userid.In(ids);

            //user.E.Where(user.username == "3" && user.userid == 4 || user.userid in ids);

            //Expression<Func<string, bool>> e1 = c => c == "London";
            //string cmd = (user.username.Equals("3") & user.userid.Except(5) & user.username in ids));

            //string cmd2 =  user.username.Equals("3") & user.username.In(ids);



            user.E.Where(user.username.Equals("3") & user.username.Except("5"));


            //user.E.Where(user.userid.In(ids));

            //MessageBox.Show(cmd.cmd);
        }
    }
}