/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using HOYIENTITY.BAS;
using HOYIENTITY.Demo;
using Infrastructure.Database;
using Infrastructure.Database.Cluster;
using Infrastructure.Database.ctrl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOYIEntityApp
{
    public partial class TRANS : Form
    {
        public TRANS()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDatabase database = DataBaseCluster.Get_Transaction_WRITE_SERVER();
            database.InitAndOpenTransaction();

            tuser user = new tuser("aa", "1", 0, "aa");
            tuser user1 = new tuser("b00000000", "2", 0, "bb");

            try
            {
                user.Insert(database);
                int userid = user1.Insert_RETURN_ID(database);

                tuser.E.Where(tuser.userid == 18).Delete();
                tuser.E.Where(tuser.userid == 20).Delete(database);
                database.CommitTranscation();
            }
            catch (Exception ex)
            {
                database.RollBackTransaction();
            }
            database.CloseConnection();
            
            trole role = new trole("DDDSX", "DDDSXA", "AASDAS");
            role.Insert();

        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            trole role = new trole(txroleName.Text, "", "");
            role.Insert();

            dataGridrole.DataSource = trole.E.Select();
        }

        private void btnGetRole_Click(object sender, EventArgs e)
        {
            dataGridrole.DataSource = trole.E.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbRoles.DataSource = trole.E.Select();
            cmbRoles.DisplayMember = trole.rolename.fieldname;
            cmbRoles.ValueMember = trole.roleid.fieldname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int64 roleid = cmbRoles.SelectedValue != null ? Int64.Parse(cmbRoles.SelectedValue.ToString()) : Int64.Parse("-1");
            tuser user = new tuser(txusername.Text, "", roleid, "");
            user.Insert_RETURN_ID();

            dataGriduser.DataSource = tuser.E.Select();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGriduser.DataSource = tuser.E.Select();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string rolename = txuserrole.Text;

            dataGriduser.DataSource = tuser.E.Where(tuser.roleid.In(
                                                        trole.E.Where(
                                                            trole.roleid.In(
                                                                tuser.E.Where(tuser.username == "B").Select_CMD(tuser.userid)
                                                            )
                                                        ).Select_CMD(trole.roleid)
                                                    )).Select(tuser.userid, tuser.username, tuser.roleid);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string username = txuuname.Text;
            string usertype = txpwd.Text;


            List<tuser> users = tuser.E.Where(tuser.username == username & tuser.usertype == usertype).Select<tuser>();

            MessageBox.Show(users.Count.ToString());

            string cmd = "select count(userid) from user where username = '" + txuuname.Text + "' and pwd = '" + txpwd.Text + "'";
            //MessageBox.Show(cmd);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //user user = user.E.First<user>();

            user user = new user("XX", 12,"XX");

            bool Exists = user.Exists();
            MessageBox.Show(Exists.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool Exists = user.E.Where(user.userid == 10).Exists();
            MessageBox.Show(Exists.ToString());
        }
    }
}
