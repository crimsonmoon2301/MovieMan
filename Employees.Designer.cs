namespace Kursadarbs
{
    partial class Employees
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.filtr_txtbox = new System.Windows.Forms.TextBox();
            this.filtr_label = new System.Windows.Forms.Label();
            this.struct_grpbox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.desc_label = new System.Windows.Forms.Label();
            this.title_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.add_btn = new System.Windows.Forms.Button();
            this.savech_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.struct_grpbox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(535, 298);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // filtr_txtbox
            // 
            this.filtr_txtbox.Location = new System.Drawing.Point(15, 370);
            this.filtr_txtbox.Name = "filtr_txtbox";
            this.filtr_txtbox.Size = new System.Drawing.Size(171, 20);
            this.filtr_txtbox.TabIndex = 2;
            this.filtr_txtbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // filtr_label
            // 
            this.filtr_label.AutoSize = true;
            this.filtr_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filtr_label.Location = new System.Drawing.Point(12, 354);
            this.filtr_label.Name = "filtr_label";
            this.filtr_label.Size = new System.Drawing.Size(43, 13);
            this.filtr_label.TabIndex = 3;
            this.filtr_label.Text = "Filter: ";
            this.filtr_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // struct_grpbox
            // 
            this.struct_grpbox.Controls.Add(this.button1);
            this.struct_grpbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.struct_grpbox.Location = new System.Drawing.Point(300, 354);
            this.struct_grpbox.Name = "struct_grpbox";
            this.struct_grpbox.Size = new System.Drawing.Size(167, 71);
            this.struct_grpbox.TabIndex = 19;
            this.struct_grpbox.TabStop = false;
            this.struct_grpbox.Text = "Show employee structure:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.desc_label);
            this.groupBox1.Controls.Add(this.title_label);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(561, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(131, 456);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available options";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // desc_label
            // 
            this.desc_label.AutoSize = true;
            this.desc_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desc_label.Location = new System.Drawing.Point(10, 403);
            this.desc_label.Name = "desc_label";
            this.desc_label.Size = new System.Drawing.Size(35, 13);
            this.desc_label.TabIndex = 15;
            this.desc_label.Text = "label4";
            // 
            // title_label
            // 
            this.title_label.AutoSize = true;
            this.title_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_label.Location = new System.Drawing.Point(10, 370);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(41, 13);
            this.title_label.TabIndex = 14;
            this.title_label.Text = "label3";
            this.title_label.Click += new System.EventHandler(this.title_label_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.add_btn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.savech_btn, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 26);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(111, 318);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // add_btn
            // 
            this.add_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.add_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_btn.Location = new System.Drawing.Point(8, 8);
            this.add_btn.Margin = new System.Windows.Forms.Padding(8);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(95, 143);
            this.add_btn.TabIndex = 12;
            this.add_btn.Text = "Add new Employee";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.button3_Click);
            // 
            // savech_btn
            // 
            this.savech_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.savech_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savech_btn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.savech_btn.Location = new System.Drawing.Point(8, 167);
            this.savech_btn.Margin = new System.Windows.Forms.Padding(8);
            this.savech_btn.Name = "savech_btn";
            this.savech_btn.Size = new System.Drawing.Size(95, 143);
            this.savech_btn.TabIndex = 11;
            this.savech_btn.Text = "Save changes made";
            this.savech_btn.UseVisualStyleBackColor = true;
            this.savech_btn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Available information about the store staff";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(32, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show hiarchically";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // Employees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 456);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.struct_grpbox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.filtr_label);
            this.Controls.Add(this.filtr_txtbox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Employees";
            this.Text = "Employees";
            this.Load += new System.EventHandler(this.Employees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.struct_grpbox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox filtr_txtbox;
        private System.Windows.Forms.Label filtr_label;
        private System.Windows.Forms.GroupBox struct_grpbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.Button savech_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label desc_label;
        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.Button button1;
    }
}