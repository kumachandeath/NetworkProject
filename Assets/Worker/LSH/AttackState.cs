using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    private UnitController _unitController;
    private UnitData _data;

    private Collider2D _detectEnemy;
    private IDamageable _damageAble;

    public AttackState(UnitController controller)
    {
        //������
        _unitController = controller;
        _data = _unitController.UnitData;
    }


    public void OnEnter()
    {
        Debug.Log("Attack���� ����");
    }

    public void OnUpdate()
    {
        if (_data.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (_data.DetectColider == null &&
            _data.Path.Count == _data.PathIndex)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
        }
        if (_data.DetectColider == null &&
            _data.Path.Count > 0 && _data.PathIndex != _data.Path.Count)
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
        float damageRate = 0;
        damageRate += Time.deltaTime;

        if (_unitController.AttackTarget != null)
        {
            _unitController.GetDamage();
            damageRate = 0f;
        }
        else
        {
            _unitController.GetDamage();
            damageRate = 0f;
        }

        
    }



}
