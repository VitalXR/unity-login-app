using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthScript : MonoBehaviour
{
    public GameObject errorText;

    string _poolID = "ca-central-1_OlaljS10Q";
    string _clientID = "13ipgiteb6lu6o3f3tlg70dr9q";

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Material errorMaterial;

    public async void AuthenticateUser()
    {
        errorText.SetActive(false);

        //string username = "d56aae88-62da-49de-8340-5d7a257c6995";
        //string password = "NewPassword456$";

        string username = usernameInput.text;
        string password = passwordInput.text;

        Token token = await GetTokensAsync(username, password);
        Debug.Log(token?.refreshToken);
        Debug.Log(token?.accessToken);

        if (token == null)
        {
            //Show error msg, maybe popup later
            errorText.SetActive(true);
            GameObject childButton = transform.GetChild(0).gameObject;
            childButton.GetComponent<Renderer>().sharedMaterial = errorMaterial;
        }
        else
        {
            //save token globally
            //switch scene
            SceneManager.LoadScene(1);
        }
    }
    public async Task<Token> GetTokensAsync(string username, string password)
    {
        // Setting up cognito auth request. need to include a region endpoint
        // in the provider client, unless something is set locally through AWS CLI.
        // TODO: add as env variable.
        AmazonCognitoIdentityProviderClient provider =
            new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), Amazon.RegionEndpoint.CACentral1);
        CognitoUserPool userPool = new CognitoUserPool(_poolID, _clientID, provider);
        CognitoUser user = new CognitoUser(username, _clientID, userPool, provider);
        // Using SRP instead of regular InitateAuth request because this requires
        // less upfront information like auth type. Easier to maintain.
        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
        {
            Password = password,
        };

        // Try to send auth request.
        try
        {
            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);

            // If result is not null, means we successfully authenticated.
            // Store tokens.
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
            // Otherwise check if new password required challenge is specified.
            // Occurs when a new user tries to authenticate, but they haven't updated
            // their account password (mandatory, no way to turn off). Throw approriate
            // error.
            else if (authResponse.ChallengeName == "NEW_PASSWORD_REQUIRED")
            {
                return null; 
                throw new AwsCognitoAuthException("User must set new password before logging in.");
            }
            // Otherwise, there was some other reason for an unsuccessful login - NOT
            // a bad username/password - see below. Throw appropriate error.
            else
            {
                return null;
                throw new AwsCognitoAuthException("Login attempt for user unsuccessful.");
            }
        }
        // TODO: Exception is approrpiatly caught, but this ends up throwing some
        //random exception causing issues - exceptioncommon.cs not found. Figure out.
        catch (Amazon.CognitoIdentityProvider.Model.NotAuthorizedException e)
        {
            return null;
            throw new AwsCognitoAuthException("Bad username or password.", e);
        }
        // Something went wrong while sending the request. Throw a vague error.
        catch (Exception e)
        {
            throw new AwsCognitoAuthException("something went wrong while authenticating user.", e);
        }
    }
}
