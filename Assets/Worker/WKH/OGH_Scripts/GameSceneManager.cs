using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class GameSceneManager : MonoBehaviourPunCallbacks
{
    public const string RoomName = "TestRoom";

    private Coroutine _spawnUnitroutine;

    [SerializeField] private EGameState _gameState;

    [SerializeField] private int _playerCount;                          // �����ϱ� ���� ���� �÷��̾� ��

    [SerializeField] private UnitSpawner _spawner;                      // ������(Ŀ�ǵ弾��)

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

    IEnumerator WaitPlayerEnter()
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        while (PhotonNetwork.PlayerList.Length < _playerCount)
        {
            Debug.Log($"���� �÷��̾� �� : {PhotonNetwork.PlayerList.Length}");
            yield return delay;
        }
        Debug.Log("��� �÷��̾ �����߽��ϴ�. ���ӽ��� �غ� ��");
        StartCoroutine(StartDelayRoutine());
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(WaitPlayerEnter());
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

        // TODO : playercontroller Ŭ���̾�Ʈ���� ��ġ �ٸ��� �����ϴ� �κ�
        SetPlayerController();
        if (PhotonNetwork.IsMasterClient == false)
        {
            return;
        }
        //���常 ������ �� �ִ� �ڵ�


    }
    /// <summary>
    /// PlayerController�� Ŭ���̾�Ʈ���� �ٸ��� �����ϴ� �޼���
    /// </summary>
    private void SetPlayerController()
    {
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            switch (PhotonNetwork.LocalPlayer.GetPlayerNumber())
            {
                case 0:
                    GameObject playerController_zealot = PhotonNetwork.Instantiate("Prefabs/PlayerController_Zealot", Vector3.zero, Quaternion.identity);
                    break;
                case 1:
                    GameObject playerController_DarkTempler = PhotonNetwork.Instantiate("Prefabs/PlayerController_DarkTempler", Vector3.zero, Quaternion.identity);
                    break;
                case 2:
                    GameObject playerController_Zergling = PhotonNetwork.Instantiate("Prefabs/PlayerController_Zergling", Vector3.zero, Quaternion.identity);
                    break;
                case 3:
                    GameObject playerController_Ultrarisk = PhotonNetwork.Instantiate("Prefabs/PlayerController_Ultrarisk", Vector3.zero, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }

    }
}
