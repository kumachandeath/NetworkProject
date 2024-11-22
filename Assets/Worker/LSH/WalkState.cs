using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WalkState : MonoBehaviour, IState
{
    private UnitController _unitController;

    private UnitData _data;

    private AStar _aStar;

    private Transform _currentAttackTarget;

    private float _checkAttackTargetTime;

    public WalkState(UnitController controller)
    {
        //������
        _unitController = controller;
        _data = _unitController.UnitData;
        _aStar = _unitController.AStar;
    }


    public void OnEnter()
    {
        Debug.Log("Walk���� ����");
        if(_data.AttackTarget != null)
        {
            _currentAttackTarget = _data.AttackTarget.transform;
        }
        _checkAttackTargetTime = 0;
    }

    public void OnUpdate()
    {
        _checkAttackTargetTime += Time.deltaTime;

        if (_data.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (_data.Path.Count == _data.PathIndex)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
        }
        if (_data.DetectObject != null && _data.HitObject != null)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Attack]);
        }

        if (_data.AttackTarget.transform.position != _currentAttackTarget.position && _checkAttackTargetTime > _data.FindLoadTime)
        {
            _currentAttackTarget.position = _data.AttackTarget.transform.position;
            Vector2Int startPos = new Vector2Int((int)_unitController.transform.position.x, (int)_unitController.transform.position.y);
            Vector2Int endPos = new Vector2Int((int)_data.AttackTarget.transform.position.x, (int)_data.AttackTarget.transform.position.y);
            _aStar.DoAStar(startPos, endPos);
            _data.Path.Clear();
            _data.PathIndex = 0;
            _data.Path = _aStar.Path;
        }

        if(_data.PathIndex < _data.Path.Count)
        {
            DoWalk(_data.Path[_data.PathIndex]);
        }

    }

    public void OnExit()
    {
        Debug.Log("Walk���� Ż��");
    }


    //TO DO: �ִϸ��̼� �߰�
    public void DoWalk(Vector2Int pathPoint)
    {
        // ���� ��ġ���� ��ǥ �������� �̵�
        _unitController.transform.position = Vector2.MoveTowards(_unitController.transform.position, pathPoint, _data.MoveSpeed * Time.deltaTime);

        // ��ǥ ������ �����ߴ��� Ȯ��
        if ((Vector2)_unitController.transform.position == pathPoint)
        {
            _data.PathIndex++;  // ���� �������� �̵�
        }

    }


}
