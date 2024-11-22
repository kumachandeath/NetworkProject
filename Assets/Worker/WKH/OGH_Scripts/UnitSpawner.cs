using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UnitSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] Vector3 _spawnPos;                      // ���� ��ġ

    [SerializeField] private float _unitDistance = 0.1f;    // ���ְ��� �Ÿ�

    [SerializeField] private int _unitCount = 0;            // ���� ���� ī��Ʈ
    public int UnitCount { get { return _unitCount; } set { _unitCount = value; } }

    [SerializeField] private int _unitsInLine = 5;          // �� ���δ� ���� ���� ��

    [SerializeField] private float _time;                   // ���� �ð��� Ÿ��

    [SerializeField] private float _spawnTime;              // ���� �ð��� Ÿ��

    private void Update()
    {
        // if(!photonView.IsMine)
        // return;
        _time += Time.deltaTime;
        if(_spawnTime <= _time && _unitCount < 25)
        {
            _time = 0;
            Spawn();
        }
    }

    public void Spawn()
    {
        int col = _unitCount / _unitsInLine;                // ��
        int row = _unitCount % _unitsInLine;                // ��
        float colRatio = col * 0.3f;                        // ���� �� ���� ����
        float rowRatio = row * 0.3f;                        // ���� �� ���� ����

        _spawnPos = transform.position + new Vector3(1, 0.7f, 0);
        Vector3 newSpawnPos = _spawnPos + new Vector3(rowRatio, -colRatio, 0);
        UnitFactory meleeFact = new MeleeUnitFactory();
        meleeFact.Spawn(newSpawnPos);
        _unitCount++;   
    }
}
