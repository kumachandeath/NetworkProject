using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour
{

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
