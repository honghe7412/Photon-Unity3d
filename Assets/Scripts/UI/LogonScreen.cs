using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class LogonScreen : MonoBehaviour
{
    [SerializeField] private GameObject   registerPanel;
    [SerializeField] private GameObject   loginPanel;
    [SerializeField] private Ease         ease;
                     private Vector3      position;
                     private bool         isMove;

    [Header("Login Message")]
    [SerializeField] private InputField   loginUsername;
    [SerializeField] private InputField   loginPassword;
    [SerializeField] private LoginRequest loginRequest;

    [Header("Register Message")]
    [SerializeField] private InputField   registerUsername;
    [SerializeField] private InputField   registerPassword;
    [SerializeField] private RegisterRequest registerRequest;
    private void Start()
    {
        registerPanel.SetActive(true);
        loginPanel.SetActive(true);

        position = registerPanel.transform.position;
        position.y = position.y - Screen.height;
        registerPanel.transform.position = position;
    }

    public void OnLogin()
    {
        if (loginUsername.text == "" || loginPassword.text == "")
        {
            Debug.Log("用户名或者密码不能为空");
            return;
        }

        loginRequest.UserName = loginUsername.text;
        loginRequest.Password = loginPassword.text;

        loginRequest.DefaultRequest();
    }

    public void OnRegiste()
    {
        if (registerUsername.text == "" || registerPassword.text == "")
        {
            Debug.Log("用户名或者密码不能为空");
            return;
        }

        registerRequest.UserName = registerUsername.text;
        registerRequest.Password = registerPassword.text;

        registerRequest.DefaultRequest();
    }

    public void OnRegister()
    {
        MoveY(1);
    }

    public void OnReturn()
    {
        MoveY(-1);
    }

    private void MoveY(int IsMoveUpAndDown)
    {
        if (isMove)
            return;

        isMove = true;

        float curRegisterPanelY = registerPanel.transform.position.y;
        float curLoginPanelY = loginPanel.transform.position.y;

        registerPanel.transform.DOMoveY(curRegisterPanelY + Screen.height * IsMoveUpAndDown, 0.5f).SetEase(ease);
        loginPanel.transform.DOMoveY(curLoginPanelY + Screen.height * IsMoveUpAndDown, 0.5f).OnComplete(() =>
           {
               isMove = false;
           }).SetEase(ease);
    }

    public void LoginResponse(ReturnCode code)
    {
        Debug.Log(code);
        if (code == ReturnCode.Success)
            Debug.Log("登陆成功");
        else if(code == ReturnCode.Failed)
            Debug.Log("用户名或者密码错误");
    }

    public void RegisterResponse(ReturnCode code)
    {
        Debug.Log(code);
        if (code == ReturnCode.Success)
            Debug.Log("注册成功");
        else if (code == ReturnCode.Failed)
            Debug.Log("用户名已存在");
    }
}
