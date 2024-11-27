using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviourPun
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


    [SerializeField] private int _unitCounts;   // Unit�� ���� ����.

    public int UnitCounts { get { return _unitCounts; } set { _unitCounts = value; } }

    [SerializeField] private Vector3 _spawnPos; // Unit Spawn����.

    public Vector3 SpawnPos { get { return _spawnPos; } set { _spawnPos = value; } }


}
