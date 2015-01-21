/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using HOYIENTITY.Demo;
using Infrastructure.Database.ctrl;
using Infrastructure.Database.ents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOYIEntityApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = user.E.Where(user.userid.Equals("4")).Select();

            //dataGridView1.DataSource = user.E.Where(user.userid != 4 & user.username.Except("How Areyou")).Select();

            dataGridView1.DataSource = user.E.Where(user.userid != 3 | user.username != "aaa").Limit(0, 2).ASC(user.userid).Select();


            int aa = 10;

            //dataGridView1.DataSource = user.E.Where(user.userid.Except(4)).Select();

            
            //dataGridView1.DataSource = user.E.Where(user.userid != -1).Select(user.userid, "min(userid)");

            //dataGridView1.DataSource = user.E.Where(user.userid != 4 | user.username != "999" | user.pwd != "123").Select();

            dataGridView1.DataSource = user.E.Select();

            //user.E.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = user.E.Count();
            //int count = user.E.Where(user.userid == 4 | user.userid != 5).Count();
            MessageBox.Show(count.ToString());
            button1_Click(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = user.E.Where(user.userid==6).Update(user.pwd >= 7777, user.username >= "你好啊靓仔");
            MessageBox.Show(count.ToString());
            button1_Click(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = user.E.Where(user.userid == 15).Delete();
            MessageBox.Show(count.ToString());
            button1_Click(null, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            user us = new user(tx_username.Text, tx_password.Text, 0, "");
            int count =  us.Insert();

            var ss = new{ user.userid, user.username };

            button1_Click(null, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = user.STA_ExecuteTable(tx_Execute.Text);
            List<user> uses = Entity.TransFromTable<user>(dt);
            MessageBox.Show(uses.Count.ToString());

            dataGridView1.DataSource = uses;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            user us = new user(tx_username.Text, tx_password.Text, 0, "");
            int id = us.Insert_RETURN_ID();

            MessageBox.Show(id.ToString());
            button1_Click(null, null);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = user.E.Where(user.userid == 4  | user.userid == 5 | user.userid == 7).Select<user>();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {
                int userid = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                user uu = user.E.Where(user.userid == userid).First<user>();
                //List<user> us = user.E.Where(user.userid == userid).Select<user>();
                //user uu = us[0];
                int ret = uu.Delete();
                
                //MessageBox.Show(ret.ToString());

                button1_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please Check a Row Now.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {
                int userid = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                user uu = user.E.Where(user.userid == userid).First<user>();
                uu.Username = "更新";
                uu.Pwd = "更新";

                int ret = uu.Update();

                button1_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please Check a Row Now.");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {
                int userid = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                user uu = user.E.Where(user.userid == userid).First<user>();
                uu.Username = "Updated";
                uu.Pwd = "WHAT THE FK?";
                var filter = new[] { user.userid };
                int ret = uu.Update(filter);

                button1_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please Check a Row Now.");
            }
        }

        int datacount = 0;
        int pgsize = 0;
        int pgcount = 0;
        int pgindex = 0;
        private void button16_Click(object sender, EventArgs e)
        {
            datacount = user.E.Count();
            pgsize = Int32.Parse(tx_pgsize.Text);
            dataGridView1.DataSource = user.E.DataCount(datacount).PgSize(pgsize).Jump(pgindex).Desc(user.userid).Select(ref pgcount, ref pgindex);
            ShowPgInfo();
        }

        public void ShowPgInfo()
        {
            this.label3.Text = "共" + datacount + "条 第" + pgindex.ToString()+ "/" + pgcount.ToString() + "页";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            pgindex = 1;
            button16_Click(null, null);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pgindex --;
            button16_Click(null, null);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            pgindex++;
            button16_Click(null, null);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            pgindex = pgcount;
            button16_Click(null, null);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = user.E.Where(user.pwd % 'a' | user.username % 'b' | user.username == "EE").Select();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //var strs = new object[] { "SS", "DD", "更新" };
            //this.dataGridView1.DataSource = user.E.Where(user.username.In(strs)).Select<user>();
            var ids = new object[] {  2, 3 };
            this.dataGridView1.DataSource = user.E.Where(user.userid.In(ids)).Select();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var strs = new object[] { "SS", "DD", "更新" };
            this.dataGridView1.DataSource = user.E.Where(user.username.NotIn(strs)).Select<user>();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var strs = new object[] { "S", "G" };
            this.dataGridView1.DataSource = user.E.Where(user.username.Like(strs)).Select();
        }
    }
}
