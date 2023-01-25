using System;
using System.Linq;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
		string voteInput = args["command"].ToString();
		string globalvoters = CPH.GetGlobalVar<string>("pollvoters", true);
		string[] globaloptions = CPH.GetGlobalVar<string>("polloptions", true).Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
		var length = globaloptions.Count();
		string user = args["user"].ToString();
		string voteOld = CPH.GetUserVar<string>(user, "pollVote", true);
		CPH.LogInfo("range for: " + globaloptions.Count() );
		CPH.LogInfo("parse for: " + int.Parse(voteInput) );
		if(int.Parse(voteInput) > length){
			CPH.LogInfo("vote out of range for: " + user );
			return true;
		}
		if (string.IsNullOrEmpty(voteOld))
		{
			CPH.LogInfo("voteOld was empty");
			voteOld = "0";
        }
		CPH.LogInfo("voteOld: " + voteOld );
		CPH.SetUserVar(user, "pollVote", voteInput, true);
		CPH.LogInfo("voteInput: " + voteInput );
		if (string.IsNullOrEmpty(globalvoters))
		{
			CPH.LogInfo("init globalvoters");
			globalvoters = "";
        }
		
		if(voteOld == voteInput) 
		{
			CPH.LogInfo("vote not changed: " + user );
			return true;
		}else{
			CPH.LogInfo("vote changed: " + user );
		}
		if(!globalvoters.Contains(user))
		{
			globalvoters = globalvoters + " " + user;
		}
		
		CPH.SetGlobalVar("pollvoters", globalvoters, true);
		CPH.LogInfo("pollvoters: " + globalvoters );

		string json = "{'type': 'pollUpdated', 'vote': '" + voteInput +"', 'decreaseVote':" + voteOld +"}";
		CPH.LogInfo("json: " + json );
		CPH.WebsocketBroadcastJson(json);
		return true;
	}
}