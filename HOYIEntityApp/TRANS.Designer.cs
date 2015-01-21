namespace HOYIEntityApp
{
    partial class TRANS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dataGriduser = new System.Windows.Forms.DataGridView();
            this.dataGridrole = new System.Windows.Forms.DataGridView();
            this.lb = new System.Windows.Forms.Label();
            this.txroleName = new System.Windows.Forms.TextBox();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.txusername = new System.Windows.Forms.TextBox();
            this.l = new System.Windows.Forms.Label();
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnGetRole = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txuserrole = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txuuname = new System.Windows.Forms.TextBox();
            this.txpwd = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGriduser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridrole)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "事务测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGriduser
            // 
            this.dataGriduser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGriduser.Location = new System.Drawing.Point(14, 349);
            this.dataGriduser.Name = "dataGriduser";
            this.dataGriduser.RowTemplate.Height = 23;
            this.dataGriduser.Size = new System.Drawing.Size(941, 198);
            this.dataGriduser.TabIndex = 1;
            // 
            // dataGridrole
            // 
            this.dataGridrole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridrole.Location = new System.Drawing.Point(14, 86);
            this.dataGridrole.Name = "dataGridrole";
            this.dataGridrole.RowTemplate.Height = 23;
            this.dataGridrole.Size = new System.Drawing.Size(941, 198);
            this.dataGridrole.TabIndex = 2;
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(20, 55);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(59, 12);
            this.lb.TabIndex = 3;
            this.lb.Text = "roleName:";
            // 
            // txroleName
            // 
            this.txroleName.Location = new System.Drawing.Point(84, 48);
            this.txroleName.Name = "txroleName";
            this.txroleName.Size = new System.Drawing.Size(173, 21);
            this.txroleName.TabIndex = 4;
            // 
            // btnAddRole
            // 
            this.btnAddRole.Location = new System.Drawing.Point(322, 48);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(75, 23);
            this.btnAddRole.TabIndex = 5;
            this.btnAddRole.Text = "Add";
            this.btnAddRole.UseVisualStyleBackColor = true;
            this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);
            // 
            // txusername
            // 
            this.txusername.Location = new System.Drawing.Point(87, 294);
            this.txusername.Name = "txusername";
            this.txusername.Size = new System.Drawing.Size(173, 21);
            this.txusername.TabIndex = 7;
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Location = new System.Drawing.Point(23, 301);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(59, 12);
            this.l.TabIndex = 6;
            this.l.Text = "username:";
            // 
            // cmbRoles
            // 
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(341, 293);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(121, 20);
            this.cmbRoles.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "roleName:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(639, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGetRole
            // 
            this.btnGetRole.Location = new System.Drawing.Point(403, 48);
            this.btnGetRole.Name = "btnGetRole";
            this.btnGetRole.Size = new System.Drawing.Size(75, 23);
            this.btnGetRole.TabIndex = 11;
            this.btnGetRole.Text = "GetRoles";
            this.btnGetRole.UseVisualStyleBackColor = true;
            this.btnGetRole.Click += new System.EventHandler(this.btnGetRole_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(478, 291);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "GetRoles";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(559, 290);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "GetUser";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(478, 320);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "GetUser In";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txuserrole
            // 
            this.txuserrole.Location = new System.Drawing.Point(341, 322);
            this.txuserrole.Name = "txuserrole";
            this.txuserrole.Size = new System.Drawing.Size(121, 21);
            this.txuserrole.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 329);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "roleName:";
            // 
            // txuuname
            // 
            this.txuuname.Location = new System.Drawing.Point(525, 12);
            this.txuuname.Name = "txuuname";
            this.txuuname.Size = new System.Drawing.Size(205, 21);
            this.txuuname.TabIndex = 17;
            // 
            // txpwd
            // 
            this.txpwd.Location = new System.Drawing.Point(525, 40);
            this.txpwd.Name = "txpwd";
            this.txpwd.Size = new System.Drawing.Size(205, 21);
            this.txpwd.TabIndex = 18;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(768, 11);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 19;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(559, 320);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 20;
            this.button7.Text = "Exits";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(640, 320);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(106, 23);
            this.button8.TabIndex = 21;
            this.button8.Text = "Exits Static";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // TRANS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 559);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.txpwd);
            this.Controls.Add(this.txuuname);
            this.Controls.Add(this.txuserrole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnGetRole);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRoles);
            this.Controls.Add(this.txusername);
            this.Controls.Add(this.l);
            this.Controls.Add(this.btnAddRole);
            this.Controls.Add(this.txroleName);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.dataGridrole);
            this.Controls.Add(this.dataGriduser);
            this.Controls.Add(this.button1);
            this.Name = "TRANS";
            this.Text = "TRANS";
            ((System.ComponentModel.ISupportInitialize)(this.dataGriduser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridrole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGriduser;
        private System.Windows.Forms.DataGridView dataGridrole;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.TextBox txroleName;
        private System.Windows.Forms.Button btnAddRole;
        private System.Windows.Forms.TextBox txusername;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnGetRole;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txuserrole;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txuuname;
        private System.Windows.Forms.TextBox txpwd;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}