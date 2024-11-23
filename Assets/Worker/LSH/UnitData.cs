using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    [SerializeField] private int _hp;  // ü��
    public int HP { get { return _hp; }  set { _hp = value; } }

    [SerializeField] private int _power;  //���ݷ� 
    public int Power { get { return _power; } private set { } }

    [SerializeField] private float _damageRate; // ������ �ֱ� �ð�
    public float DamageRate { get { return _damageRate; } private set { } }

    [SerializeField] private List<Vector2Int> _path = new List<Vector2Int>();  // �̵� ���
    public List<Vector2Int> Path { get { return _path; } set { _path = value; } }

    [SerializeField] private int _pathIndex; // ���� � ��η� �̵� ������ ����.
    public int PathIndex { get { return _pathIndex; } set { _pathIndex = value; } }

    [SerializeField] private float _moveSpeed; // �̵� �ӵ�
    public float MoveSpeed { get{ return _moveSpeed; } private set { } }

    [SerializeField] private Collider2D[] _detectColider; // ������ ���� 
    public Collider2D[] DetectColider { get { return _detectColider; }  set { _detectColider = value; } }

    [SerializeField] private Collider2D _detectObject; // ���� Ư�� ��� 
    public Collider2D DetectObject { get { return _detectObject; } set { _detectObject = value; } }

    [SerializeField] private Collider2D[] _hitColider; // ���ݰ����� ���� 
    public Collider2D[] HitColider { get { return _hitColider; }  set { _hitColider = value; } }

    [SerializeField] private Collider2D _hitObject; // ���� ���� ��� 
    public Collider2D HitObject {  get { return _hitObject; } set { _hitObject = value; } }

    [SerializeField] private GameObject _attackTarget; // Player�� ���� ������ ���� ��� 
    public GameObject AttackTarget { get { return _attackTarget; } set { _attackTarget = value; } }

    [SerializeField] private float _detectRadius; // ���� ������ ����
    public float DetectRadius { get { return _detectRadius; } private set { } }

    [SerializeField] private float _hitRadius; // ���� ������ ����
    public float HitRadius { get { return _hitRadius; } private set { } }

    [SerializeField] private float _findLoadTime; // �� ��ã�� �ֱ�
    public float FindLoadTime { get { return _findLoadTime; } private set { } }

    [SerializeField] private bool _hasReceivedMove; // �̵������ �޾Ҵ��� ���� 
    public bool HasReceivedMove { get { return _hasReceivedMove; } set { _hasReceivedMove = value; } }

    [SerializeField] private bool _hasReceivedAttack; //���ݸ���� �޾Ҵ��� ���� 
    public bool HasReceivedAttack { get { return _hasReceivedAttack; } set { _hasReceivedAttack = value; } }



}
