using RZHT_Model;
using System.Data;
using System.Text;
using System.Xml;
using System.Text.Encodings;
using System.Globalization;
using System.ComponentModel;
using RastrLibrary;
using ASTRALib;
using static RZHT_Model.EventForProtocolMessage;
using static RZHT_Model.MessageType;
using View.Properties;
using RZHT_Model.Forms;

namespace TestGUI
{
    public partial class MainForm : Form
    {
        public RZHT common;
        public DataTable optimizedSolution;
        public string sortingOreder;
        public RWin megaPoint;
        public LoadedRegimeFiles loadedRegimeFiles;

        public MainForm()
        {
            InitializeComponent();



            optimizationDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //AssociateAndRaiseEvents();
        }

        private void MainForm_load(object sender, EventArgs e)
        {
            CreateTableForProtocol(this.protocolGridView1);
            //this.protocolGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RZHT.Message += MessageHandler;
        }
        /*
        private void AssociateAndRaiseEvents()
        {
            event += delegate {}
        }

        public TimeOnly StartTime
        {
            get
            {

            }
            set
            {

            }
        }

        public TimeOnly FinishTime
        {
            get
            {

            }
            set
            {

            }
        }

        */

        private void �����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var qer = common.ConvertToDatatableDesignatedHour(common.UnsortedTable[int.Parse(this.toolStripComboBox1.Text.ToString())],
                this.toolStripComboBox2.Text == "��������" ? true : false);
            EditRZHTForm editRZHTForm = new EditRZHTForm(common.UnsortedTable[int.Parse(this.toolStripComboBox1.Text.ToString())], qer);
            editRZHTForm.Show();
        }

        private void optimizationButton2_Click(object sender, EventArgs e)
        {

        }

        private void WriteToDGV(DataGridView dgv, Dictionary<int, Dictionary<string, RankList>> table, bool up)
        {
            //RZTH to datatable
            var datatable = ConvertToDatatable(table, up);

            //dgv.source = datatable
            dgv.DataSource = datatable;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private DataTable ConvertToDatatable(Dictionary<int, Dictionary<string, RankList>> table, bool up)
        {
            var datatable = new DataTable();
            datatable.Columns.Add("���", typeof(int));
            datatable.Columns.Add("������������", typeof(string));
            datatable.Columns.Add("����", typeof(int));
            datatable.Columns.Add("���� ���", typeof(decimal));
            datatable.Columns.Add("���� ��", typeof(decimal));
            datatable.Columns.Add("������� ��������", typeof(decimal));
            if (up)
            {
                datatable.Columns.Add("������ �� ��������", typeof(decimal));
                datatable.Columns.Add("Pmax", typeof(decimal));
            }
            else
            {
                datatable.Columns.Add("������ �� ���������", typeof(decimal));
                datatable.Columns.Add("Pmin", typeof(decimal));
            }

            /*
            var divided = RZHT.DivideUPandDOWN(table);
            var rzhtUP = divided[0];
            var rzhtDOWN = divided[1];
            */
            foreach(var hourGouPair in table)
            {
                foreach(var gouTable in table[hourGouPair.Key])
                {
                    foreach(var rank in gouTable.Value)
                    {
                        if (up)
                        {
                            datatable.Rows.Add(hourGouPair.Key, gouTable.Key, rank.RankGou, rank.Price, rank.PriceBR, rank.Pbr, rank.ReserveUP, rank.Pmax);
                        }
                        else
                        {
                            datatable.Rows.Add(hourGouPair.Key, gouTable.Key, rank.RankGou, rank.Price, rank.PriceBR, rank.Pbr, rank.ReserveDOWN, rank.Pmin);
                        }
                        
                    }
                }
            }
            return datatable;
        }

        public void WriteResults(Dictionary<int, Dictionary<string, RankList>> optimizationResult, bool up)
        {
            var datatable = new DataTable();
            datatable.Columns.Add("���", typeof(int));
            datatable.Columns.Add("������������", typeof(string));
            datatable.Columns.Add("����", typeof(int));
            datatable.Columns.Add("���� ���. ���", typeof(decimal));
            datatable.Columns.Add("���� ��. ���", typeof(decimal));
            datatable.Columns.Add("���� � �������. ���", typeof(decimal));
            datatable.Columns.Add("������� ��������. ���", typeof(decimal));
            datatable.Columns.Add("��������� ��������. ���", typeof(decimal));
            datatable.Columns.Add("������ �� ��������. ���", typeof(decimal));

            foreach (var hourGouPair in optimizationResult)
            {
                foreach (var gouTable in optimizationResult[hourGouPair.Key])
                {
                    foreach (var rank in gouTable.Value)
                    {
                        datatable.Rows.Add(hourGouPair.Key, gouTable.Key, rank.RankGou, rank.Price, rank.PriceBR, (up == true ? rank.MaxCost : rank.MinCost), rank.Pbr, rank.LoadVolume, (up == true ? rank.ReserveUP:rank.ReserveDOWN));
                    }
                }
            }
            optimizationDataGridView.DataSource = datatable;
            optimizedSolution = datatable;

            var headers = new BindingList<string>();
            foreach(DataColumn column in datatable.Columns)
            {
                headers.Add(column.ColumnName);
            }

            DataView view = optimizedSolution.DefaultView;
            view.Sort = "��� ASC, ���� � �������. ��� ASC";
            optimizationDataGridView.DataSource = view;

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            optimizationDataGridView.DataSource = null;
            optimizationDataGridView.DataSource = optimizedSolution;
            optimizationDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm inputForm = new InputForm(this);
            this.�������ToolStripMenuItem.Visible = false;
            inputForm.Show();
            inputForm.FormClosed += (s,e) => this.�������ToolStripMenuItem.Visible = true;
        }

        private void toolStripTextBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("������������ ��������� ��������� �����������\n" +
                            "��������:\n" +
                            "��������� �.�. rbi2@tpu.ru, rusimankulov98@gmail.com",
                            "���������� � ���������", MessageBoxButtons.OK);
        }

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// ���������� ���������
        /// </summary>
        private void MessageHandler(object sender, EventProtocolMessage e)
        {
            this.Invoke((Action)delegate
            {
                AddMessageToDataGrid(e.MessageType, e.Message);

            });
        }

