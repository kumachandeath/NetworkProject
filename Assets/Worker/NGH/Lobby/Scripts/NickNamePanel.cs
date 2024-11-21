using Firebase.Auth;
using Firebase.Extensions;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NickNamePanel : UIBInder
{
    [SerializeField] TMP_InputField nickNameInputField;

    public void Confirm()
    {
        string nickName = nickNameInputField.text;
        if(nickName == "")
        {
            Debug.LogWarning("�г����� �������ּ���.");
            return;
        }

        FirebaseUser user = BackendManager.Auth.CurrentUser;
        UserProfile profile = new UserProfile();
        profile.DisplayName = nickNameInputField.text;

        user.UpdateUserProfileAsync(profile)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
                Debug.Log($"Display Name : {user.DisplayName}");
                Debug.Log($"Email : {user.Email}");
                Debug.Log($"Email verified : {user.IsEmailVerified}");
                Debug.Log($"User ID : {user.UserId}");

                PhotonNetwork.LocalPlayer.NickName = nickName;
                PhotonNetwork.ConnectUsingSettings();
                gameObject.SetActive(false);
            });
    }
}
