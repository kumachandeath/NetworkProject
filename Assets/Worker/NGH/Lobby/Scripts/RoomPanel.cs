using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] PlayerEntry[] playerEntries;
    [SerializeField] Button startButton;

    private void OnEnable()
    {
        UpdatePlayers();

        PlayerNumbering.OnPlayerNumberingChanged += UpdatePlayers;

        PhotonNetwork.LocalPlayer.SetReady(false);
        //PhotonNetwork.LocalPlayer.SetLoad(false);
    }

    private void OnDisable()
    {
        PlayerNumbering.OnPlayerNumberingChanged -= UpdatePlayers;
    }

    public void UpdatePlayers()
    {
        foreach (PlayerEntry entry in playerEntries)
        {
            entry.SetEmpty();
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.GetPlayerNumber() == -1)
                continue;

            int number = player.GetPlayerNumber();
            playerEntries[number].SetPlayer(player);
        }

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startButton.interactable = CheckAllReady();
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void EnterPlayer(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} 입장!");
        UpdatePlayers();
    }

    public void ExitPlayer(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} 퇴장!");
        UpdatePlayers();
    }

    public void UpdatePlayerProperty(Player targetPlayer, Hashtable properties)
    {
        // 레디 커스텀 프로퍼티를 변경한 경우면 READY 키가 있음
        if (properties.ContainsKey(CustomProperty.READY))
        {
            UpdatePlayers();
        }
    }

    private bool CheckAllReady()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.GetReady() == false)
                return false;
        }

        return true;
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
