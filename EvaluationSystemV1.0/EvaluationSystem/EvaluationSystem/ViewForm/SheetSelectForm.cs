using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using EvaluationSystem.Event;

namespace EvaluationSystem.ViewForm
{
    public partial class SheetSelectForm : Form
    {
        public event SheetSelectedEventHandler SheetSelectedEvent;
        private string fullFileName;
        private string[] sheetNames;

        public string FullFileName
        {
            get { return this.fullFileName; }
            set { this.fullFileName = value; }
        }

        public string[] SheetNames
        {
            get { return this.sheetNames; }
            set 
            {
                this.sheetNames = value;
                ComboBoxItemCollection coll = this.cmbSheetNames.Properties.Items;
                coll.Clear();
                try
                {
                    coll.BeginUpdate();
                    foreach (string sheetName in sheetNames)
                    {
                        coll.Add(sheetName);
                    }
                }
                finally
                {
                    coll.EndUpdate();
                }
                this.cmbSheetNames.SelectedIndex = 0;
            }
        }

        public SheetSelectForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string sheetName = this.cmbSheetNames.SelectedItem.ToString();
            if (SheetSelectedEvent != null)
            {
                SheetSelectedEvent(this, new SheetSelectedEventArgs(this.fullFileName, sheetName));
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
