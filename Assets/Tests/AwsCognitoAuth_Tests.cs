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
        AwsCognitoAuth auth = new AwsCognitoAuth();
        var token = auth.GetTokensAsync("ruhajaved2826@gmail.com", "Hello123!");
        token.Wait();
        // TODO: Figure out why Debug.Log does not log access token, just says
        // object, but Console.WriteLine does
        Console.WriteLine(token.Result.accessToken);
        // Debug.Log(token.Result.accessToken);
        Assert.NotNull(token.Result.accessToken);
    }
}
