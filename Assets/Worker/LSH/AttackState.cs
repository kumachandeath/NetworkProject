using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{
    private UnitController _unitController;

    public AttackState(UnitController controller)
    {
        //������
        _unitController = controller;
    }


    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        if (_unitController.UnitData.HP <= 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Dead]);
        }

        if (true /*������ �ܺ� �׵θ��� ���*/ &&
            _unitController.UnitData.Path.Count == _unitController.UnitData.PathIndex)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Idle]);
        }
        if (true/*������ �ܺ� �׵θ��� ���*/ &&
            _unitController.UnitData.Path.Count > 0)
        {
            _unitController.ChangeState(_unitController.States[(int)EStates.Walk]);
        }

        OverlapCircle();

    }

    public void OnExit()
    {

    }

    /// <summary>
    /// �׻�, ������ �ܺ� �׵θ��� ����� �ܺ� �׵θ��� �浹�ϴ��� �˻��մϴ�.
    /// </summary>
    public void OverlapCircle()
    {

    }

    /// <summary>
    /// ������ �ٱ� �׵θ� �ȿ� ��밡 ���� ���, ���� �׵θ� ���� ������ �����մϴ�.
    /// </summary>
    public void DoAttack()
    {

    }

}
