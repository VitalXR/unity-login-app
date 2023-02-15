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
    public async void GetAccessTokenTest()
    {
        // Use the Assert class to test conditions
        GameObject gameObject = new GameObject("AwsCognitoAuth");
        AwsCognitoAuth auth = gameObject.AddComponent<AwsCognitoAuth>();
        var token = await auth.GetTokensAsync();
        //Console.WriteLine(token);
        Assert.True(true);
    }

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator SceneLoader_TestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}
}
