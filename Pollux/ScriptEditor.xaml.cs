using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für ScriptEditor.xaml
    /// </summary>
    public partial class ScriptEditor : Window
    {

        public ScriptEditor()
        {
            InitializeComponent();
        }

        private void SourceCode_TextChanged(object sender, EventArgs e)
        {
            TextRange textRange = new TextRange(this.SourceCode.Document.ContentStart, this.SourceCode.Document.ContentEnd);
            string str = textRange.Text;

            textRange.Text = "";

            List<string> vs = Split(str, ' ', '\n', '\t', '-');

            foreach (string i in vs)
            {
                switch (i)
                {
                    case "public": AppendText(this.SourceCode, i, Brushes.Red); break;
                    case "\n": break;
                    default: AppendText(this.SourceCode, i, Brushes.Black); break;
                }
            }
        }

        private static void AppendText(RichTextBox box, string i, Brush brush)
        {
            TextRange textRange = new TextRange(box.Document.Blocks.FirstBlock.ContentEnd, box.Document.Blocks.FirstBlock.ContentEnd);

            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, brush);

            textRange.Text += i;
        }

        public static List<string> Split(string str, params char[] vs)
        {
            List<string> vs1 = new List<string>();
            vs1.Add("");
            foreach(char i in str)
            {
                vs1[vs1.Count() - 1] += i;
                if (vs.Contains(i))
                {
                    vs1.Add("");
                }
            }
            return vs1;
        }
    }
}
