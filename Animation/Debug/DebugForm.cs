using System.Windows.Forms;

namespace UltimateAnimate.Debug
{
     partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

         internal void AddLine(string line)
        {
            richTextBox1.Text +=  "[ " + System.DateTime.Now.ToLongTimeString() +  " ]: " + (line + "\n");
        }
         internal void AddCommand(string command)
         {
             comboBox1.Items.Add(command);
         }
         internal void RemoveCommand(string command)
         {
             comboBox1.Items.Remove(command);
         }

         internal void SetInfoText(string text)
         {
             UseButtonInfo.Text = text;
         }

        private void button5_Click(object sender, System.EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                DebugWindow.OnCommandSubmitted(new CommandSubmittedEventArgs(comboBox1.SelectedItem as string));
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            DebugWindow.OnUseButtonPressed(new UseButtonEventArgs(DebugWindow.UseButtons.Button1));
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            DebugWindow.OnUseButtonPressed(new UseButtonEventArgs(DebugWindow.UseButtons.Button2));
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            DebugWindow.OnUseButtonPressed(new UseButtonEventArgs(DebugWindow.UseButtons.Button3));
        }

        private void button2_MouseEnter(object sender, System.EventArgs e)
        {
            DebugWindow.OnUseButtonMouseOver(new UseButtonEventArgs(DebugWindow.UseButtons.Button1));
        }

        private void button3_MouseEnter(object sender, System.EventArgs e)
        {
            DebugWindow.OnUseButtonMouseOver(new UseButtonEventArgs(DebugWindow.UseButtons.Button2));
        }

        private void button4_MouseEnter(object sender, System.EventArgs e)
        {
            DebugWindow.OnUseButtonMouseOver(new UseButtonEventArgs(DebugWindow.UseButtons.Button3));
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Text = string.Empty;
        }

         


    }
}
