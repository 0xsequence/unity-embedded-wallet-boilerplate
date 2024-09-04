using System;
using System.Collections;
using System.Collections.Generic;
using Sequence.Demo;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private LoginPanel _loginPanel;

    private void Awake()
    {
        _loginPanel = GetComponentInChildren<LoginPanel>();
    }

    private void Start()
    {
        _loginPanel.Open();
    }
}
