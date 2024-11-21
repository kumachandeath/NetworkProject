using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    private UnitController _unitController;

    private Collider2D _detectEnemy;    

    

    public AttackState(UnitController controller)
    {
        //������
        _unitController = controller;
    }


    public void OnEnter()
    {
        Debug.Log("Attack���� ����");
    }

    public void OnUpdate()
    {
        if (_unitController.UnitData.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (_unitController.Detect == null &&
            _unitController.UnitData.Path.Count == _unitController.UnitData.PathIndex)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
        }
        if (_unitController.Detect == null &&
            _unitController.UnitData.Path.Count > 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Walk]);
        }

        OverlapCircle();

    }

    public void OnExit()
    {
        Debug.Log("Attack���� Ż��");
    }

    /// <summary>
    /// �׻�, ������ �ܺ� �׵θ��� ����� �ܺ� �׵θ��� �浹�ϴ��� �˻��մϴ�.
    /// </summary>
    public void OverlapCircle()
    {        
        _detectEnemy = Physics2D.OverlapCircle(this.transform.position, _unitController.InRadius);

        if (_detectEnemy != null)
        {
            DoAttack();
        }
        
    }

    /// <summary>
    /// ������ �ٱ� �׵θ� �ȿ� ��밡 ���� ���, ���� �׵θ� ���� ������ �����մϴ�.
    /// </summary>
    public void DoAttack()
    {
        float damageRate = 0;
        damageRate += Time.deltaTime;

        if (damageRate > _unitController.UnitData.DamageRate)
        {
            _unitController.GetDamage();
            damageRate = 0f;
        }
    }

}
