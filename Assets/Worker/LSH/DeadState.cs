using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : MonoBehaviour, IState
{
    private UnitController _unitController;

    public DeadState(UnitController controller)
    {
        //������
        _unitController = controller;
    }


    public void OnEnter()
    {
        Debug.Log("Dead���� ����");
    }

    public void OnUpdate()
    {
        Dead();
    }

    public void OnExit()
    {
        Debug.Log("Dead���� Ż��");
    }


    //TO DO: ���� ���� �ϼ��ϱ�
    public void Dead()
    {
        Debug.Log("Dead ���� (����)");
        //�������̰�
        //�ִϸ��̼� �߰�
        //������Ʈ �����
    }


}
