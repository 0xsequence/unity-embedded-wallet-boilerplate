using System;
using System.Threading.Tasks;
using Sequence.Authentication;
using Sequence.Demo;
using Sequence.EmbeddedWallet;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace Game.Scripts
{
    public class BoilerplateLoginPanel : LoginPanel
    {
        private PlayFabLoginPage _playFabLoginPage;

        protected override void Awake()
        {
            base.Awake();
            _playFabLoginPage = GetComponentInChildren<PlayFabLoginPage>();
        }

        public void OpenPlayFabLoginPage()
        {
            StartCoroutine(SetUIPage(_playFabLoginPage));
        }

        public void GuestLogin()
        {
            ILogin loginHandler = SequenceLogin.GetInstance();
            loginHandler.GuestLogin();
        }
    }
}