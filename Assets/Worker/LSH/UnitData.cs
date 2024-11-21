using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    [SerializeField] private int _hp;
    public int HP { get; set; }


    [SerializeField] private float _attackRange;
    public float AttackRange { get; set; }

    [SerializeField] private int _power;
    public int Power { get; set; }


    //FIX ME: path ��ΰ� �̰� �³���
    public List<Vector2Int> Path = new List<Vector2Int>();

    public int PathIndex = 0;

    [SerializeField] private float _moveSpeed;



    void Update()
    {
        // ��ǥ ������ �����ߴ��� Ȯ��
        if (Path.Count > 0 && PathIndex < Path.Count)
        {
            MoveTowardsTarget(Path[PathIndex]);
        }
    }

    // ��ǥ �������� �̵��ϴ� �Լ�
    void MoveTowardsTarget(Vector2Int pathPoint)
    {
        // ���� ��ġ���� ��ǥ �������� �̵�
        transform.position = Vector2.MoveTowards(transform.position, pathPoint, _moveSpeed * Time.deltaTime);

        // ��ǥ ������ �����ߴ��� Ȯ��
        if ((Vector2)transform.position == pathPoint)
        {
            PathIndex++;  // ���� �������� �̵�
        }
    }

}
