using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : MonoBehaviourPun, IState
{
    private UnitController _unitController;

    private UnitData _data;

    private Animator _animator;

    private int _hashDead;

    private float _setFalsTime;

    private float _curTime;


    public DeadState(UnitController controller)
    {
        //������
        _unitController = controller;

        _data = _unitController.UnitData;

        _animator = _unitController.GetComponent<Animator>();

        _hashDead = Animator.StringToHash("Death");

        _setFalsTime = _data.SetFalseTime;

    }


    public void OnEnter()
    {
        _curTime = 0;
        Debug.Log("Dead���� ����");
        PlayDeadAnimation();
    }

    public void OnUpdate()
    {
        _curTime += Time.deltaTime;
        // �ִϸ��̼� ���� �� setfalse
        if(_curTime > _setFalsTime)
        {
            _unitController.gameObject.SetActive(false);
            //TO DO: ���� �� ���� �ڵ带 �� �ڵ�� ����
            //ObjectPool.Instance.ReturnObject(_unitController.gameObject);
        }
    }

    public void OnExit()
    {
        Debug.Log("Dead���� Ż��");
        StopAni();
    }

    // �״� �ִϸ��̼� ���� �� false ���ֱ�
    // �״� �ִϸ��̼��� �ݺ��� �ʿ�����Ƿ� roof false ���ֱ�.
    private void  PlayDeadAnimation()
    {
        _animator.Play(_hashDead);
    }

    private void StopAni()
    {
        _animator.StopPlayback();
    }


}
