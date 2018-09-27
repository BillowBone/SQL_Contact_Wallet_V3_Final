using System;
using System.Windows.Forms;

namespace Annuaire_V3
{
    public partial class Form3 : Form
    {
        private AnnuaireDataSet annuaireDataSet = new AnnuaireDataSet();
        private AnnuaireDataSetTableAdapters.societeTableAdapter societeTableAdapter = new AnnuaireDataSetTableAdapters.societeTableAdapter();
        public int selectedSociety;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            societeTableAdapter.Fill(annuaireDataSet.societe);
            FillComboBox();
        }

        //Fills the combobox with the societies' names from the database
        private void FillComboBox()
        {
            for (int i = 0; i < annuaireDataSet.societe.Rows.Count; i++)
            {
                comboBoxEdit1.Properties.Items.Add(annuaireDataSet.societe.Rows[i][1]);
            }
        }

        //Checks if the selected society is valid
        private bool SocietySelected(string selected)
        {
            for (int i = 0; i < annuaireDataSet.societe.Rows.Count; i++)
            {
                if (annuaireDataSet.societe.Rows[i][1].ToString() == selected)
                {
                    selectedSociety = (int)annuaireDataSet.societe.Rows[i][0];
                    return (true);
                }
            }
            return (false);
        }

        //Manages the button that let you create a new society, which is next to the combobox's drop down button
        private void comboBoxEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                selectedSociety = -1;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //Button cancel event manager
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Button validate event manager, checks if the user selected one of the proposed societies
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (SocietySelected(comboBoxEdit1.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
