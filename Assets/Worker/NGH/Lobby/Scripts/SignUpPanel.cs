using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using WebSocketSharp;

public class SignUpPanel : UIBInder
{
    //[SerializeField] TMP_InputField emailInputField;
    //[SerializeField] TMP_InputField passwordInputField;
    //[SerializeField] TMP_InputField passwordConfirmInputField;
    //private bool existsEmail = false;

    private void Awake()
    {
        BindAll();

        AddEvent("SignUpConfirmButton", EventType.Click, SignUp);
        AddEvent("SignUPCancelButton", EventType.Click, Cancel);
    }

    // ������ ����
    private void SignUp(PointerEventData eventData)
    {
        string email = GetUI<TMP_InputField>("SignUPEmailInputField").text; //emailInputField.text;
        string password = GetUI<TMP_InputField>("SignUPPasswordInputField").text; //passwordInputField.text;
        string confirm = GetUI<TMP_InputField>("SignUPPasswordConfirmInputField").text; //passwordConfirmInputField.text;

        if (email.IsNullOrEmpty())
        {
            Debug.LogWarning("�̸����� �Է����ּ���.");
            return;
        }

        if (password != confirm)
        {
            Debug.LogWarning("�н����尡 ��ġ���� �ʽ��ϴ�.");
            return;
        }

        /*CheckEmailExists(email, (exists) =>
        {
            if (exists)
            {
                Debug.LogWarning("�̹� �����ϴ� �����Դϴ�.");
                return;
            }
            else
            {
                Debug.Log("��� ������ �̸����Դϴ�.");
                
            }
        });*/

        CreateUser(email, password);
    }

    // �ش� â�� ����
    private void Cancel(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
    
    // ������ ���̽��� ���� ������ ���
    private void CreateUser(string email, string password)
    {
        BackendManager.Auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
                gameObject.SetActive(false);
            });
    }

    //private void CheckEmailExists(string email, System.Action<bool> callback)
    //{
    //    Firebase.Auth.FirebaseAuth.DefaultInstance.FetchProvidersForEmailAsync(email)
    //        .ContinueWithOnMainThread(task =>
    //        {
    //            if (task.IsCanceled)
    //            {
    //                Debug.LogError("FetchProvidersForEmailAsync was canceled.");
    //                return false;
    //            }
    //            if (task.IsFaulted)
    //            {
    //                Debug.LogError("FetchProvidersForEmailAsync encountered an error: " + task.Exception);
    //                return false;
    //            }

    //            // �̸��� �����ڰ� �ִٸ� �̹� ��ϵ� �̸���
    //            var providers = task.Result;
    //            return true;
    //        });
    //}
}
