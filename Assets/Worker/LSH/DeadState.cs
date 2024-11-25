using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : MonoBehaviour, IState
{
    private UnitController _unitController;

    private UnitData _data;

    private Animator _animator;

    private int _hashDead;
    public DeadState(UnitController controller)
    {
        //������
        _unitController = controller;
        _data = _unitController.UnitData;

        _animator = _unitController.GetComponent<Animator>();

        _hashDead = Animator.StringToHash("Death");
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
        StopAni();
    }


    //TO DO: ���� ���� �ϼ��ϱ�
    public void Dead()
    {
        Debug.Log("Dead ���� (����)");
        _unitController.gameObject.SetActive(false);
        PlayDeadAnimation();
    }

    public void PlayDeadAnimation()
    {
        _animator.Play(_hashDead);
    }

    public void StopAni()
    {
        _animator.StopPlayback();
    }


}
