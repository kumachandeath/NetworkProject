using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    private Vector2 _rightTop;                      // ī�޶� ����Ʈ ����

    private Vector2 _leftTop;                       // ī�޶� ����Ʈ �»��

    private Vector2 _rightBottom;                   // ī�޶� ����Ʈ ���ϴ�

    private Vector2 _leftBottom;                    // ī�޶� ����Ʈ ���ϴ�

    [SerializeField] private Image _angle;          // �̴ϸʿ� ǥ�õ� ī�޶� �ޱ�

    [SerializeField] private GameObject _test;

    private void Start()
    {
        //_angle.transform.localScale = Camera.main.scaledPixelHeight;
    }

    private void Update()
    {
        _rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1,0));
        _leftTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        _rightBottom = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        _leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //Debug.DrawLine(_rightTop, _leftTop, Color.red);
        //Debug.DrawLine(_leftTop, _leftBottom, Color.red);
        //Debug.DrawLine(_leftBottom, _rightBottom, Color.red);
        //Debug.DrawLine(_rightBottom, _rightTop, Color.red);
        
        _test.transform.position = Camera.main.transform.position + new Vector3(0,0,10);
        
        
        //_angle.rectTransform.position = Camera.main.WorldToViewportPoint();
        //_angle.rectTransform.localScale = new Vector3(0.1f,0.1f,0);
    }
}
