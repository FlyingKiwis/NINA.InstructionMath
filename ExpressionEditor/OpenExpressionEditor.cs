using NINA.Core.Utility;
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
            _item = item;
            _variables = expressionVariables;
        }

        private readonly ExpressionVariables _variables;
        private readonly IExpressionItem _item;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            var viewModel = new ExpressionEditorViewModel();
            viewModel.Init(_item, _variables);

            Logger.Info($"Opening expression editor for {_item}");

            Application.Current.Dispatcher.Invoke(new Action(() => {
                var window = new ExpressionEditorView(viewModel);
                window.Show();
            }));
        }
    }
}
