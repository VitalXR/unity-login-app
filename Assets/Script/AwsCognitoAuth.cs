using UnityEngine;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System.Threading.Tasks;
using System;

public class AwsCognitoAuth
{
    string _poolID = "ca-central-1_9P0HNlZKD";
    string _clientID = "e576vvntvnekgrngackrfcc79";

    public async Task<Token> GetTokensAsync(string username, string password)
    {

        AmazonCognitoIdentityProviderClient provider =
            new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), Amazon.RegionEndpoint.CACentral1);
        CognitoUserPool userPool = new CognitoUserPool(_poolID, _clientID, provider);
        CognitoUser user = new CognitoUser(username, _clientID, userPool, provider);
        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
        {
            Password = password,
        };

        try
        {
            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);

            if (authResponse.AuthenticationResult != null)
            {
                Token token = new Token
                {
                    accessToken = authResponse.AuthenticationResult.AccessToken,
                    expiresIn = authResponse.AuthenticationResult.ExpiresIn,
                    idToken = authResponse.AuthenticationResult.IdToken,
                    refreshToken = authResponse.AuthenticationResult.RefreshToken,
                    tokenType = authResponse.AuthenticationResult.TokenType,
                };
                return token;
            }
            else if (authResponse.ChallengeName == "NEW_PASSWORD_REQUIRED")
            {
                throw new AwsCognitoAuthException("User must set new password before logging in.");
            }
            else
            {
                throw new AwsCognitoAuthException("Login attempt for user unsuccessful.");
            }
        }
        // TODO: Throw some random exception causing issues - exceptioncommon.cs not found. Figure out.
        //catch (Amazon.CognitoIdentityProvider.Model.NotAuthorizedException e)
        //{
        //    throw new AwsCognitoAuthException("Bad username or password.", e);
        //}
        catch (Exception e)
        {
            throw new AwsCognitoAuthException("something went wrong while authenticating user.", e);
        }
    }
}

public class Token
{
    public string accessToken;
    public int expiresIn;
    public string idToken;
    public string refreshToken;
    public string tokenType;
}

public class AwsCognitoAuthException : Exception
{
    public AwsCognitoAuthException(string message) : base(message)
    {
    }

    public AwsCognitoAuthException(string message, Exception inner) : base(message, inner)
    {
    }
}