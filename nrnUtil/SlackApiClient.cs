using Slack.Webhooks;
using Slack.Webhooks.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace nrnUtil
{
    public class SlackApiClient
    {
        static String MessageFehlerWebHookUrl = "https://hooks.slack.com/services/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";


        public static void SendMSGFailed(string user, string workstation,string appserver, TimeSpan lastSeen)
        {
            string FailMesage = (lastSeen.TotalSeconds == 0) ? appserver + " seit Start nicht erreichbar" : appserver + " seit " + lastSeen.Seconds + " Sekunden offline";
            
            Slack.Webhooks.Elements.Element element = new Slack.Webhooks.Elements.Button
            {
                Text = new TextObject { Text = "Server neustarten" },
                ActionId = "TestAction",
            };

            var slackClient = new SlackClient(MessageFehlerWebHookUrl);
            SlackMessage msg = new SlackMessage();
            msg.Blocks = new List<Slack.Webhooks.Block>
            {
                new Slack.Webhooks.Blocks.Section
                {
                    Text = new TextObject()
                    {
                        Type = TextObject.TextType.Markdown,
                        Text = "*Messages Serer antwortet nicht! " + DateTime.Now.ToShortDateString() + "*\n " + FailMesage
                    },
                    Fields = new List<TextObject>
                    {
                        new TextObject { Type = TextObject.TextType.Markdown, Text = "*User:* " + user  },
                        new TextObject { Type = TextObject.TextType.Markdown, Text = "*workstation:* " + workstation }

                    },
                        Accessory = element
                },
                new Slack.Webhooks.Blocks.Divider(),
                    };
            //msg.Attachments = new List<SlackAttachment> { slackAttachment };
            try
            {
                slackClient.Post(msg);
            }
            catch 
            {

            }
        }
    }
}
