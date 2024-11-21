using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WalkState : MonoBehaviour, IState
{
    private UnitController _unitController;

    public WalkState(UnitController controller)
    {
        //������
        _unitController = controller;
    }


    public void OnEnter()
    {
        Debug.Log("Walk���� ����");
    }

    public void OnUpdate()
    {       
        if (_unitController.UnitData.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (_unitController.UnitData.Path.Count == _unitController.UnitData.PathIndex)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
        }
        if (false)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Attack]);
        }

        DoWalk();

    }

    public void OnExit()
    {
        Debug.Log("Walk���� Ż��");
    }


    public void DoWalk()
    {
        Debug.Log("Walk ���� ������ (�ѹ�)");
    }

}
