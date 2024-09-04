using PlayFab;
using PlayFab.ClientModels;
using Samples.Scripts;
using Sequence.Demo;
using Sequence.EmbeddedWallet;
using Sequence.Utils;
using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class PlayFabLoginPage : UIPage
    {
        [SerializeField] private TMP_InputField _emailInputField;
        [SerializeField] private TMP_InputField _passwordInputField;
        [SerializeField] private TextMeshProUGUI _errorText;

        private string _email;
        private string _password;
        private SequenceLogin _sequenceLogin;
        private bool _federateAuth = false;
        
        public override void Open(params object[] args)
        {
            base.Open(args);
            _federateAuth = args.GetObjectOfTypeIfExists<bool>();
        }

        public void Login()
        {
            _email = _emailInputField.text;
            _password = _passwordInputField.text;
            _sequenceLogin = SequenceLogin.GetInstance();
            
            var request = new LoginWithEmailAddressRequest { Email = _email, Password = _password };
            PlayFabClientAPI.LoginWithEmailAddress(request,
                result =>
                {
                    _sequenceLogin.PlayFabLogin(PlayFabSettings.staticSettings.TitleId, result.SessionTicket, _email);
                }, OnLoginFailure);
        }
        
        private void OnLoginFailure(PlayFabError error)
        {
            string errorMessage = error.GenerateErrorReport();
            if (errorMessage.Contains("User not found"))
            {
                Debug.Log("User not found, creating new user");
                RegisterUser();
                return;
            }
            Debug.LogError(errorMessage);
            _errorText.text = errorMessage;
        }
        
        private void RegisterUser()
        {
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest { Email = _email, Password = _password, RequireBothUsernameAndEmail = false };
            PlayFabClientAPI.RegisterPlayFabUser(request,
                result =>
                {
                    _sequenceLogin.PlayFabLogin(PlayFabSettings.staticSettings.TitleId, result.SessionTicket, _email);
                }, error =>
                {
                    Debug.LogError(error.GenerateErrorReport());
                    _errorText.text = error.GenerateErrorReport();
                });
        }
    }
}