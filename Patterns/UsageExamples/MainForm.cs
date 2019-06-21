using System.Windows.Forms;
using UsageExamples.SimpleStatePattern;

namespace UsageExamples
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SimpleStatePatternExampleButton_Click(object sender, System.EventArgs e)
        {
            var example = new SimpleStatePatternExampleForm(this);
            example.Show();
        }
    }
}
