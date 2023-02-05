namespace TestGUI
{
    partial class InputForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.loadUPRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.finishMinuteTextBox = new System.Windows.Forms.TextBox();
            this.finishHourTextBox = new System.Windows.Forms.TextBox();
            this.startMinuteTextBox = new System.Windows.Forms.TextBox();
            this.startHourTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loadDOWNRadioButton = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.loadTextBox = new System.Windows.Forms.TextBox();
            this.calculateButton = new System.Windows.Forms.Button();
            this.speedCheckBox = new System.Windows.Forms.CheckBox();
            this.schemeCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Задайте тип и время действия команды";
            // 
            // loadUPRadioButton
            // 
            this.loadUPRadioButton.AutoSize = true;
            this.loadUPRadioButton.Location = new System.Drawing.Point(10, 51);
            this.loadUPRadioButton.Name = "loadUPRadioButton";
            this.loadUPRadioButton.Size = new System.Drawing.Size(73, 19);
            this.loadUPRadioButton.TabIndex = 1;
            this.loadUPRadioButton.TabStop = true;
            this.loadUPRadioButton.Text = "Загрузка";
            this.loadUPRadioButton.UseVisualStyleBackColor = true;
            this.loadUPRadioButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.schemeCheckBox);
            this.panel1.Controls.Add(this.speedCheckBox);
            this.panel1.Controls.Add(this.finishMinuteTextBox);
            this.panel1.Controls.Add(this.finishHourTextBox);
            this.panel1.Controls.Add(this.startMinuteTextBox);
            this.panel1.Controls.Add(this.startHourTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.loadDOWNRadioButton);
            this.panel1.Controls.Add(this.loadUPRadioButton);
            this.panel1.Location = new System.Drawing.Point(19, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 109);
            this.panel1.TabIndex = 2;
            // 
            // finishMinuteTextBox
            // 
            this.finishMinuteTextBox.Location = new System.Drawing.Point(281, 76);
            this.finishMinuteTextBox.Name = "finishMinuteTextBox";
            this.finishMinuteTextBox.Size = new System.Drawing.Size(42, 23);
            this.finishMinuteTextBox.TabIndex = 10;
            // 
            // finishHourTextBox
            // 
            this.finishHourTextBox.Location = new System.Drawing.Point(230, 76);
            this.finishHourTextBox.Name = "finishHourTextBox";
            this.finishHourTextBox.Size = new System.Drawing.Size(42, 23);
            this.finishHourTextBox.TabIndex = 9;
            // 
            // startMinuteTextBox
            // 
            this.startMinuteTextBox.Location = new System.Drawing.Point(281, 50);
            this.startMinuteTextBox.Name = "startMinuteTextBox";
            this.startMinuteTextBox.Size = new System.Drawing.Size(42, 23);
            this.startMinuteTextBox.TabIndex = 8;
            // 
            // startHourTextBox
            // 
            this.startHourTextBox.Location = new System.Drawing.Point(230, 50);
            this.startHourTextBox.Name = "startHourTextBox";
            this.startHourTextBox.Size = new System.Drawing.Size(42, 23);
            this.startHourTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(273, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(273, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "По";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "С";
            // 
            // loadDOWNRadioButton
            // 
            this.loadDOWNRadioButton.AutoSize = true;
            this.loadDOWNRadioButton.Location = new System.Drawing.Point(10, 76);
            this.loadDOWNRadioButton.Name = "loadDOWNRadioButton";
            this.loadDOWNRadioButton.Size = new System.Drawing.Size(78, 19);
            this.loadDOWNRadioButton.TabIndex = 2;
            this.loadDOWNRadioButton.TabStop = true;
            this.loadDOWNRadioButton.Text = "Разгрузка";
            this.loadDOWNRadioButton.UseVisualStyleBackColor = true;
            this.loadDOWNRadioButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "Задайте мощность ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(189, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "МВт";
            // 
            // loadTextBox
            // 
            this.loadTextBox.Location = new System.Drawing.Point(139, 171);
            this.loadTextBox.Name = "loadTextBox";
            this.loadTextBox.Size = new System.Drawing.Size(42, 23);
            this.loadTextBox.TabIndex = 11;
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(300, 190);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(75, 23);
            this.calculateButton.TabIndex = 13;
            this.calculateButton.Text = "Расчет";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // speedCheckBox
            // 
            this.speedCheckBox.AutoSize = true;
            this.speedCheckBox.Location = new System.Drawing.Point(17, 14);
            this.speedCheckBox.Name = "speedCheckBox";
            this.speedCheckBox.Size = new System.Drawing.Size(172, 19);
            this.speedCheckBox.TabIndex = 11;
            this.speedCheckBox.Text = "Учитывать маневренность";
            this.speedCheckBox.UseVisualStyleBackColor = true;
            this.speedCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // schemeCheckBox
            // 
            this.schemeCheckBox.AutoSize = true;
            this.schemeCheckBox.Location = new System.Drawing.Point(198, 14);
            this.schemeCheckBox.Name = "schemeCheckBox";
            this.schemeCheckBox.Size = new System.Drawing.Size(138, 19);
            this.schemeCheckBox.TabIndex = 12;
            this.schemeCheckBox.Text = "Проверить на схеме";
            this.schemeCheckBox.UseVisualStyleBackColor = true;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 231);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.loadTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "InputForm";
            this.Text = "Задание на расчет";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private RadioButton loadUPRadioButton;
        private Panel panel1;
        private RadioButton loadDOWNRadioButton;
        private TextBox finishMinuteTextBox;
        private TextBox finishHourTextBox;
        private TextBox startMinuteTextBox;
        private TextBox startHourTextBox;
        private Label label4;
        private Label label5;
        private Label label3;
        private Label label2;
        private Label label8;
        private Label label9;
        private TextBox loadTextBox;
        private Button calculateButton;
        private CheckBox speedCheckBox;
        private CheckBox schemeCheckBox;
    }
}