        // <summary>
        /// �������� ��������� � ��������
        /// </summary>
        public void AddMessageToDataGrid(MessageType type, string message)
        {
            switch (type)
            {
                case Error:
                    {
                        Bitmap img = new Bitmap(Resources.new_close);
                        this.protocolGridView1.Rows.Add(img, message); break;
                    }
                case Warning:
                    {
                        Bitmap img = new Bitmap(Resources.warning);
                        this.protocolGridView1.Rows.Add(img, message); break;
                    }
                case Info:
                    {
                        Bitmap img = new Bitmap(Resources.info);
                        this.protocolGridView1.Rows.Add(img, message); break;
                    }
            }
            this.protocolGridView1.FirstDisplayedCell = protocolGridView1.Rows[protocolGridView1.Rows.Count - 1].Cells[0];
            this.protocolGridView1.CurrentCell = null;
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        private void ClearProtocol_Click(object sender, EventArgs e)
        {
            this.protocolGridView1.Rows.Clear();
        }
        /// <summary>
        /// ����������� ���� - �������� ��������
        /// </summary>
        private void ClearProtocolMenuItem_Click(object sender, EventArgs e)
        {
            this.protocolGridView1.Rows.Clear(); 
        }

        /// <summary>
        /// �������, ��������� �������� � ��������� ������ �������� �� ���� ������
        /// </summary>
        private void ProtocolDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            protocolGridView1.ClearSelection();

        }

        public static void CreateTableForProtocol(DataGridView dataGridView)
        {
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn.Name = "type";
            dataGridView.Columns.Add(imgColumn);
            dataGridView.Columns.Add("message", "���������");
            dataGridView.Columns["type"].Width = 25;
            dataGridView.Columns["message"].Width = 1600;
            //dataGridView.Columns["message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["type"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["type"].Frozen = false;
            dataGridView.Columns["message"].Frozen = false;

            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.ScrollBars = ScrollBars.Both;
        }

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(openFileDialog.FileName);
                    RZHT rzht = RZHT.ReadXML(xDoc);
                    common = rzht;

                    // ������ ��� � ��������������� ���������
                    var dividedRzht = RZHT.DivideUPandDOWN(rzht.SortedTable);
                    WriteToDGV(loadDataGridView, dividedRzht[0], true);
                    WriteToDGV(unloadDataGridView, dividedRzht[1], false);

                }
                catch
                {
                    throw new DataException($"������ ��� �������� ����� {openFileDialog.FileName}"); ;
                }
            }
        }

        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MPTSMZ|*.mptsmz";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    megaPoint = RZHT.LoadMegapoint(openFileDialog.FileName);
                }
                catch
                {
                    throw new DataException($"������ ��� �������� ����� {openFileDialog.FileName}"); ;
                }
            }
        }

        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "��������� ������ ��� ��������� �� �����";
            loadedRegimeFiles = RZHT.LoadRegimes(openFileDialog);
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        // ���������� ��������� � ������������


    }
}