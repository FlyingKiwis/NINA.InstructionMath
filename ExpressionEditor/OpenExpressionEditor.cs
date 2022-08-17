using NINA.Astrometry.Interfaces;
using NINA.Core.Utility;
using NINA.InstructionMath.ExpressionUtil;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NINA.InstructionMath.ExpressionEditor {
    public class OpenExpressionEditor : ICommand {

        public event EventHandler CanExecuteChanged;

        public OpenExpressionEditor(IExpressionItem item, INighttimeCalculator nighttimeCalculator) 
        {
            _item = item;
            _nightCalc = nighttimeCalculator;
        }

        private readonly INighttimeCalculator _nightCalc;
        private readonly IExpressionItem _item;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            var viewModel = new ExpressionEditorViewModel();
            viewModel.Init(_item, _nightCalc);

            Logger.Info($"Opening expression editor for {_item}");

            Application.Current.Dispatcher.Invoke(new Action(() => {
                var window = new ExpressionEditorView(viewModel);
                window.Show();
            }));
        }
    }
}
