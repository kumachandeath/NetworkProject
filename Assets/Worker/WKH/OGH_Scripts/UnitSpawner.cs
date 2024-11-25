using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UnitSpawner : MonoBehaviourPunCallbacks
{

    [SerializeField] private int _unitsInLine;          // �� ���δ� ���� ���� ��

    [SerializeField] private float _interval;

    public void Spawn(int unitNum, int unitCount, Vector3 spawnPos) // spawnPos�� Ŀ��弾�� ��ġ.
    {
        int col = unitCount / _unitsInLine;                // ��
        int row = unitCount % _unitsInLine;                // ��
        float colRatio = col * _interval;                        // ���� �� ���� ����
        float rowRatio = row * _interval;                        // ���� �� ���� ����

        Vector3 newSpawnPos = spawnPos + new Vector3(rowRatio, -colRatio, 0);

        switch (unitNum)
        {
            case (int)EUnit.Zealot:
                UnitFactory meleeFact = new ZealotFactory();
                meleeFact.Create(newSpawnPos);
                break;
            case (int)EUnit.DarkTemplar:
                meleeFact = new DarkTemplerFactory();
                meleeFact.Create(newSpawnPos);
                break;
            case (int)EUnit.Juggling:
                meleeFact = new JugglingFactory();
                meleeFact.Create(newSpawnPos);
                break;
            case (int)EUnit.Ultralisk:
                meleeFact = new UltraFactory();
                meleeFact.Create(newSpawnPos);
                break;
        }

        unitCount++;   
    }
}
