using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CPHInline
{
	public bool Execute()
	{	
		string rawInput = args["rawInput"].ToString();
		string message = "tippe <Nummer> um abzustimmen oder dein Vote zu ändern!";
		CPH.SetGlobalVar("pollvoters", "", true);
		CPH.SetGlobalVar("polloptions", "", true);
		bool quickpoll = false;
		string globaloptions = "";
		if (string.IsNullOrEmpty(args["rawInput"].ToString()))
		{
			CPH.LogInfo("polly has no content!");
			rawInput = "Quickpoll?Nummer1,Nummer2";
			quickpoll = true;
        }
		if(!rawInput.Contains("?"))
		{
			CPH.LogInfo("polly has no '?' !");
			CPH.SendMessage("'?' vergessen?", true);
			return true;
        }
		string[] input = rawInput.Split(new char[]{'?'}, 2); //StringSplitOptions.RemoveEmptyEntries);
		string[] options = input[1].Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);

		var length = options.Count();
		string topic = input[0].ToString()+"?";
		List<string> pollOptions = new List<string>();
        for (int i = 0; i < length ; i++)
        {
            pollOptions.Add(options[i].Trim());
			var validOption = i+1;
			globaloptions = globaloptions + " " + validOption;
        }
		string json_data = JsonConvert.SerializeObject(pollOptions);
		CPH.SetGlobalVar("polloptions", globaloptions, true);
		CPH.LogInfo("registered options: " + globaloptions.ToString());
		CPH.LogInfo("new polly. " + topic+ ": " + json_data);
		string json = "{'type': 'pollStarted', 'title': '"+topic+"', 'choices': "+json_data+", 'quickpoll': '"+ quickpoll +"'}";

		CPH.WebsocketBroadcastJson(json);
		CPH.SendMessage(message, true);
		return true;
	}
}