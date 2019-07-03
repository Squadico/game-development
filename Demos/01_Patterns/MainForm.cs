using System.Windows.Forms;

namespace Demo.StatePattern
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SimpleStatePatternExampleButton_Click(object sender, System.EventArgs e)
        {
            var example = new StatePatternDemoForm(this);
            example.Show();
        }
    }
}
