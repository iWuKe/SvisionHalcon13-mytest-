using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Svision
{
    public partial class ProcessEdit : Form
    {
        public ProcessEdit()
        {
            InitializeComponent();
        }

        private void TemplaMatch_Click(object sender, EventArgs e)
        {
            TemplateMatch TM = new TemplateMatch();
            TM.Show();
        }
    }
}
