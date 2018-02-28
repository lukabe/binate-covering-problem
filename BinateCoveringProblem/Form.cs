using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinateCoveringProblem
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        // generate table for given x, y
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(textBox1.Text);
                int y = int.Parse(textBox2.Text);
                dgMatrix.ColumnCount = x;
                dgMatrix.RowCount = y;
            }
            catch
            {
                MessageBox.Show("Incorrect X,Y data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // generate solution
        private void btnSolve_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();

            try
            {
                // convert given matrix {1, 0, -1} to dictionary
                // initialize b, currSol 
                MyMatrix A = new MyMatrix(dgMatrix);

                // matrix is binate
                if (A.CheckBinate())
                {
                    BinateCovering bcp = new BinateCovering();
                    List<int> Solution = bcp.FindCovering(A.F, A.currSol, A.b, richTextBox2);

                    richTextBox1.Text = "CS = { " + bcp.PrintList(Solution);
                }
                // matrix is unate
                else
                {
                    UnateCovering ucp = new UnateCovering();
                    List<int> Solution = ucp.FindCovering(A.F, A.currSol, A.b, richTextBox2);

                    richTextBox1.Text = "CS = { " + ucp.PrintList(Solution);
                }
            }
            catch
            {
                MessageBox.Show("Incorrect matrix data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
