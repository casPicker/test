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
    public partial class PDFViewer : Form
    {
        public PDFViewer(string path)
        {
            InitializeComponent();
            this.webBrowser1.Navigate(path);
        }
    }
}
