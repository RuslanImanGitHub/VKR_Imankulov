using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZHT_Model.Forms
{
    public partial class EditRZHTForm : Form
    {
        // При закрытии формы изменения перетекут в RZHT_Model
        public RankList commonRankList;
        public DataTable optimizedSolution;


        public event EventHandler<RankListEventArgs> RankListEdited;

        public EditRZHTForm(RankList rzhtHourSlice, DataTable data)
        {
            InitializeComponent();
            this.commonRankList = rzhtHourSlice;
            this.optimizedSolution = data;
            this.editDataGridView.DataSource = optimizedSolution;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            var q = new RankListEventArgs();
            q.rankList = commonRankList;
            RankListEdited?.Invoke(this, q);
            this.Close();
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            foreach(DataRow row in optimizedSolution.Rows)
            {
                foreach (var rank in commonRankList)
                {
                    if (rank.Gou == row["Наименование"].ToString() && rank.RankGou == int.Parse(row["Ранк"].ToString()))
                    {
                        // rank.Gou, rank.RankGou, rank.Price, rank.PriceBR, rank.Pbr, rank.LoadVolume, rank.ReserveUP, rank.SpeedBarierUP,  rank.Pmax
                        rank.LoadVolume = decimal.Parse(row["Изменение загрузки"].ToString());
                        rank.ReserveUP = decimal.Parse(row["Резерв на изменение нагрузки"].ToString());
                        rank.SpeedBarierUP = decimal.Parse(row["Резерв с учетом скорости"].ToString());
                    }
                }
            }
        }
    }
}
