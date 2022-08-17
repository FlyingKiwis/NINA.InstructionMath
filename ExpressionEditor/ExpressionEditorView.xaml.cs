using System;
using System.Windows;

namespace NINA.InstructionMath.ExpressionEditor {
    /// <summary>
    /// Interaction logic for ExpressionEditorView.xaml
    /// </summary>
    public partial class ExpressionEditorView : Window {
        public ExpressionEditorView(ExpressionEditorViewModel expressionEditorVM) {
            InitializeComponent();
            _expressionEditorVM = expressionEditorVM;
            
            DataContext = expressionEditorVM;

            _expressionEditorVM.RequestCloseWindow += ExpressionEditorVM_RequestCloseWindow;
        }

        private ExpressionEditorViewModel _expressionEditorVM;

        private void ExpressionEditorVM_RequestCloseWindow(object sender, EventArgs e) {
            _expressionEditorVM.RequestCloseWindow -= ExpressionEditorVM_RequestCloseWindow;
            Close();
        }
    }
}
