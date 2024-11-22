using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WalkState : MonoBehaviour, IState
{
    private UnitController _unitController;

    private UnitData _data;

    private AStar _aStar;

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
    }

    public void OnUpdate()
    {
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
