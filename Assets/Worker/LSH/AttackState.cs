using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    private UnitController _unitController;

    private UnitData _data;

    private Collider2D _detectEnemy;

    private IDamageable _damageAble;

    private float _curDamageRate;

    public AttackState(UnitController controller)
    {
        //������
        _unitController = controller;
        _data = _unitController.UnitData;
    }


    public void OnEnter()
    {
        Debug.Log("Attack���� ����");
        _curDamageRate = 0;
    }

    public void OnUpdate()
    {
        Debug.Log("������!");

        if (_data.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }
        if (_data.Path.Count == _data.PathIndex && _data.HitObject == null)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
        }
        if ((_data.HasReceivedMove == true || _data.HitObject == null) && _data.Path.Count > 0 && _data.PathIndex != _data.Path.Count)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Walk]);
        }

        DoAttack();

    }

    public void OnExit()
    {
        Debug.Log("Attack���� Ż��");
    }

    
    /// <summary>
    /// �׻�, ������ �ܺ� �׵θ��� ����� �ܺ� �׵θ��� �浹�ϴ��� �˻��մϴ�.
    /// ������ �ٱ� �׵θ� �ȿ� ��밡 ���� ���, ���� �׵θ� ���� ������ �����մϴ�.
    /// </summary>
    public void DoAttack()
    {
        _curDamageRate += Time.deltaTime;

        if(_curDamageRate > _data.DamageRate)
        {
            IDamageable damageable = _data.HitObject.GetComponent<IDamageable>();
            damageable.GetDamage(_data.Power);
            _curDamageRate = 0;
        }
    }



}
