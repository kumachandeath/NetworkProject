using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
{
    private UnitController _unitController;

    public IdleState(UnitController controller)
    {
        //������
        _unitController = controller;
    }


    public void OnEnter()
    {
        Debug.Log("Idle���� ����");
    }

    public void OnUpdate()
    {
        if (_unitController.UnitData.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (_unitController.UnitData.Path.Count > 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Walk]);
        }
        if (false)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Attack]);
        }

        DoIdle();

    }

    public void OnExit()
    {
        Debug.Log("Idle���� Ż��");
    }


    public void DoIdle()
    {
        Debug.Log("Idle ���� ������ (�ֶ�)");
    }

}
