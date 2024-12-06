using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private UnitController _unitController;

    private UnitData _data;

    private Collider2D _detectEnemy;

    private IDamageable _damageAble;

    private float _curDamageRate;

    private Animator _animator;

    private int _hashAttackFront;

    private int _hashAttackBack;

    private int _hashAttackRight;

    private Vector3 _attackDir; //���ݹ���

    private SpriteRenderer _render;

    public AttackState(UnitController controller)
    {
        //������
        _unitController = controller;
        _data = _unitController.UnitData;

        _animator = _unitController.GetComponent<Animator>();
        _render = _unitController.GetComponent<SpriteRenderer>();

        _hashAttackFront = Animator.StringToHash("Attack_Front");
        _hashAttackBack = Animator.StringToHash("Attack_Back");
        _hashAttackRight = Animator.StringToHash("Attack_Right");
    }


    public void OnEnter()
    {
        Debug.Log("Attack���� ����");        

        _curDamageRate = 0;        
    }

    public void OnUpdate()
    {
        Debug.Log("������!");

        if (_data.HP <= 0 || (GameSceneManager.Instance.IsFinish == true && _unitController.photonView.IsMine == true)) 
        {
            //_unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
            _unitController.photonView.RPC("ChangeState", RpcTarget.All, (int)EStates.Dead);
        }
        if (_data.Path != null && _data.Path.Count == _data.PathIndex && _data.HitObject == null)
        {
            //_unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
            _unitController.photonView.RPC("ChangeState", RpcTarget.All, (int)EStates.Idle);
        }
        if ((_data.HasReceivedMove == true || _data.HitObject == null) && _data.Path.Count > 0 && _data.PathIndex != _data.Path.Count)
        {
            //_unitController.ChangeState(_unitController.States[(int)EStates.Walk]);
            _unitController.photonView.RPC("ChangeState", RpcTarget.All, (int)EStates.Walk);
        }

        DoAttack();

    }

    public void OnExit()
    {
        Debug.Log("Attack���� Ż��");
        StopAni();
    }

    
    /// <summary>
    /// �׻�, ������ �ܺ� �׵θ��� ����� �ܺ� �׵θ��� �浹�ϴ��� �˻��մϴ�.
    /// ������ �ٱ� �׵θ� �ȿ� ��밡 ���� ���, ���� �׵θ� ���� ������ �����մϴ�.
    /// </summary>
    public void DoAttack()
    {
        _curDamageRate += Time.deltaTime;

        if(_data.HitObject == null)
        {
            return;
        }

        if (_curDamageRate > _data.DamageRate)
        {
            IDamageable damageable = _data.HitObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.GetDamage(_data.Power);
            }
            _curDamageRate = 0;
            _unitController.Audio.PlayOneShot(_data.AudioCLips[(int)ESound.Attack]);
        }

        // ��� unit���� �ٶ󺸴� ���� ����Ͽ� �ִϸ��̼� ����.
        _attackDir = _data.HitObject.transform.position - _unitController.transform.position;
        PlayAttackAnimation();

    }

    //FIX ME: Walk �ִϸ��̼��� ���⿡ ���� ��,�츸 ��� (24.11/15 16:30)
    private void PlayAttackAnimation()
    {

        if (_attackDir.normalized.x >= 0)
        {
            //������ ���� �ִϸ��̼� �۵�
            _render.flipX = false;
            _animator.Play(_hashAttackRight);
        }
        else if (_attackDir.normalized.x < 0)
        {
            //���� ���� �ִϸ��̼� �۵�
            _render.flipX = true;
            _animator.Play(_hashAttackRight);
        }
    }
    private void StopAni()
    {
        _animator.StopPlayback();
    }

}
