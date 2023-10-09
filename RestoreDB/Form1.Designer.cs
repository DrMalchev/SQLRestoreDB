namespace RestoreDB
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            button1 = new Button();
            label4 = new Label();
            textBox3 = new TextBox();
            label5 = new Label();
            textBox4 = new TextBox();
            label6 = new Label();
            textBox5 = new TextBox();
            progressBar1 = new ProgressBar();
            notifyIcon1 = new NotifyIcon(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 9);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "Server";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Local Machine", "Servfinity2", "Servfinity2,1444" });
            comboBox1.Location = new Point(21, 32);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(736, 28);
            comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 73);
            label2.Name = "label2";
            label2.Size = new Size(116, 20);
            label2.TabIndex = 2;
            label2.Text = "Database Name";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(21, 96);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(736, 27);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += TextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 138);
            label3.Name = "label3";
            label3.Size = new Size(108, 20);
            label3.TabIndex = 4;
            label3.Text = "Path to bak file";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(21, 161);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(736, 27);
            textBox2.TabIndex = 5;
            textBox2.TextChanged += TextBox_TextChanged;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(21, 412);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 6;
            button1.Text = "Restore";
            button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 200);
            label4.Name = "label4";
            label4.Size = new Size(162, 20);
            label4.TabIndex = 7;
            label4.Text = "Data and Log directory";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(21, 223);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(731, 27);
            textBox3.TabIndex = 8;
            textBox3.TextChanged += TextBox_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 262);
            label5.Name = "label5";
            label5.Size = new Size(82, 20);
            label5.TabIndex = 9;
            label5.Text = "User Name";
            label5.Click += label5_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(21, 285);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(731, 27);
            textBox4.TabIndex = 10;
            textBox4.TextChanged += TextBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(21, 325);
            label6.Name = "label6";
            label6.Size = new Size(70, 20);
            label6.TabIndex = 11;
            label6.Text = "Password";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(21, 352);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(731, 27);
            textBox5.TabIndex = 12;
            textBox5.TextChanged += TextBox_TextChanged;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(275, 412);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(477, 29);
            progressBar1.TabIndex = 13;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 453);
            Controls.Add(progressBar1);
            Controls.Add(textBox5);
            Controls.Add(label6);
            Controls.Add(textBox4);
            Controls.Add(label5);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(800, 500);
            MinimumSize = new Size(800, 500);
            Name = "Form1";
            Text = "eVeliko Database Restore Wizard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Button button1;
        private Label label4;
        private TextBox textBox3;
        private Label label5;
        private TextBox textBox4;
        private Label label6;
        private TextBox textBox5;
        private ProgressBar progressBar1;
        private NotifyIcon notifyIcon1;
    }
}