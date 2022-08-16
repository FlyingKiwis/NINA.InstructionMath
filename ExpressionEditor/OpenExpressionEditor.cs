using NINA.InstructionMath.ExpressionUtil;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NINA.InstructionMath.ExpressionEditor {
    public class OpenExpressionEditor : ICommand {

        public event EventHandler CanExecuteChanged;

        public OpenExpressionEditor(IExpressionItem item, ExpressionVariables expressionVariables) 
        {
           
            _viewModel = new ExpressionEditorViewModel();
            _viewModel.Init(item, expressionVariables);
        }

        private readonly ExpressionEditorViewModel _viewModel;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                var window = new ExpressionEditorView(_viewModel);
                window.Show();
            }));
        }
    }
}
