namespace Kursadarbs
{
    partial class EmployeeHiarchy
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
            this.struct_grpbox = new System.Windows.Forms.GroupBox();
            this.flatlist_radio = new System.Windows.Forms.RadioButton();
            this.hiarch_radio = new System.Windows.Forms.RadioButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.struct_grpbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // struct_grpbox
            // 
            this.struct_grpbox.Controls.Add(this.flatlist_radio);
            this.struct_grpbox.Controls.Add(this.hiarch_radio);
            this.struct_grpbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.struct_grpbox.Location = new System.Drawing.Point(12, 367);
            this.struct_grpbox.Name = "struct_grpbox";
            this.struct_grpbox.Size = new System.Drawing.Size(173, 71);
            this.struct_grpbox.TabIndex = 20;
            this.struct_grpbox.TabStop = false;
            this.struct_grpbox.Text = "Show employee structure:";
            // 
            // flatlist_radio
            // 
            this.flatlist_radio.AutoSize = true;
            this.flatlist_radio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatlist_radio.Location = new System.Drawing.Point(6, 23);
            this.flatlist_radio.Name = "flatlist_radio";
            this.flatlist_radio.Size = new System.Drawing.Size(114, 17);
            this.flatlist_radio.TabIndex = 17;
            this.flatlist_radio.Text = "On flat list (Default)";
            this.flatlist_radio.UseVisualStyleBackColor = true;
            // 
            // hiarch_radio
            // 
            this.hiarch_radio.AutoSize = true;
            this.hiarch_radio.Checked = true;
            this.hiarch_radio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hiarch_radio.Location = new System.Drawing.Point(6, 46);
            this.hiarch_radio.Name = "hiarch_radio";
            this.hiarch_radio.Size = new System.Drawing.Size(146, 17);
            this.hiarch_radio.TabIndex = 16;
            this.hiarch_radio.TabStop = true;
            this.hiarch_radio.Text = "Hierarchically (Advanced)";
            this.hiarch_radio.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(18, 39);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(167, 313);
            this.treeView1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Employee Hiarchy list:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(241, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "workername";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(241, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "workersurname";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(241, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(241, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Salary";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(241, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "managed by";
            // 
            // EmployeeHiarchy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.struct_grpbox);
            this.Name = "EmployeeHiarchy";
            this.Text = "EmployeeHiarchy";
            this.struct_grpbox.ResumeLayout(false);
            this.struct_grpbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox struct_grpbox;
        private System.Windows.Forms.RadioButton flatlist_radio;
        private System.Windows.Forms.RadioButton hiarch_radio;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}