using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; set; }

    private List<GameObject>[] _poolDict;                   // 오브젝트 리스트

    private void Awake()
    {
        Instance = this;
        _poolDict = new List<GameObject>[4];
        for(int i = 0; i < _poolDict.Length; i++)
        {
            _poolDict[i] = new List<GameObject>();
        }     
    }

    public GameObject GetObject(int unitNum, Vector3 spawnPos)
    {
        GameObject select = null;

        foreach (GameObject poolObj in _poolDict[unitNum])
        {
            if(!poolObj.activeSelf)
            {
                select = poolObj;
                select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = PhotonNetwork.Instantiate($"Prefabs/Unit{unitNum}", spawnPos, Quaternion.identity);
            _poolDict[unitNum].Add(select);
        }

        return select;
    }

}
