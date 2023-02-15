using UnityEngine;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System.Threading.Tasks;

public class AwsCognitoAuth : MonoBehaviour
{
    private string _poolID = "ca-central-1_9P0HNlZKD";
    private string _clientID = "e576vvntvnekgrngackrfcc79";

    public async Task<string> GetTokensAsync()
    {
        AmazonCognitoIdentityProviderClient provider =
            new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials());
        CognitoUserPool userPool = new CognitoUserPool(_poolID, _clientID, provider);
        CognitoUser user = new CognitoUser("d56aae88-62da-49de-8340-5d7a257c6995", _clientID, userPool, provider);
        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
        {
            Password = "Password123!"
        };

        AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
        string accessToken = authResponse.AuthenticationResult.AccessToken;
        return accessToken;
    }
}