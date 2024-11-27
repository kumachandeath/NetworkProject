using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    private Vector3 _rightTop;                      // ī�޶� ����Ʈ ����

    private Vector3 _leftTop;                       // ī�޶� ����Ʈ �»��

    private Vector3 _rightBottom;                   // ī�޶� ����Ʈ ���ϴ�

    private Vector3 _leftBottom;                    // ī�޶� ����Ʈ ���ϴ�

    [SerializeField] private GameObject _test;      // �׽�Ʈ�� ������Ʈ


    private void Start()
    {
        _rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        _leftTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        _rightBottom = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        _leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        _test.transform.localScale = new Vector3(Vector3.Distance(_rightTop, _leftTop), Vector3.Distance(_rightTop, _rightBottom), 0);
    }
    private void Update()
    {
        ChaseMainCam();
    }

    private void ChaseMainCam()
    {
        _rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        _leftTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        _rightBottom = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        _leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //Debug.DrawLine(_rightTop, _leftTop, Color.red);
        //Debug.DrawLine(_leftTop, _leftBottom, Color.red);
        //Debug.DrawLine(_leftBottom, _rightBottom, Color.red);
        //Debug.DrawLine(_rightBottom, _rightTop, Color.red);

        _test.transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
    }
}
