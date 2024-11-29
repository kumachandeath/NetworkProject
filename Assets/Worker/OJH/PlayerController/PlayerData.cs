using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerData : MonoBehaviourPun , IPunObservable
{
    [SerializeField] private EUnit _unitType;

    public EUnit UnitType { get { return _unitType; } private set { } }

    [SerializeField] private AStar _aStar;

    public AStar Astar { get { return _aStar; } private set { } }

    [SerializeField] private UnitSpawner _unitSpawner;

    public UnitSpawner UnitSpawner { get { return _unitSpawner; } private set { } }

    private List<UnitData> _units;

    public List<UnitData> Units { get { return _units; } set  { _units = value; } }


    [SerializeField] private GameObject _target;

    public GameObject Target { get { return _target; } set { _target = value; } }


    [SerializeField] private float _spawnTime;  // Unit�� ��ȯ �ֱ�

    public float SpawnTime { get { return _spawnTime; } set { _spawnTime = value; } }

    [SerializeField] private Vector3 _spawnPos; // Unit Spawn����.

    public Vector3 SpawnPos { get { return _spawnPos; } set { _spawnPos = value; } }

    [SerializeField] private int _hp; // Ŀ�ǵ弾�� ���� ü��
    public int HP { get { return _hp; } set { _hp = value; OnHpChanged?.Invoke(); } }

    [SerializeField] private int _maxHp; // Ŀ�ǵ弾�� Maxü��
    public int MaxHp { get { return _maxHp; } private set { } }

    public UnityAction OnHpChanged;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_hp);
        }
        else if (stream.IsReading)
        {
            _hp = (int)stream.ReceiveNext();
        }
    }
}
