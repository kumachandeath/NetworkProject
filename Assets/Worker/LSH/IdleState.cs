using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class IdleState : IState
{
    private UnitController _unitController;

    private UnitData _data;

    private Animator _animator;

    private int _hashIdleFront;

    private int _hashIdleBack;

    private int _hashIdleRight;

    public IdleState(UnitController controller)
    {
        //������
        _unitController = controller;

        _data = _unitController.UnitData;

        _animator = _unitController.GetComponent<Animator>();

        _hashIdleFront = Animator.StringToHash("Idle_Front");
    }


    public void OnEnter()
    {
        Debug.Log("Idle���� ����");
        
    }

    public void OnUpdate()
    {
        if (_data.HP <= 0)
        {
            //_unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
            _unitController.photonView.RPC("ChangeState", RpcTarget.All, (int)EStates.Dead);
        }

        if (_data.Path.Count > 0 && _data.PathIndex != _data.Path.Count)        
        {
            // _unitController.ChangeState(_unitController.States[(int)EStates.Walk]);
            _unitController.photonView.RPC("ChangeState", RpcTarget.All, (int)EStates.Walk);
        }
        if (_data.HitObject != null)
        {
            // _unitController.ChangeState(_unitController.States[(int)EStates.Attack]);
            _unitController.photonView.RPC("ChangeState", RpcTarget.All, (int)EStates.Attack);
        }

        DoIdle();

    }

    public void OnExit()
    {
        Debug.Log("Idle���� Ż��");
    }


    private void DoIdle()
    {
        Debug.Log("Idle ���� ������");
        PlayIdleAnimation();

    }

    //FIX ME: Idle �ִϸ��̼��� �Ʒ� �������� ���⸸ ��� (24.11/15 16:30)
    private void PlayIdleAnimation()
    {
        _animator.Play(_hashIdleFront);             
    }

    private void StopAni()
    {
        _animator.StopPlayback();
    }


}
