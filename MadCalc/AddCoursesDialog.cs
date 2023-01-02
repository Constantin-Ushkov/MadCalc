using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MadCalc
{
    public partial class AddCoursesDialog : Form
    {
        public IEnumerable<int> Courses { get; set; }

        public AddCoursesDialog()
        {
            InitializeComponent();
        }

        private void uiClearBtn_Click(object sender, EventArgs e)
        {
            uiCoursesTb.Clear();
        }

        private void uiAddRangeBtn_Click(object sender, EventArgs e)
        {
            var from = uiFromUd.Value;
            var to = uiToUd.Value;
            var step = uiStepUd.Value;
            var value = from;

            uiCoursesTb.Text += $"{from:G29}, ";
            value += step - 1;

            while (value <= to)
            {
                uiCoursesTb.Text += $"{value:G29}, ";
                value += step;
            }

        }

        private void uiAddButton_Click(object sender, EventArgs e)
        {
            Courses = uiCoursesTb.Text
                .Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x))
                .ToArray();

            DialogResult = DialogResult.OK;
        }
    }
}
