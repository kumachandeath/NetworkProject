using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class DatabaseTester : MonoBehaviour
{
    [SerializeField] TMP_InputField searchInputField;
    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text playerLevelText;
    [SerializeField] int statIncreasePerLevel = 5; // ������ �� ���� ������

    private DatabaseReference userDataRef;
    private PlayerData currentPlayer;

    private void Start()
    {
        string uid = BackendManager.Auth.CurrentUser.UserId;
        userDataRef = BackendManager.Database.RootReference.Child("UserData").Child(uid);

        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        userDataRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("�����͸� �ҷ����� �� ���� �߻�");
                return;
            }

            DataSnapshot snapshot = task.Result;
            if (snapshot.Value == null)
            {
                CreateDefaultPlayerData();
            }
            else
            {
                currentPlayer = JsonUtility.FromJson<PlayerData>(snapshot.GetRawJsonValue());
                UpdateUI();
            }
        });
    }

    private void CreateDefaultPlayerData()
    {
        currentPlayer = new PlayerData
        {
            name = BackendManager.Auth.CurrentUser.DisplayName,
            email = BackendManager.Auth.CurrentUser.Email,
            level = 1,
            job = "Warrior", // �⺻ ���� ����
            stats = new PlayerStats { strength = 10, agility = 10, intelligence = 10, luck = 10 }
        };

        SavePlayerData();
    }

    private void SavePlayerData()
    {
        string json = JsonUtility.ToJson(currentPlayer);
        userDataRef.SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("������ ���� �� ���� �߻�");
            }
        });
    }

    private void UpdateUI()
    {
        playerLevelText.text = $"����: {currentPlayer.level}";
    }

    public void LevelUp()
    {
        currentPlayer.level++;
        currentPlayer.stats.strength += statIncreasePerLevel;
        currentPlayer.stats.agility += statIncreasePerLevel;
        currentPlayer.stats.intelligence += statIncreasePerLevel;
        currentPlayer.stats.luck += statIncreasePerLevel;

        SavePlayerData();
        UpdateUI();
    }

    public void SearchPlayer()
    {
        string playerName = searchInputField.text;
        BackendManager.Database.RootReference.Child("UserData")
            .OrderByChild("name")
            .EqualTo(playerName)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    resultText.text = "�˻� �� ������ �߻��߽��ϴ�.";
                    return;
                }

                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    foreach (var userSnapshot in snapshot.Children)
                    {
                        PlayerData playerData = JsonUtility.FromJson<PlayerData>(userSnapshot.GetRawJsonValue());
                        resultText.text = $"�÷��̾� ����:\n�̸�: {playerData.name}\n" +
                                          $"����: {playerData.level}\n" +
                                          $"����: {playerData.job}\n" +
                                          $"��: {playerData.stats.strength}\n" +
                                          $"��ø: {playerData.stats.agility}\n" +
                                          $"����: {playerData.stats.intelligence}\n" +
                                          $"���: {playerData.stats.luck}";
                    }
                }
                else
                {
                    resultText.text = "�������� �ʴ� �÷��̾��Դϴ�.";
                }
            });
    }
}

[Serializable]
public class PlayerData
{
    public string name;
    public string email;
    public int level;
    public string job;
    public PlayerStats stats;
}

[Serializable]
public class PlayerStats
{
    public int strength;
    public int agility;
    public int intelligence;
    public int luck;
}