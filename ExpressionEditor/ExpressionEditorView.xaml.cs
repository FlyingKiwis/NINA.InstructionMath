using NINA.InstructionMath.ExpressionUtil;
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

namespace NINA.InstructionMath.ExpressionEditor {
    /// <summary>
    /// Interaction logic for ExpressionEditorView.xaml
    /// </summary>
    public partial class ExpressionEditorView : Window {
        public ExpressionEditorView(ExpressionEditorViewModel expressionEditorVM) {
            InitializeComponent();
            DataContext = expressionEditorVM;
        }
    }
}
