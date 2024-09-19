using System;
using System.Windows.Input;

namespace nrnUtil
{
    public class TerminateCommand : ICommand
    {
      

        public TerminateCommand()
        {
          
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Environment.Exit(0);
        }
    }
}