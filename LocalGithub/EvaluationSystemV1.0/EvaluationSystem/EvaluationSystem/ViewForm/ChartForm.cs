using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvaluationSystem.ViewForm
{
    public partial class ChartForm : Form
    {
        public ChartForm(string path)
        {
            InitializeComponent();
            chartPicBox.Image = Image.FromFile(path);
        }
    }
}
