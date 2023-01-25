using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShapesLib; 

namespace CNCCoil_Base
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            CNCGrid.AutoGenerateColumns = true; 

            List<CNCNode> nodes = new List<CNCNode>()
            {
                new CNCNode(ShapesLib.CommandType.Line, new CommandParams(){ X = 15.2, Y = 14.8,}),
                new CNCNode(ShapesLib.CommandType.CWArc, new CommandParams(){ X = 24.7, Y = 4.12, R = 3 }),
            };

            CNCGrid.DataSource = nodes;

        }
    }
}
