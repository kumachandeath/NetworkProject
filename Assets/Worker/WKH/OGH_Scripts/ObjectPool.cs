using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public enum EUnitType 
    {
        NONE = 0,
        MELEE = 1
    }

    public static ObjectPool Instance { get; set; }

    [SerializeField] private GameObject[] _poolingObj;                  // Ǯ���� ������Ʈ

    private Dictionary<EUnitType, List<GameObject>> _poolDict;         // ������Ʈ ��ųʸ�

    [SerializeField] GameObject[] _minimapRender;                         // �̴ϸʿ� ǥ�õ� ����


    private void Awake()
    {
        Instance = this;

        Init(25);
    }

    private void Init(int count)
    {
        _poolDict = new Dictionary<EUnitType, List<GameObject>>();
        for (int j = 0; j < _poolingObj.Length; j++)
        {
            EUnitType type = (EUnitType)(j + 1);
            _poolDict[type] = new List<GameObject>();

            for (int i = 0; i < count; i++)
            {
                GameObject poolObj = Instantiate(_poolingObj[j]);
                GameObject minimapRender = Instantiate(_minimapRender[j]);
                minimapRender.transform.parent = poolObj.transform;
                minimapRender.transform.position = poolObj.transform.position;
                poolObj.SetActive(false);
                _poolDict[type].Add(poolObj);
            }
        }
    }

    public GameObject GetObject(EUnitType type)
    {
        if (!_poolDict.ContainsKey(type))
        {
            return null;
        }
        
        foreach (GameObject poolObj in _poolDict[type])
        {
            if(!poolObj.activeSelf)
            {
                poolObj.SetActive(true);
                return poolObj;
            }
        }
        return null;
    }

    public void ReturnObject(GameObject gameObj)
    {
        gameObj.SetActive(false);

        // TODO : ���������� ���� ���� �� ObjectPool.Instance.ReturnObject(gameObject) �ش޶�� �ϱ�
    }
}
