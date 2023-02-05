using RZHT_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TestGUI
{
    public partial class InputForm : Form
    {
        MainForm mainForm;
        public InputForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(loadUPRadioButton.Checked == true)
            {
                loadDOWNRadioButton.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (loadDOWNRadioButton.Checked == true)
            {
                loadUPRadioButton.Checked = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                decimal load = Decimal.Parse(loadTextBox.Text, CultureInfo.InvariantCulture);
                bool checkSpeed = this.speedCheckBox.Checked;
                bool checkScheme = this.schemeCheckBox.Checked;

                int startTime = int.Parse(startHourTextBox.Text);
                int startTimeMinute = int.Parse(startMinuteTextBox.Text); ;
                int finishTime = int.Parse(finishHourTextBox.Text);
                int finishTimeMinutes = int.Parse(finishMinuteTextBox.Text);
                TimeOnly start = new TimeOnly(startTime, startTimeMinute);
                TimeOnly finish = new TimeOnly(finishTime, finishTimeMinutes);

                bool up = true;
                if (loadUPRadioButton.Checked == true && loadDOWNRadioButton.Checked == false)
                {
                    up = true;
                }
                else if (loadUPRadioButton.Checked == false && loadDOWNRadioButton.Checked == true)
                {
                    up = false;
                }

                GouSpeed gouSpeed = new GouSpeed();
                gouSpeed.CreateDefault(mainForm.common);
                LoadedRegimeFiles loadedRegimeFiles = mainForm.loadedRegimeFiles;
                RZHTtoSCHEME mappingDict = new RZHTtoSCHEME();
                mappingDict.BuildMapping(mainForm.common, mappingDict.BuildDefaultTemplate());
                int some = 0;

                var result = mainForm.common.Optimisation(start, finish, load, up, gouSpeed, loadedRegimeFiles, mappingDict, checkScheme, checkSpeed, some);
                mainForm.WriteResults(result, up);
                this.Close();
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
