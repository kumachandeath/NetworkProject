using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using static Photon.Pun.UtilityScripts.PlayerNumbering;

public class TestGameScene : MonoBehaviourPunCallbacks
{

    public const string RoomName = "TestRoom";

    private Coroutine spawnRoutine;
    void Start()
    {
        PhotonNetwork.LocalPlayer.NickName = $"Player {Random.Range(1000, 10000)}";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsVisible = false;

        PhotonNetwork.JoinOrCreateRoom(RoomName, options, TypedLobby.Default);
    }
    
    public override void OnJoinedRoom()
    {
        StartCoroutine(StartDelayRoutine());
    }

    IEnumerator StartDelayRoutine()
    {
        yield return new WaitForSeconds(1f);
        TestGameStart();
    }
    public void TestGameStart()
    {
        Debug.Log("���� ����");

        //�׽�Ʈ�� ���� ���� �κ�
        Debug.Log($"�÷��̾� �ѹ� : {PhotonNetwork.LocalPlayer.GetPlayerNumber()}");

        if (PhotonNetwork.IsMasterClient == false)
        {
            return;
        }

        //���常 ������ �� �ִ� �ڵ�
        ObjectPool.Instance.Init(5);
    }

    //������ �ٲ�� �Ǹ� ���ο� ������ ������ 
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (newMasterClient.IsLocal)
        {
        }
    }
}