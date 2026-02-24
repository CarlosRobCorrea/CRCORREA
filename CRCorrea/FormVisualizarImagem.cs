using System.Windows.Forms;
using System.Drawing;

namespace CRCorrea
{
    public partial class FormVisualizarImagem : Form
    {
        public FormVisualizarImagem()
        {
            InitializeComponent();
        }
        public void init(string fileName)
        {
            pictureBox1.Image = Image.FromFile(fileName);
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
