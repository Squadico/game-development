﻿using Common;
using StatePattern;
using System.Windows.Forms;

namespace Demo.StatePattern
{
    public partial class StatePatternDemoForm : Form
    {
        MainForm Main;
        Logger Logger;
        ActivationLogic Logic;

        public StatePatternDemoForm(MainForm main)
        {
            Main = main;
            Main.Hide();

            InitializeComponent();

            Logger = new Logger(Log);
            Logic = new ActivationLogic();

            Logic.Enabled += () => Logger.Message("Enabled :)");
            Logic.Disabled += () => Logger.Message("Disabled :(");
        }

        private void SimpleStatePatternExampleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.Show();
        }

        private void EnableButton_Click(object sender, System.EventArgs e)
        {
            Logic.Enable();
        }

        private void DisableButton_Click(object sender, System.EventArgs e)
        {
            Logic.Disable();
        }
    }
}
