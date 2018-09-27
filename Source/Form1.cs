using System;
using System.Data;
using System.Windows.Forms;

namespace Annuaire_V3
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.totalTableAdapter.Fill(this.annuaireDataSet.total);
            this.contacts_societesTableAdapter.Fill(this.annuaireDataSet.contacts_societes);
        }

        //Opens a form that let you edit a society's details and create or a edit a contact/infoContact
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            AnnuaireDataSet.totalRow row = ((DataRowView)totalBindingSource.Current).Row as AnnuaireDataSet.totalRow;
            Form2 f = new Form2(row.id2);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.totalTableAdapter.Fill(annuaireDataSet.total);
            }
        }

        //Opens a form that let you select the society you want to edit or create a new society
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Form2 f2 = new Form2(f.selectedSociety);
                if (f2.ShowDialog() == DialogResult.OK)
                {
                    this.totalTableAdapter.Fill(annuaireDataSet.total);
                }
            }
        }
    }
}
