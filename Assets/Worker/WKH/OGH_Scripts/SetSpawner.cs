using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class SetSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private Vector3[] _spawnerPos;                     // ������ġ (��Ʈ��ũ ���Խ� �������� �ٸ��� ����)


    public override void OnConnectedToMaster()
    {
        Invoke("Spawn", 1);
    }

    private void Spawn()
    {
        //int playerNum = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        //Vector3 pos = _spawnerPos[playerNum];
        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = _spawnerPos[i];
            PhotonNetwork.Instantiate($"Prefabs/Spawner{i}", pos, Quaternion.identity);
        }
    }
}
