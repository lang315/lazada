namespace Lazada
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnFileAccountPath = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxtResult = new System.Windows.Forms.RichTextBox();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.chkRemoveProduct = new System.Windows.Forms.CheckBox();
            this.chkOnlyLike = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudMaxAccount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTimeMax = new System.Windows.Forms.NumericUpDown();
            this.btnFileSSHPath = new System.Windows.Forms.Button();
            this.btnFileLinkPath = new System.Windows.Forms.Button();
            this.nudMinAccount = new System.Windows.Forms.NumericUpDown();
            this.nudTimeMin = new System.Windows.Forms.NumericUpDown();
            this.nudCountThreads = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileSSHPath = new System.Windows.Forms.TextBox();
            this.txtFileLinkPath = new System.Windows.Forms.TextBox();
            this.txtFileAccountPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFileAccountPath
            // 
            this.btnFileAccountPath.Location = new System.Drawing.Point(329, 101);
            this.btnFileAccountPath.Name = "btnFileAccountPath";
            this.btnFileAccountPath.Size = new System.Drawing.Size(67, 22);
            this.btnFileAccountPath.TabIndex = 5;
            this.btnFileAccountPath.Text = "...";
            this.btnFileAccountPath.UseVisualStyleBackColor = true;
            this.btnFileAccountPath.Click += new System.EventHandler(this.btnFileAccountPath_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(328, 252);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(67, 28);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtResult);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(422, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 375);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result:";
            // 
            // rtxtResult
            // 
            this.rtxtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtResult.Location = new System.Drawing.Point(3, 18);
            this.rtxtResult.Name = "rtxtResult";
            this.rtxtResult.ReadOnly = true;
            this.rtxtResult.Size = new System.Drawing.Size(368, 354);
            this.rtxtResult.TabIndex = 0;
            this.rtxtResult.TabStop = false;
            this.rtxtResult.Text = "";
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.chkRemoveProduct);
            this.grpSettings.Controls.Add(this.chkOnlyLike);
            this.grpSettings.Controls.Add(this.label6);
            this.grpSettings.Controls.Add(this.label5);
            this.grpSettings.Controls.Add(this.label8);
            this.grpSettings.Controls.Add(this.label4);
            this.grpSettings.Controls.Add(this.label7);
            this.grpSettings.Controls.Add(this.nudMaxAccount);
            this.grpSettings.Controls.Add(this.label3);
            this.grpSettings.Controls.Add(this.nudTimeMax);
            this.grpSettings.Controls.Add(this.btnFileSSHPath);
            this.grpSettings.Controls.Add(this.btnFileLinkPath);
            this.grpSettings.Controls.Add(this.nudMinAccount);
            this.grpSettings.Controls.Add(this.btnFileAccountPath);
            this.grpSettings.Controls.Add(this.nudTimeMin);
            this.grpSettings.Controls.Add(this.nudCountThreads);
            this.grpSettings.Controls.Add(this.label2);
            this.grpSettings.Controls.Add(this.txtFileSSHPath);
            this.grpSettings.Controls.Add(this.txtFileLinkPath);
            this.grpSettings.Controls.Add(this.txtFileAccountPath);
            this.grpSettings.Controls.Add(this.label1);
            this.grpSettings.Location = new System.Drawing.Point(0, 0);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(409, 246);
            this.grpSettings.TabIndex = 13;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings:";
            // 
            // chkRemoveProduct
            // 
            this.chkRemoveProduct.AutoSize = true;
            this.chkRemoveProduct.Location = new System.Drawing.Point(125, 212);
            this.chkRemoveProduct.Name = "chkRemoveProduct";
            this.chkRemoveProduct.Size = new System.Drawing.Size(201, 20);
            this.chkRemoveProduct.TabIndex = 21;
            this.chkRemoveProduct.Text = "Xóa sản phẩm trong giỏ hàng";
            this.chkRemoveProduct.UseVisualStyleBackColor = true;
            // 
            // chkOnlyLike
            // 
            this.chkOnlyLike.AutoSize = true;
            this.chkOnlyLike.Location = new System.Drawing.Point(125, 189);
            this.chkOnlyLike.Name = "chkOnlyLike";
            this.chkOnlyLike.Size = new System.Drawing.Size(186, 20);
            this.chkOnlyLike.TabIndex = 8;
            this.chkOnlyLike.Text = "Chỉ tương tác với sản phẩm";
            this.chkOnlyLike.UseVisualStyleBackColor = true;
            this.chkOnlyLike.CheckedChanged += new System.EventHandler(this.chkOnlyLike_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "File SSH:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "File Link:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(196, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "đến";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "đến";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "Random account:";
            // 
            // nudMaxAccount
            // 
            this.nudMaxAccount.Location = new System.Drawing.Point(237, 73);
            this.nudMaxAccount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxAccount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxAccount.Name = "nudMaxAccount";
            this.nudMaxAccount.Size = new System.Drawing.Size(57, 22);
            this.nudMaxAccount.TabIndex = 4;
            this.nudMaxAccount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Thời gian nghỉ:";
            // 
            // nudTimeMax
            // 
            this.nudTimeMax.Location = new System.Drawing.Point(237, 44);
            this.nudTimeMax.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTimeMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeMax.Name = "nudTimeMax";
            this.nudTimeMax.Size = new System.Drawing.Size(57, 22);
            this.nudTimeMax.TabIndex = 2;
            this.nudTimeMax.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnFileSSHPath
            // 
            this.btnFileSSHPath.Location = new System.Drawing.Point(328, 158);
            this.btnFileSSHPath.Name = "btnFileSSHPath";
            this.btnFileSSHPath.Size = new System.Drawing.Size(68, 22);
            this.btnFileSSHPath.TabIndex = 7;
            this.btnFileSSHPath.Text = "...";
            this.btnFileSSHPath.UseVisualStyleBackColor = true;
            this.btnFileSSHPath.Click += new System.EventHandler(this.btnFileSSHPath_Click);
            // 
            // btnFileLinkPath
            // 
            this.btnFileLinkPath.Location = new System.Drawing.Point(328, 130);
            this.btnFileLinkPath.Name = "btnFileLinkPath";
            this.btnFileLinkPath.Size = new System.Drawing.Size(68, 22);
            this.btnFileLinkPath.TabIndex = 6;
            this.btnFileLinkPath.Text = "...";
            this.btnFileLinkPath.UseVisualStyleBackColor = true;
            this.btnFileLinkPath.Click += new System.EventHandler(this.btnFileLinkPath_Click);
            // 
            // nudMinAccount
            // 
            this.nudMinAccount.Location = new System.Drawing.Point(126, 73);
            this.nudMinAccount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMinAccount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinAccount.Name = "nudMinAccount";
            this.nudMinAccount.Size = new System.Drawing.Size(57, 22);
            this.nudMinAccount.TabIndex = 3;
            this.nudMinAccount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudTimeMin
            // 
            this.nudTimeMin.Location = new System.Drawing.Point(126, 44);
            this.nudTimeMin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTimeMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeMin.Name = "nudTimeMin";
            this.nudTimeMin.Size = new System.Drawing.Size(57, 22);
            this.nudTimeMin.TabIndex = 1;
            this.nudTimeMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudCountThreads
            // 
            this.nudCountThreads.Location = new System.Drawing.Point(126, 16);
            this.nudCountThreads.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCountThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCountThreads.Name = "nudCountThreads";
            this.nudCountThreads.Size = new System.Drawing.Size(57, 22);
            this.nudCountThreads.TabIndex = 0;
            this.nudCountThreads.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Số thread:";
            // 
            // txtFileSSHPath
            // 
            this.txtFileSSHPath.Location = new System.Drawing.Point(125, 158);
            this.txtFileSSHPath.Name = "txtFileSSHPath";
            this.txtFileSSHPath.Size = new System.Drawing.Size(197, 22);
            this.txtFileSSHPath.TabIndex = 13;
            this.txtFileSSHPath.TabStop = false;
            this.txtFileSSHPath.Text = "C:\\Users\\Lang\\Desktop\\New Text Document.txt";
            // 
            // txtFileLinkPath
            // 
            this.txtFileLinkPath.Location = new System.Drawing.Point(125, 130);
            this.txtFileLinkPath.Name = "txtFileLinkPath";
            this.txtFileLinkPath.Size = new System.Drawing.Size(197, 22);
            this.txtFileLinkPath.TabIndex = 13;
            this.txtFileLinkPath.TabStop = false;
            this.txtFileLinkPath.Text = "C:\\Users\\Lang\\Desktop\\New Text Document.txt";
            // 
            // txtFileAccountPath
            // 
            this.txtFileAccountPath.Location = new System.Drawing.Point(126, 101);
            this.txtFileAccountPath.Name = "txtFileAccountPath";
            this.txtFileAccountPath.Size = new System.Drawing.Size(197, 22);
            this.txtFileAccountPath.TabIndex = 13;
            this.txtFileAccountPath.TabStop = false;
            this.txtFileAccountPath.Text = "C:\\Users\\Lang\\Desktop\\New Microsoft Excel Worksheet.xlsx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "File Account:";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 375);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Lazada";
            this.groupBox1.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCountThreads)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnFileAccountPath;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudTimeMax;
        private System.Windows.Forms.NumericUpDown nudTimeMin;
        private System.Windows.Forms.NumericUpDown nudCountThreads;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileAccountPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFileLinkPath;
        private System.Windows.Forms.TextBox txtFileLinkPath;
        private System.Windows.Forms.CheckBox chkOnlyLike;
        private System.Windows.Forms.RichTextBox rtxtResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnFileSSHPath;
        private System.Windows.Forms.TextBox txtFileSSHPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudMaxAccount;
        private System.Windows.Forms.NumericUpDown nudMinAccount;
        private System.Windows.Forms.CheckBox chkRemoveProduct;
    }
}

