namespace nrnUtil
{
    public class logable
    {
        public delegate void OnMessageHandler(object sender, LogEventArgs e);
        public event OnMessageHandler messageEvent;

        public logable()
        {

        }

        public void msg(string tmp)
        {
            if (messageEvent != null)
            {
                LogEventArgs e = new LogEventArgs();
                e.message = tmp;
                messageEvent.Invoke(this, e);
            }
        }


    }
}