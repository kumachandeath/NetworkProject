using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum EStates { Idle, Walk, Attack, Dead, Size}
public class UnitController : MonoBehaviour, IDamageable
{
 
    [SerializeField] private AStar _aStar;
    public AStar AStar { get { return _aStar; } set { _aStar = value; } }

    [SerializeField] private UnitData _unitData;
    public UnitData UnitData { get { return _unitData; } set { } }

    private IState _currentState;

    private IState[] _states = new IState[(int)EStates.Size];    
    public IState[] States { get { return _states; } set { } }



    private void Awake()
    {
        _states[(int)EStates.Idle] = new IdleState(this);
        _states[(int)EStates.Walk] = new WalkState(this);
        _states[(int)EStates.Attack] = new AttackState(this);
        _states[(int)EStates.Dead] = new DeadState(this);

    }

    private void Start()
    {
        ChangeState(_states[(int)EStates.Idle]);

    }


    private void Update()
    {
        // ���� ��� üũ
        _unitData.DetectColider = Physics2D.OverlapCircleAll(this.transform.position, _unitData.DetectRadius);

        // Length == 1 ��, ������ ������� x.
        if(_unitData.DetectColider.Length == 1)
        {
            _unitData.DetectObject = null;
        }
        else
        {
            for (int i = 0; i < _unitData.DetectColider.Length; i++)
            {
                // �ڱ� �ڽ��̳� ��ֹ��� ����������� x.
                if (_unitData.DetectColider[i].gameObject != gameObject || _unitData.DetectColider[i].tag == "Obstacle")
                {
                    _unitData.DetectObject = _unitData.DetectColider[i];
                    break;
                }
            }
        }

        // Hit ��� üũ
        _unitData.HitColider = Physics2D.OverlapCircleAll(this.transform.position, _unitData.HitRadius);

        // Length == 1 ��, ������ ������� x. Player�� ���������� ���ݴ���� �������� ���. �������ݴ���� null ��
        if (_unitData.HitColider.Length == 1 || _unitData.AttackTarget != null)
        {
            _unitData.HitObject = null;
        }
        else
        {
            for (int i = 0; i < _unitData.HitColider.Length; i++)
            {
                // �ڱ� �ڽ��̳� ��ֹ��� Hit������� x.
                if (_unitData.DetectColider[i].gameObject != gameObject || _unitData.DetectColider[i].tag == "Obstacle")
                {
                    _unitData.HitObject = _unitData.HitColider[i];
                    break;
                }
            }
        }

        _currentState?.OnUpdate();
    }

    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit();
        }

        _currentState = newState;
        _currentState.OnEnter();
    }

    public void GetDamage(int damage)
    {
        _unitData.HP -= damage;
    }

}
