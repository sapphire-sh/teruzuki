using System;

[Serializable]
public class Token
{
	public string AccessToken;
	public string AccessTokenSecret;

	public Token()
	{
		AccessToken = "";
		AccessTokenSecret = "";
	}
}
