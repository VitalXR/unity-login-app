using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AwsCognitoAuth_Tests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GetAccessTokenTest()
    {
        // Use the Assert class to test conditions
        AuthScript auth = new AuthScript();
        var token = auth.GetTokensAsync("d56aae88-62da-49de-8340-5d7a257c6995", "NewPassword456$");
        token.Wait();
        // TODO: Figure out why Debug.Log does not log access token, just says
        // object, but Console.WriteLine does
        Console.WriteLine(token.Result.accessToken);
        // Debug.Log(token.Result.accessToken);
        Assert.NotNull(token.Result.accessToken);
    }
}
