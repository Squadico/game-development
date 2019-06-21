using System.Windows.Forms;

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
