using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WalkState : MonoBehaviour, IState
{
    private UnitController _unitController;

    private UnitData _data;

    private AStar _aStar;

    private Vector3 _currentAttackTarget;

    private float _checkAttackTargetTime;

    private Animator _animator;
    private int _hashWalkFront;
    private int _hashWalkBack;
    private int _hashWalkRight;

    public WalkState(UnitController controller)
    {
        //������
        _unitController = controller;
        _data = _unitController.UnitData;
        _aStar = _unitController.AStar;
    }

    private void Awake()
    {
        _hashWalkFront = Animator.StringToHash("Walk_Front");
        _hashWalkBack = Animator.StringToHash("Walk_Back");
        _hashWalkRight = Animator.StringToHash("Walk_Right");
    }


    public void OnEnter()
    {
        Debug.Log("Walk���� ����");

        // ��� ��Ž���� ���� �ʱ�ȭ.
        if (_data.AttackTarget != null)
        {
            _currentAttackTarget = _data.AttackTarget.transform.position;
        }
        _checkAttackTargetTime = 0;
    }

    //���� �ٲ��� ����!
    public void OnUpdate()
    {
        _checkAttackTargetTime += Time.deltaTime;

        // �̵� ������ ��α��� �̵��� ���´ٸ�, HasReceivedMove false.
        if (_data.PathIndex == _data.Path.Count)
        {
            _data.HasReceivedMove = false;
        }

        //������ȯ 
        if (_data.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (_data.HasReceivedMove == false)
        {
            if (_data.Path.Count == _data.PathIndex && _data.AttackTarget == null)
            {
                _unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
            }
            if (_data.HitObject != null)
            {
                _unitController.ChangeState(_unitController.States[(int)EStates.Attack]);
            }
        }

        // ��Ž��
        if (_data.AttackTarget != null && (_data.AttackTarget.transform.position != _currentAttackTarget && _checkAttackTargetTime > _data.FindLoadTime))
        {
            ReSearchPath();
        }

        // �̵�
        if(_data.PathIndex < _data.Path.Count)
        {
            DoWalk(_data.Path[_data.PathIndex]);
        }

    }

    public void OnExit()
    {
        Debug.Log("Walk���� Ż��");
    }

    // ��� ��Ž��
    public void ReSearchPath()
    {
        Debug.Log("��� ��ȯ!!!");
        _currentAttackTarget = _data.AttackTarget.transform.position;
        Vector2Int startPos = new Vector2Int((int)_unitController.transform.position.x, (int)_unitController.transform.position.y);
        Vector2Int endPos = new Vector2Int((int)_data.AttackTarget.transform.position.x, (int)_data.AttackTarget.transform.position.y);
        _aStar.DoAStar(startPos, endPos);
        _data.Path.Clear();
        _data.PathIndex = 0;
        _data.Path = _aStar.Path;
        _checkAttackTargetTime = 0;
    }

    public void DoWalk(Vector2Int pathPoint)
    {
        Debug.Log("Walk��!");
        // ���� ��ġ���� ��ǥ �������� �̵�
        _unitController.transform.position = Vector2.MoveTowards(_unitController.transform.position, pathPoint, _data.MoveSpeed * Time.deltaTime);

        // ��ǥ ������ �����ߴ��� Ȯ��
        if ((Vector2)_unitController.transform.position == pathPoint)
        {
            _data.PathIndex++;  // ���� �������� �̵�
        }

        PlayWalkAnimation();

    }

    //FIX ME: ���⿡ ���� �����¿� �ִϸ��̼� ��� �ʿ�
    public void PlayWalkAnimation()
    {
        _animator.Play(_hashWalkFront);
    }

}
