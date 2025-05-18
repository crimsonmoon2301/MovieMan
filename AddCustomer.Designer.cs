namespace Kursadarbs
{
    partial class AddCustomer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.newcustom_grpbox = new System.Windows.Forms.GroupBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.newcust_name = new System.Windows.Forms.Label();
            this.newcust_phone = new System.Windows.Forms.Label();
            this.newcust_surname = new System.Windows.Forms.Label();
            this.newcust_email = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.newcustom_grpbox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.newcustom_grpbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 189);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information about the Customer";
            // 
            // newcustom_grpbox
            // 
            this.newcustom_grpbox.Controls.Add(this.txtNumber);
            this.newcustom_grpbox.Controls.Add(this.txtEmail);
            this.newcustom_grpbox.Controls.Add(this.txtSurname);
            this.newcustom_grpbox.Controls.Add(this.txtName);
            this.newcustom_grpbox.Controls.Add(this.newcust_name);
            this.newcustom_grpbox.Controls.Add(this.newcust_phone);
            this.newcustom_grpbox.Controls.Add(this.newcust_surname);
            this.newcustom_grpbox.Controls.Add(this.newcust_email);
            this.newcustom_grpbox.Location = new System.Drawing.Point(18, 28);
            this.newcustom_grpbox.Name = "newcustom_grpbox";
            this.newcustom_grpbox.Size = new System.Drawing.Size(286, 144);
            this.newcustom_grpbox.TabIndex = 7;
            this.newcustom_grpbox.TabStop = false;
            this.newcustom_grpbox.Text = "Credentials:";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(91, 110);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(167, 20);
            this.txtNumber.TabIndex = 11;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(91, 83);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(167, 20);
            this.txtEmail.TabIndex = 10;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(91, 57);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(167, 20);
            this.txtSurname.TabIndex = 9;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 31);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(167, 20);
            this.txtName.TabIndex = 8;
            // 
            // newcust_name
            // 
            this.newcust_name.AutoSize = true;
            this.newcust_name.Location = new System.Drawing.Point(6, 34);
            this.newcust_name.Name = "newcust_name";
            this.newcust_name.Size = new System.Drawing.Size(38, 13);
            this.newcust_name.TabIndex = 1;
            this.newcust_name.Text = "Name:";
            // 
            // newcust_phone
            // 
            this.newcust_phone.AutoSize = true;
            this.newcust_phone.Location = new System.Drawing.Point(6, 113);
            this.newcust_phone.Name = "newcust_phone";
            this.newcust_phone.Size = new System.Drawing.Size(79, 13);
            this.newcust_phone.TabIndex = 4;
            this.newcust_phone.Text = "Phone number:";
            // 
            // newcust_surname
            // 
            this.newcust_surname.AutoSize = true;
            this.newcust_surname.Location = new System.Drawing.Point(6, 60);
            this.newcust_surname.Name = "newcust_surname";
            this.newcust_surname.Size = new System.Drawing.Size(52, 13);
            this.newcust_surname.TabIndex = 2;
            this.newcust_surname.Text = "Surname:";
            // 
            // newcust_email
            // 
            this.newcust_email.AutoSize = true;
            this.newcust_email.Location = new System.Drawing.Point(6, 86);
            this.newcust_email.Name = "newcust_email";
            this.newcust_email.Size = new System.Drawing.Size(35, 13);
            this.newcust_email.TabIndex = 3;
            this.newcust_email.Text = "Email:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(79, 216);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 83);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 35);
            this.button1.TabIndex = 9;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(3, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(194, 36);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 311);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddCustomer";
            this.Text = "Add New Customer";
            this.Load += new System.EventHandler(this.AddCustomer_Load);
            this.groupBox1.ResumeLayout(false);
            this.newcustom_grpbox.ResumeLayout(false);
            this.newcustom_grpbox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox newcustom_grpbox;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label newcust_name;
        private System.Windows.Forms.Label newcust_phone;
        private System.Windows.Forms.Label newcust_surname;
        private System.Windows.Forms.Label newcust_email;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}