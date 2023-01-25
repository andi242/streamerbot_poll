using System;

public class CPHInline
{
    public bool Execute()
    {
        string json = "{'type': 'pollStop', 'stop': 'true'}";
        string users = CPH.GetGlobalVar<string>("pollvoters", true);
        if (!string.IsNullOrEmpty(users))
        {
            string[] globalvoters = users.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string user in globalvoters)
            {
                CPH.SetUserVar(user, "pollVote", "0", true);
                CPH.LogInfo("reset votes on " + user);
            }
        }
        else
        {
            CPH.LogInfo("no voters");
        }

        CPH.LogInfo("polly was stopped.");
        CPH.WebsocketBroadcastJson(json);
        return true;
    }
}