namespace TestGUI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.ClearProtocolStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearProtocolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рЖТToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мегаточкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оцененныйРежимToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RZHTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьРЖТToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.расчетОптимизацииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обычныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.руководствоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.optimizationDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.unloadDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.loadDataGridView = new System.Windows.Forms.DataGridView();
            this.protocolGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ClearProtocolStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optimizationDataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unloadDataGridView)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.protocolGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClearProtocolStrip
            // 
            this.ClearProtocolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ClearProtocolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearProtocolMenuItem});
            this.ClearProtocolStrip.Name = "ClearProtocolStrip";
            this.ClearProtocolStrip.Size = new System.Drawing.Size(183, 26);
            // 
            // ClearProtocolMenuItem
            // 
            this.ClearProtocolMenuItem.Name = "ClearProtocolMenuItem";
            this.ClearProtocolMenuItem.Size = new System.Drawing.Size(182, 22);
            this.ClearProtocolMenuItem.Text = "Очистить протокол";
            this.ClearProtocolMenuItem.Click += new System.EventHandler(this.ClearProtocolMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.RZHTToolStripMenuItem,
            this.расчетОптимизацииToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1266, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьФайлToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(48, 20);
            this.toolStripTextBox1.Text = "Файл";
            // 
            // открытьФайлToolStripMenuItem
            // 
            this.открытьФайлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.рЖТToolStripMenuItem,
            this.мегаточкаToolStripMenuItem,
            this.оцененныйРежимToolStripMenuItem});
            this.открытьФайлToolStripMenuItem.Name = "открытьФайлToolStripMenuItem";
            this.открытьФайлToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.открытьФайлToolStripMenuItem.Text = "Открыть файл";
            this.открытьФайлToolStripMenuItem.Click += new System.EventHandler(this.открытьФайлToolStripMenuItem_Click);
            // 
            // рЖТToolStripMenuItem
            // 
            this.рЖТToolStripMenuItem.Name = "рЖТToolStripMenuItem";
            this.рЖТToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.рЖТToolStripMenuItem.Text = "РЖТ";
            this.рЖТToolStripMenuItem.Click += new System.EventHandler(this.рЖТToolStripMenuItem_Click);
            // 
            // мегаточкаToolStripMenuItem
            // 
            this.мегаточкаToolStripMenuItem.Name = "мегаточкаToolStripMenuItem";
            this.мегаточкаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.мегаточкаToolStripMenuItem.Text = "Мегаточка";
            this.мегаточкаToolStripMenuItem.Click += new System.EventHandler(this.мегаточкаToolStripMenuItem_Click);
            // 
            // оцененныйРежимToolStripMenuItem
            // 
            this.оцененныйРежимToolStripMenuItem.Name = "оцененныйРежимToolStripMenuItem";
            this.оцененныйРежимToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.оцененныйРежимToolStripMenuItem.Text = "Оцененный режим";
            this.оцененныйРежимToolStripMenuItem.Click += new System.EventHandler(this.оцененныйРежимToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // RZHTToolStripMenuItem
            // 
            this.RZHTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьРЖТToolStripMenuItem});
            this.RZHTToolStripMenuItem.Name = "RZHTToolStripMenuItem";
            this.RZHTToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.RZHTToolStripMenuItem.Text = "РЖТ";
            // 
            // редактироватьРЖТToolStripMenuItem
            // 
            this.редактироватьРЖТToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripComboBox2});
            this.редактироватьРЖТToolStripMenuItem.Name = "редактироватьРЖТToolStripMenuItem";
            this.редактироватьРЖТToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.редактироватьРЖТToolStripMenuItem.Text = "Редактировать РЖТ";
            this.редактироватьРЖТToolStripMenuItem.Click += new System.EventHandler(this.редактироватьРЖТToolStripMenuItem_Click);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "Загрузка",
            "Разгрузка"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 23);
            // 
            // расчетОптимизацииToolStripMenuItem
            // 
            this.расчетОптимизацииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обычныйToolStripMenuItem});
            this.расчетОптимизацииToolStripMenuItem.Name = "расчетОптимизацииToolStripMenuItem";
            this.расчетОптимизацииToolStripMenuItem.Size = new System.Drawing.Size(133, 20);
            this.расчетОптимизацииToolStripMenuItem.Text = "Расчет оптимизации";
            // 
            // обычныйToolStripMenuItem
            // 
            this.обычныйToolStripMenuItem.Name = "обычныйToolStripMenuItem";
            this.обычныйToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.обычныйToolStripMenuItem.Text = "Начать расчет";
            this.обычныйToolStripMenuItem.Click += new System.EventHandler(this.обычныйToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox3,
            this.руководствоToolStripMenuItem});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox3.Text = "О программе";
            this.toolStripTextBox3.Click += new System.EventHandler(this.toolStripTextBox3_Click);
            // 
            // руководствоToolStripMenuItem
            // 
            this.руководствоToolStripMenuItem.Name = "руководствоToolStripMenuItem";
            this.руководствоToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.руководствоToolStripMenuItem.Text = "Руководство";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Controls.Add(this.optimizationDataGridView);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1258, 376);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Результаты оптимизации";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 376);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1258, 0);
            this.panel4.TabIndex = 17;
            // 
            // optimizationDataGridView
            // 
            this.optimizationDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.optimizationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.optimizationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optimizationDataGridView.Location = new System.Drawing.Point(0, 0);
            this.optimizationDataGridView.Name = "optimizationDataGridView";
            this.optimizationDataGridView.RowTemplate.Height = 25;
            this.optimizationDataGridView.Size = new System.Drawing.Size(1258, 376);
            this.optimizationDataGridView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.unloadDataGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1258, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "РЖТ на разгрузку";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // unloadDataGridView
            // 
            this.unloadDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.unloadDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.unloadDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unloadDataGridView.Location = new System.Drawing.Point(3, 3);
            this.unloadDataGridView.Name = "unloadDataGridView";
            this.unloadDataGridView.RowTemplate.Height = 25;
            this.unloadDataGridView.Size = new System.Drawing.Size(1252, 370);
            this.unloadDataGridView.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.loadDataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1258, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "РЖТ на загрузку";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // loadDataGridView
            // 
            this.loadDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.loadDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loadDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadDataGridView.Location = new System.Drawing.Point(3, 3);
            this.loadDataGridView.Name = "loadDataGridView";
            this.loadDataGridView.RowTemplate.Height = 25;
            this.loadDataGridView.Size = new System.Drawing.Size(1252, 370);
            this.loadDataGridView.TabIndex = 0;
            // 
            // protocolGridView1
            // 
            this.protocolGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.protocolGridView1.ContextMenuStrip = this.ClearProtocolStrip;
            this.protocolGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protocolGridView1.Location = new System.Drawing.Point(0, 428);
            this.protocolGridView1.Name = "protocolGridView1";
            this.protocolGridView1.RowTemplate.Height = 25;
            this.protocolGridView1.Size = new System.Drawing.Size(1266, 170);
            this.protocolGridView1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1266, 404);
            this.tabControl1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 598);
            this.Controls.Add(this.protocolGridView1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Главная форма";
            this.Load += new System.EventHandler(this.MainForm_load);
            this.ClearProtocolStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optimizationDataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.unloadDataGridView)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.protocolGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem RZHTToolStripMenuItem;
        private ToolStripMenuItem помощьToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox3;
        private ToolStripMenuItem toolStripTextBox1;
        private ToolStripMenuItem открытьФайлToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem редактироватьРЖТToolStripMenuItem;
        private ToolStripMenuItem руководствоToolStripMenuItem;
        private ToolStripMenuItem расчетОптимизацииToolStripMenuItem;
        private ToolStripMenuItem обычныйToolStripMenuItem;
        private TabPage tabPage3;
        private Panel panel4;
        private DataGridView optimizationDataGridView;
        private TabPage tabPage2;
        private DataGridView unloadDataGridView;
        private TabPage tabPage1;
        private DataGridView protocolGridView1;
        private TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip ClearProtocolStrip;
        private System.Windows.Forms.ToolStripMenuItem ClearProtocolMenuItem;
        private ToolStripMenuItem рЖТToolStripMenuItem;
        private ToolStripMenuItem мегаточкаToolStripMenuItem;
        private ToolStripMenuItem оцененныйРежимToolStripMenuItem;
        private DataGridView loadDataGridView;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripComboBox toolStripComboBox2;
    }
}