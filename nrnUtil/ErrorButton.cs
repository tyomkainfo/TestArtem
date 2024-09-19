using System.Windows.Input;

namespace nrnUtil
{
    public class ErrorButton
    {
        public static ErrorButton GetTerminationButton()
        {
            return new ErrorButton("Programm beenden", new TerminateCommand());
        }

        public ICommand Cmd;
        public string Bezeichnung;
        public ErrorButton(string bezeichnung, ICommand cmd)
        {
            Cmd = cmd;
            Bezeichnung = bezeichnung;
        }
    }
}