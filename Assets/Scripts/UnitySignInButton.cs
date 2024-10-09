using System;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using Sequence.EmbeddedWallet;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace Game.Scripts
{
    public class UnitySignInButton : MonoBehaviour
    {
        private void Start()
        {
            InitializeUnityServices().Start();
        }

        private async Task InitializeUnityServices()
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName("production");
                await Unity.Services.Core.UnityServices.InitializeAsync(options);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
            }
        }

        public void SignInWithUnity()
        {
            SignInAnonymouslyAsync().Start();
        }
        
        async Task SignInAnonymouslyAsync()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
                AuthenticateWithPlayFab(AuthenticationService.Instance.PlayerId);
            }
            catch (AuthenticationException ex)
            {
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                Debug.LogException(ex);
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    
        void AuthenticateWithPlayFab(string unityPlayerID)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = unityPlayerID, 
                CreateAccount = true  
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        }

        void OnLoginSuccess(LoginResult result)
        {
            SequenceLogin login = SequenceLogin.GetInstance();
            login.PlayFabLogin(PlayFabSettings.staticSettings.TitleId, result.SessionTicket, "");
        }

        void OnLoginFailure(PlayFabError error)
        {
            Debug.LogError("Failed to log in to PlayFab via Unity: " + error.GenerateErrorReport());
        }
    }
}