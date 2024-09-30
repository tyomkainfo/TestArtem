namespace nrnUtil
{
    public class WebApiProblemMessage : nrnMessage
    {
        public string message;
        public WebApiProblemMessage()
        {
            type = nameof(WebApiProblemMessage);
        }
        public WebApiProblemMessage(string msg)
        {
            type = nameof(WebApiProblemMessage);
            message = msg;
        }
    }
}