using System;
using System.Windows.Input;

namespace NINA.InstructionMath.Util {
    public class CommandEvent : ICommand {

        public event EventHandler Clicked;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
