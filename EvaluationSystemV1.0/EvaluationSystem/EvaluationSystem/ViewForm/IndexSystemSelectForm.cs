using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvaluationSystem.Service;
using EvaluationSystem.Service.ServiceImpl;
using EvaluationSystem.Entity;
using DevExpress.XtraEditors.Controls;
using EvaluationSystem.Event;

namespace EvaluationSystem.ViewForm
{
    public partial class IndexSystemSelectForm : Form
    {
        public int SelectedIndexId;
        private IndexService indexService;
        public event IndexSystemSelectedEventHandler IndexSystemSelectedEvent;
        public IndexSystemSelectForm()
        {
            InitializeComponent();
            indexService = new IndexServiceImpl();
            InitializeIndexList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (IndexSystemSelectedEvent!=null)
            {
                IndexSystemSelectedEventArgs args = new IndexSystemSelectedEventArgs();
                args.SelectedIndexSystem = (IndexSystem)this.cmbIndexSystemNames.SelectedItem;
                IndexSystemSelectedEvent(this,args);
            }
            this.Close();
        }

        private void InitializeIndexList()
        {
            List<IndexSystem> list = this.indexService.FindAllIndexSystem();
            ComboBoxItemCollection coll = this.cmbIndexSystemNames.Properties.Items;
            try
            {
                coll.BeginUpdate();
                foreach (IndexSystem item in list)
                {
                    coll.Add(item);
                }
            }
            finally
            {
                coll.EndUpdate();
            }
            this.cmbIndexSystemNames.SelectedIndex = 0;
        }
    }
}
