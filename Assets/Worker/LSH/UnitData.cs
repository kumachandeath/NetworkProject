using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EUnit {Zealot, DarkTemplar, Juggling, Ultralisk}
public class UnitData : MonoBehaviour
{
    [SerializeField] private EUnit _unitType;
    public EUnit UnitType { get { return _unitType;} private set { } }

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

    [SerializeField] private Collider2D[] _hitColiders; // ���ݰ����� ���� 
    public Collider2D[] HitColiders { get { return _hitColiders; }  set { _hitColiders = value; } }

    [SerializeField] private Collider2D _hitObject; // ���� ���� ��� 
    public Collider2D HitObject {  get { return _hitObject; } set { _hitObject = value; } }

    [SerializeField] private GameObject _attackTarget; // Player�� ���� ������ ���� ��� 
    public GameObject AttackTarget { get { return _attackTarget; } set { _attackTarget = value; } }

    [SerializeField] private float _hitRadius; // ���� ������ ����
    public float HitRadius { get { return _hitRadius; } private set { } }

    [SerializeField] private float _findLoadTime; // �� ��ã�� �ֱ�
    public float FindLoadTime { get { return _findLoadTime; } private set { } }

    [SerializeField] private bool _hasReceivedMove; // �̵������ �޾Ҵ��� ���� 
    public bool HasReceivedMove { get { return _hasReceivedMove; } set { _hasReceivedMove = value; } }

    [SerializeField] private Animator _animator; // �ִϸ�����
    public Animator Animator { get { return _animator; } set { } }

}
