using Photon.Pun;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviourPun
{
    public static ObjectPool Instance { get; set; }

    private List<GameObject>[] _poolDict;                   // ������Ʈ ����Ʈ

    public List<GameObject>[] PoolDict { get { return _poolDict; } set { _poolDict = value; } }
 
    private GameObject _select;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetPool();
    }

    public void SetPool()
    {
        _poolDict = new List<GameObject>[4];
        for (int i = 0; i < _poolDict.Length; i++)
        {
            _poolDict[i] = new List<GameObject>();
        }
    }


    public GameObject GetObject(int unitNum, Vector3 spawnPos)
    {
        _select = null;

        foreach (GameObject poolObj in _poolDict[unitNum])
        {
            // ������ �ƴ� setfalse�ϰ� settrue�ϴ� �κ��� ����ȭ�� �Ǿ����� �ʱ⶧���� Unit�ʿ��� RPc�� ���� �ٸ� PC������ ���̰��ϵ��ΰ���.
            if (!poolObj.activeSelf)
            {
                _select = poolObj;
                _select.SetActive(true);
                _select.transform.position = spawnPos;
                break;
            }
        }

        if (_select == null)
        {
            _select = PhotonNetwork.Instantiate($"Prefabs/Unit{unitNum}", spawnPos, Quaternion.identity);
            _poolDict[unitNum].Add(_select);
        }

        return _select;
    }
}
