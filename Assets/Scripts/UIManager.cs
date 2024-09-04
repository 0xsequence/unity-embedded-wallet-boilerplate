using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using Samples.Scripts;
using Sequence.Demo;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private LoginPanel _loginPanel;
    private BoilerplateSignMessagePanel _signMessagePanel;

    private void Awake()
    {
        _loginPanel = GetComponentInChildren<LoginPanel>();
        _signMessagePanel = GetComponentInChildren<BoilerplateSignMessagePanel>();
    }

    private void Start()
    {
        _loginPanel.Open();
    }

    public void OpenSignMessagePanel()
    {
        _signMessagePanel.Open(SequenceConnector.Instance.Wallet);
    }
}
