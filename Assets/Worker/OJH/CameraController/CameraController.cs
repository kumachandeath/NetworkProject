using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; // ī�޶� �̵� �ӵ�

    [SerializeField] private float _borderDistance; // ȭ�� �����ڸ����� �Ÿ� (�ȼ�)

    [SerializeField] private Vector2 _moveBoundaryMin; // ī�޶� �̵� ���� �ּҰ�

    [SerializeField] private Vector2 _moveBoundaryMax; // ī�޶� �̵� ���� �ִ밪

    [SerializeField] private Vector3 _targetPosition; // ī�޶� �̵��� ��ǥ ��ġ

    private bool _isSet;
    

    void Update()
    {
        if(GameSceneManager.Instance.IsSetCam)
        {
            SetCamera();
            MoveCamera();
        }
    }

    private void SetCamera()
    {
        if(_isSet == false)
        {
            _targetPosition = transform.position;
            _isSet = true;
        }
    }

    private void MoveCamera()
    {
        // ���콺 ��ġ�� ��ũ�� �������� ��������
        Vector3 mousePosition = Input.mousePosition;

        // ���콺�� ȭ�� ��踦 ������ �� �� ī�޶� �̵�
        if (mousePosition.x <= _borderDistance)
        {
            _targetPosition.x -= _moveSpeed * Time.deltaTime;
        }
        else if (mousePosition.x >= Screen.width - _borderDistance)
        {
            _targetPosition.x += _moveSpeed * Time.deltaTime;
        }

        if (mousePosition.y <= _borderDistance)
        {
            _targetPosition.y -= _moveSpeed * Time.deltaTime;
        }
        else if (mousePosition.y >= Screen.height - _borderDistance)
        {
            _targetPosition.y += _moveSpeed * Time.deltaTime;
        }
        //Debug.Log($"{ _targetPosition.x}, { _targetPosition.y}");
         //ī�޶� �̵� ���� ����
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, _moveBoundaryMin.x, _moveBoundaryMax.x);
        _targetPosition.y = Mathf.Clamp(_targetPosition.y, _moveBoundaryMin.y, _moveBoundaryMax.y);

        // ī�޶� �̵�
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.1f);
    }
}
