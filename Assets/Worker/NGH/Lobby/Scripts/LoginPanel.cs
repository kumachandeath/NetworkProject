using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoginPanel : UIBInder
{
    private void Awake()
    {
        BindAll();

        AddEvent("LoginButton", EventType.Click, Login);
        AddEvent("SignUpButton", EventType.Click, ShowSignUpPanel);
        AddEvent("ResetPasswordButton", EventType.Click, ShowResetPasswordPanel);
    }

    // �α����� ����
    private void Login(PointerEventData eventData)
    {
        SoundManager.Instance.PlayUI("mousedown2");
        string email = GetUI<TMP_InputField>("EmailInputField").text; //emailInputField.text;
        string password = GetUI<TMP_InputField>("PasswordInputField").text;

        BackendManager.Auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    ShowErrorPopup("�α��� ��û�� ��ҵǾ����ϴ�.");
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    string errorMessage = GetFirebaseErrorMessage(task.Exception);
                    ShowErrorPopup(errorMessage);
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                AuthResult result = task.Result;
                Debug.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
                CheckUserInfo();
            });
    }

    // ȸ������ â Ȱ��ȭ
    private void ShowSignUpPanel(PointerEventData eventData)
    {
        GetUI("SignUpPanel").SetActive(true);
    }

    // ��й�ȣ �缳�� â Ȱ��ȭ
    private void ShowResetPasswordPanel(PointerEventData eventData)
    {
        GetUI("ResetPasswordPanel").SetActive(true);
    }

    // ������ ������ Ȯ���ϰ�, �ʿ� ������ �ִٸ� ����.
    // �ٸ� ������ �ʿ���ٸ� ��Ʈ��ũ�� ����.
    private void CheckUserInfo()
    {
        FirebaseUser user = BackendManager.Auth.CurrentUser;
        if (user == null)
            return;

        Debug.Log($"Display Name : {user.DisplayName}");
        Debug.Log($"Email : {user.Email}");
        Debug.Log($"Email verified : {user.IsEmailVerified}");
        Debug.Log($"User ID : {user.UserId}");

        if (user.IsEmailVerified == false)
        {
            GetUI("VerifyPanel").SetActive(true); //verifyPanel.SetActive(true);
        }
        else if (user.DisplayName == "")
        {
            GetUI("NickNamePanel").SetActive(true); //nickNamePanel.SetActive(true);
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = user.DisplayName;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // ������ �ִٸ� �˾�â�� �޽����� ���
    private void ShowErrorPopup(string message)
    {
        GetUI<TMP_Text>("ErrorPopupText").text = message; //errorPopupText.text = message;
        GetUI("ErrorPopup").SetActive(true);
        StartCoroutine(CloseErrorPopupCoroutine());
    }

    // �˾�â�� �ڵ����� �ݴ� �ڷ�ƾ
    private IEnumerator CloseErrorPopupCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CloseErrorPopup();
    }

    // �˾�â�� �ڵ����� �ݴ� �Լ�
    private void CloseErrorPopup()
    {
        GetUI("ErrorPopup").SetActive(false);
    }

    private bool IsValidEmail(string email)
    {
        // ������ �̸��� ���� ����
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private string GetFirebaseErrorMessage(AggregateException exception)
    {
        // Firebase���� ��ȯ�� ���� �޽��� �Ľ�
        foreach (var e in exception.InnerExceptions)
        {
            if (e is FirebaseException firebaseException)
            {
                switch ((AuthError)firebaseException.ErrorCode)
                {
                    case AuthError.MissingEmail:
                        return "�̸����� �Է����ּ���.";
                    case AuthError.MissingPassword:
                        return "��й�ȣ�� �Է����ּ���.";
                    case AuthError.InvalidEmail:
                        return "��ȿ���� ���� �̸��� �����Դϴ�.";
                    case AuthError.WrongPassword:
                        return "�߸��� ��й�ȣ�Դϴ�.";
                    case AuthError.UserNotFound:
                        return "�������� �ʴ� �����Դϴ�.";
                    default:
                        return "�α��� �� �� �� ���� ������ �߻��߽��ϴ�.";
                }
            }
        }
        return "��Ʈ��ũ ������ �߻��߽��ϴ�.";
    }
}
