using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
{
    private UnitController _unitController;
    private UnitData _data;

    public IdleState(UnitController controller)
    {
        //������
        _unitController = controller;
        _data = _unitController.UnitData;
    }


    public void OnEnter()
    {
        Debug.Log("Idle���� ����");
    }

    public void OnUpdate()
    {
        if (_data.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (_data.Path.Count > 0 && _data.PathIndex != _data.Path.Count)        
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Walk]);
        }
        if (_data.DetectColider != null && _data.HitColider != null)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Attack]);
        }

        DoIdle();

    }

    public void OnExit()
    {
        Debug.Log("Idle���� Ż��");
    }


    //TO DO: �ִϸ��̼� �߰�
    public void DoIdle()
    {
        Debug.Log("Idle ���� ������");
        //Idle �ִϸ��̼� �߰�
    }

}