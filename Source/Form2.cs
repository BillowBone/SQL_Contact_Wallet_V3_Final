using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraEditors;

namespace Annuaire_V3
{
    public partial class Form2 : XtraForm
    {
        private int _idSociete;
        private int currentContact;

        public Form2(int idSociete)
        {
            GridLocalizer.Active = new MyGridLocalizer();
            InitializeComponent();
            _idSociete = idSociete;
            currentContact = -20;
            repositoryItemComboBox1.Items.Add("");
            repositoryItemComboBox1.Items.Add("Mr.");
            repositoryItemComboBox1.Items.Add("Mrs.");
            repositoryItemComboBox1.Items.Add("Ms.");
            repositoryItemComboBox1.Items.Add("Miss.");
            repositoryItemComboBox1.Items.Add("Dr.");
            repositoryItemComboBox1.Items.Add("Prof.");
        }

        //Happens while the form is loading, fill the tables if the user asked for an existing society, creates a new society otherwise
        private void Form2_Load(object sender, EventArgs e)
        {
            if (_idSociete > 0)
            {
                societeTableAdapter.FillBy(annuaireDataSet.societe, _idSociete);
                contactTableAdapter.FillBy(annuaireDataSet.contact, _idSociete);
            }
            else
            {
                societeBindingSource.AddNew();
            }
        }

        //Manages which infosContact needs to be displayed depending on the actual selected contact
        private void ManageInfosContact(int idContact)
        {
            infoContactTableAdapter.Update(annuaireDataSet1.infoContact);
            infoContactTableAdapter.FillBy(annuaireDataSet1.infoContact, idContact);
            currentContact = idContact;
        }

        //Checks if the validation provider is valid and update the modifications to the database
        private bool Save()
        {
            if (dxValidationProvider1.Validate())
            {
                societeBindingSource.EndEdit();
                societeTableAdapter.Update(annuaireDataSet.societe);
                contactTableAdapter.Update(annuaireDataSet.contact);
                infoContactTableAdapter.Update(annuaireDataSet1.infoContact);
                _idSociete = annuaireDataSet.societe.First().id;
                return true;
            }
            return false;
        }

        //Button cancel event manager, set the exit status to cancel and close the form
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Button validate event manager, checks if every validations rules are OK
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //Click on the contacts gridcontrol manager
        private void gridView1_Click(object sender, EventArgs e)
        {
            if (contactBindingSource.Current != null)
            {
                AnnuaireDataSet.contactRow row = ((DataRowView)contactBindingSource.Current).Row as AnnuaireDataSet.contactRow;
                ManageInfosContact(row.id);
            }
        }

        //Focused column changed event manager
        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (contactBindingSource.Current != null)
            {
                AnnuaireDataSet.contactRow row = ((DataRowView)contactBindingSource.Current).Row as AnnuaireDataSet.contactRow;
                ManageInfosContact(row.id);
            }
        }

        //Focus row changed event manager
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (contactBindingSource.Current != null)
            {
                AnnuaireDataSet.contactRow row = ((DataRowView)contactBindingSource.Current).Row as AnnuaireDataSet.contactRow;
                ManageInfosContact(row.id);
            }
        }

        //Verifies if the non-nullables values of a new contact row are not empties
        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (contactBindingSource.Current != null)
            {
                AnnuaireDataSet.contactRow row = ((DataRowView)contactBindingSource.Current).Row as AnnuaireDataSet.contactRow;
                e.ErrorText = "   Vous devez entrer une valeur ici\nSouhaitez-vous corriger cette valeur ?";
                if (row.nom != "" && row.prenom != "")
                    e.Valid = true;
                else
                    e.Valid = false;
            }
        }

        //Verifies if the non-nullables values of a new infoContact row are not empties
        private void gridView2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (infoContactBindingSource.Current != null)
            {
                AnnuaireDataSet.infoContactRow row = ((DataRowView)infoContactBindingSource.Current).Row as AnnuaireDataSet.infoContactRow;
                e.ErrorText = "   Vous devez entrer une valeur ici\nSouhaitez-vous corriger cette valeur ?";
                if (row.typeInfo != "" && row.info != "")
                    e.Valid = true;
                else
                    e.Valid = false;
            }
        }

        //Manages the button on the right of each infoContact row which let you delete the row
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            if (infoContactBindingSource.Current != null)
            {
                DialogResult r = XtraMessageBox.Show("Voulez-vous vraiment supprimer cette information ?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    AnnuaireDataSet.infoContactRow row = ((DataRowView)infoContactBindingSource.Current).Row as AnnuaireDataSet.infoContactRow;
                    infoContactTableAdapter.Delete(row.id, row.typeInfo, row.info, row.idContact);
                    infoContactTableAdapter.FillBy(annuaireDataSet1.infoContact, row.idContact);
                }
            }
        }

        //Happens when a new contact row is being defined and set the idSociety cell so the user doesn't have to
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            Save();
            if (contactBindingSource.Current != null)
            {
                AnnuaireDataSet.contactRow row = ((DataRowView)contactBindingSource.Current).Row as AnnuaireDataSet.contactRow;
                row.idSociete = _idSociete;
            }
        }

        //Happens when a new infoContact row is being defined and set the idContact cell so the user doesn't have to
        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            Save();
            if (infoContactBindingSource.Current != null)
            {
                AnnuaireDataSet.infoContactRow row = ((DataRowView)infoContactBindingSource.Current).Row as AnnuaireDataSet.infoContactRow;
                row.idContact = currentContact;
            }
        }

        //Actualize the database when a new contact is created
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            Save();
        }
    }

    //This override deletes the english message displayed by default at the end of an InitNewRow error
    public class MyGridLocalizer : GridLocalizer
    {
        public override string GetLocalizedString(GridStringId id)
        {
            if (id == GridStringId.ColumnViewExceptionMessage)
                return string.Empty;
            return base.GetLocalizedString(id);
        }
    }
}
