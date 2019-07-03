using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common
{
    public class Logger 
    {
        readonly RichTextBox textArea;

        public Logger(RichTextBox area)
        {
            textArea = area;
        }

        public void Error(string error)
        {
            if (textArea.InvokeRequired)
            {
                textArea.Invoke(new Action<string>(Error), error);
                return;
            }
            textArea.AppendText($"{error} {Environment.NewLine}", Color.DarkRed);
        }

        public void Message(string message)
        {
            if (textArea.InvokeRequired)
            {
                textArea.Invoke(new Action<string>(Message), message);
                return;
            }
            textArea.AppendText(message + Environment.NewLine);
        }

        public void Clear()
        {
            if (textArea.InvokeRequired)
            {
                textArea.Invoke(new Action(Clear));
                return;
            }
            textArea.Clear();
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
