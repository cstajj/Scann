namespace ScannMain
{
    partial class Main
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
            folderTextBox = new System.Windows.Forms.TextBox();
            folderButton = new System.Windows.Forms.Button();
            fileTextBox = new System.Windows.Forms.TextBox();
            fileButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            fileStartTextBox = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            celTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            rowTextBox = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            ttTextBox = new System.Windows.Forms.TextBox();
            yzMinTextBox = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            heightTextBox = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            yzMaxTextBox = new System.Windows.Forms.TextBox();
            widthTextBox = new System.Windows.Forms.TextBox();
            resTextBox = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            scannButton = new System.Windows.Forms.Button();
            stepTextBox = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            showCountCheckBox = new System.Windows.Forms.CheckBox();
            label9 = new System.Windows.Forms.Label();
            eqTextBox = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // folderTextBox
            // 
            folderTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            folderTextBox.Location = new System.Drawing.Point(4, 4);
            folderTextBox.Name = "folderTextBox";
            folderTextBox.Size = new System.Drawing.Size(172, 23);
            folderTextBox.TabIndex = 0;
            // 
            // folderButton
            // 
            folderButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            folderButton.Location = new System.Drawing.Point(187, 4);
            folderButton.Name = "folderButton";
            folderButton.Size = new System.Drawing.Size(120, 23);
            folderButton.TabIndex = 1;
            folderButton.Text = "选择文件夹";
            folderButton.UseVisualStyleBackColor = true;
            folderButton.Click += folderButton_Click;
            // 
            // fileTextBox
            // 
            fileTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            fileTextBox.Location = new System.Drawing.Point(4, 34);
            fileTextBox.Name = "fileTextBox";
            fileTextBox.Size = new System.Drawing.Size(172, 23);
            fileTextBox.TabIndex = 2;
            // 
            // fileButton
            // 
            fileButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            fileButton.Location = new System.Drawing.Point(187, 34);
            fileButton.Name = "fileButton";
            fileButton.Size = new System.Drawing.Size(120, 23);
            fileButton.TabIndex = 3;
            fileButton.Text = "选择单个文件";
            fileButton.UseVisualStyleBackColor = true;
            fileButton.Click += fileButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 67);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(68, 17);
            label1.TabIndex = 4;
            label1.Text = "文件前缀：";
            // 
            // fileStartTextBox
            // 
            fileStartTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            fileStartTextBox.Location = new System.Drawing.Point(78, 64);
            fileStartTextBox.Name = "fileStartTextBox";
            fileStartTextBox.Size = new System.Drawing.Size(226, 23);
            fileStartTextBox.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(4, 102);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 17);
            label2.TabIndex = 6;
            label2.Text = "列数：";
            // 
            // celTextBox
            // 
            celTextBox.Location = new System.Drawing.Point(54, 99);
            celTextBox.Name = "celTextBox";
            celTextBox.Size = new System.Drawing.Size(100, 23);
            celTextBox.TabIndex = 7;
            celTextBox.Text = "6";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(164, 102);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(44, 17);
            label3.TabIndex = 8;
            label3.Text = "行数：";
            // 
            // rowTextBox
            // 
            rowTextBox.Location = new System.Drawing.Point(200, 99);
            rowTextBox.Name = "rowTextBox";
            rowTextBox.Size = new System.Drawing.Size(100, 23);
            rowTextBox.TabIndex = 9;
            rowTextBox.Text = "4";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(4, 132);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(44, 17);
            label4.TabIndex = 10;
            label4.Text = "填涂：";
            // 
            // ttTextBox
            // 
            ttTextBox.Location = new System.Drawing.Point(54, 129);
            ttTextBox.Name = "ttTextBox";
            ttTextBox.Size = new System.Drawing.Size(100, 23);
            ttTextBox.TabIndex = 11;
            ttTextBox.Text = "1500";
            // 
            // yzMinTextBox
            // 
            yzMinTextBox.Location = new System.Drawing.Point(54, 158);
            yzMinTextBox.Name = "yzMinTextBox";
            yzMinTextBox.Size = new System.Drawing.Size(100, 23);
            yzMinTextBox.TabIndex = 13;
            yzMinTextBox.Text = "150";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(4, 161);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(44, 17);
            label5.TabIndex = 12;
            label5.Text = "阈值：";
            // 
            // heightTextBox
            // 
            heightTextBox.Location = new System.Drawing.Point(199, 187);
            heightTextBox.Name = "heightTextBox";
            heightTextBox.Size = new System.Drawing.Size(100, 23);
            heightTextBox.TabIndex = 15;
            heightTextBox.Text = "35";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(4, 190);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(44, 17);
            label6.TabIndex = 14;
            label6.Text = "宽高：";
            // 
            // yzMaxTextBox
            // 
            yzMaxTextBox.Location = new System.Drawing.Point(200, 158);
            yzMaxTextBox.Name = "yzMaxTextBox";
            yzMaxTextBox.Size = new System.Drawing.Size(100, 23);
            yzMaxTextBox.TabIndex = 16;
            yzMaxTextBox.Text = "255";
            // 
            // widthTextBox
            // 
            widthTextBox.Location = new System.Drawing.Point(54, 187);
            widthTextBox.Name = "widthTextBox";
            widthTextBox.Size = new System.Drawing.Size(100, 23);
            widthTextBox.TabIndex = 17;
            widthTextBox.Text = "50";
            // 
            // resTextBox
            // 
            resTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            resTextBox.Location = new System.Drawing.Point(4, 301);
            resTextBox.Multiline = true;
            resTextBox.Name = "resTextBox";
            resTextBox.Size = new System.Drawing.Size(301, 297);
            resTextBox.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(4, 279);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(68, 17);
            label7.TabIndex = 19;
            label7.Text = "运行结果：";
            // 
            // scannButton
            // 
            scannButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            scannButton.Location = new System.Drawing.Point(4, 604);
            scannButton.Name = "scannButton";
            scannButton.Size = new System.Drawing.Size(301, 23);
            scannButton.TabIndex = 20;
            scannButton.Text = "扫描";
            scannButton.UseVisualStyleBackColor = true;
            scannButton.Click += scannButton_Click;
            // 
            // stepTextBox
            // 
            stepTextBox.Location = new System.Drawing.Point(67, 244);
            stepTextBox.Name = "stepTextBox";
            stepTextBox.Size = new System.Drawing.Size(41, 23);
            stepTextBox.TabIndex = 22;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(4, 247);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(68, 17);
            label8.TabIndex = 21;
            label8.Text = "显示步骤：";
            // 
            // showCountCheckBox
            // 
            showCountCheckBox.AutoSize = true;
            showCountCheckBox.Location = new System.Drawing.Point(229, 246);
            showCountCheckBox.Name = "showCountCheckBox";
            showCountCheckBox.Size = new System.Drawing.Size(75, 21);
            showCountCheckBox.TabIndex = 23;
            showCountCheckBox.Text = "数量显示";
            showCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(4, 224);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(68, 17);
            label9.TabIndex = 24;
            label9.Text = "调试参数：";
            // 
            // eqTextBox
            // 
            eqTextBox.Location = new System.Drawing.Point(177, 244);
            eqTextBox.Name = "eqTextBox";
            eqTextBox.Size = new System.Drawing.Size(42, 23);
            eqTextBox.TabIndex = 26;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(114, 247);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(68, 17);
            label10.TabIndex = 25;
            label10.Text = "显示矩形：";
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(311, 634);
            Controls.Add(eqTextBox);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(showCountCheckBox);
            Controls.Add(stepTextBox);
            Controls.Add(label8);
            Controls.Add(scannButton);
            Controls.Add(label7);
            Controls.Add(resTextBox);
            Controls.Add(widthTextBox);
            Controls.Add(yzMaxTextBox);
            Controls.Add(heightTextBox);
            Controls.Add(label6);
            Controls.Add(yzMinTextBox);
            Controls.Add(label5);
            Controls.Add(ttTextBox);
            Controls.Add(label4);
            Controls.Add(rowTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(celTextBox);
            Controls.Add(label1);
            Controls.Add(fileStartTextBox);
            Controls.Add(fileButton);
            Controls.Add(fileTextBox);
            Controls.Add(folderButton);
            Controls.Add(folderTextBox);
            Name = "Main";
            Text = "扫描";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileStartTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox celTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox rowTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ttTextBox;
        private System.Windows.Forms.TextBox yzMinTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox yzMaxTextBox;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.TextBox resTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button scannButton;
        private System.Windows.Forms.TextBox stepTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox showCountCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox eqTextBox;
        private System.Windows.Forms.Label label10;
    }
}

