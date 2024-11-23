using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum EOrder {Move, Attack };
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AStar _aStar;

    private List<UnitData> _units;

    private const int _linePosCount = 5;

    private Vector2 _lineStartPoint;

    private Vector2 _lineEndPoint;

    private Vector2 _movePoint;

    [SerializeField]private GameObject _target;

    private Vector2 _recentTargetPos;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private float _lineZPos;

    [SerializeField] private float _lineWidth;

    [SerializeField] private float _findAttackPathTime;


    private Vector2Int[] _endPosDir =
    {
        new Vector2Int( 0, +2), // ��
        new Vector2Int( 0, -2), // ��
        new Vector2Int(-2,  0), // ��
        new Vector2Int(+2,  0), // ��
        new Vector2Int(+2, +2), // ���
        new Vector2Int(+2, -2), // ����
        new Vector2Int(-2, +2), // �»�
        new Vector2Int(-2, -2), // ����
    };


    void Start()
    {
        _units = new List<UnitData>();
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;

        _lineRenderer.startColor = Color.green;
        _lineRenderer.endColor = Color.green;
    }

    void Update()
    {
        SelectUnits();
        CheckCommand();     
    }

    // Unit�����ϱ�
    private void SelectUnits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //���� ���� ���콺 ��ư���� �� �ʱ�ȭ.
            _units.Clear();
            _lineRenderer.positionCount = _linePosCount;
            _lineStartPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }

        if (Input.GetMouseButton(0))
        {
            _lineEndPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            DrawRectangle();
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckUnits();
            //�ʱ�ȭ
            _lineRenderer.positionCount = 0;
            _lineStartPoint = Vector3.zero;
            _lineEndPoint = Vector3.zero;
        }
    }


    // �簢�� ���� �׸���
    private void DrawRectangle()
    {
        //������, ��������, �����ʾƷ�, �Ʒ� �𼭸�.
        Vector3 vertex1 = new Vector3(_lineStartPoint.x, _lineStartPoint.y, _lineZPos);
        Vector3 vertex2 = new Vector3(_lineEndPoint.x, _lineStartPoint.y, _lineZPos);
        Vector3 vertex3 = new Vector3(_lineEndPoint.x, _lineEndPoint.y, _lineZPos);
        Vector3 vertex4 = new Vector3(_lineStartPoint.x, _lineEndPoint.y, _lineZPos);

        _lineRenderer.SetPosition(0, vertex1);
        _lineRenderer.SetPosition(1, vertex2);
        _lineRenderer.SetPosition(2, vertex3);
        _lineRenderer.SetPosition(3, vertex4);
        _lineRenderer.SetPosition(4, vertex1);
    }

    // �簢���ȿ� Unit�� �����ϴ���.
    private void CheckUnits()
    {
        Vector2 centerPos = new Vector2((_lineStartPoint.x + _lineEndPoint.x) / 2, (_lineStartPoint.y + _lineEndPoint.y) / 2);
        float width = Mathf.Abs(_lineStartPoint.x - _lineEndPoint.x);
        float height = Mathf.Abs(_lineStartPoint.y - _lineEndPoint.y);
        Vector2 size = new Vector3(width, height);

        Collider2D[] coliders = Physics2D.OverlapBoxAll(centerPos, size, 0);

        foreach(Collider2D hitCollider in coliders)
        {
            UnitData unit = hitCollider.GetComponent<UnitData>();
            if(unit != null)
            {
                _units.Add(unit);
            }
        }

    }

    // �������� �̵��������� üũ.
    private void CheckCommand()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _movePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            Collider2D target = Physics2D.OverlapCircle(_movePoint, 0.4f);
            // ������ ����� �ֵ���
            if (target != null)
            {
                // ��ֹ��� ���� ������� ��� x.
                if (target.tag != "Obstacle")
                {
                    _target = target.gameObject;
                    CommandUnits(_target.transform.position.x, _target.transform.position.y, (int)EOrder.Attack);
                }
            }
            else
            {
                //������ ����� ���� ���̶�� 
                _target = null;
                CommandUnits(_movePoint.x, _movePoint.y, (int)EOrder.Move);
            }
        }
    }
 
    // ������ ���� ���� �۵�.
    private void CommandUnits(float movePosX, float movePosY, int orderNum)
    {
        Debug.Log("Move!");
        if(_movePoint == Vector2.zero)
        {
            return;
        }

        int xPos = 0;
        int yPos = 0;

        int dirIndex = 0;

        for(int i = 0; i < _units.Count; i++)
        {
            UnitData unit = _units[i];
            // ����order�� ��� Ÿ�� ����
            if(orderNum == (int)EOrder.Attack)
            {
                unit.AttackTarget = _target;
                unit.HasReceivedMove = false;
            }
            else if(orderNum == (int)EOrder.Move)
            {
                unit.AttackTarget = null;
                unit.HasReceivedMove = true;
            }

            Vector2Int startPos = new Vector2Int((int)_units[i].transform.position.x, (int)_units[i].transform.position.y);
            Vector2Int endPos = new Vector2Int((int)movePosX + xPos, (int)movePosY + yPos);

            if(_aStar.DoAStar(startPos, endPos) == true)
            {
                unit.Path.Clear();

                foreach (Vector2Int path in _aStar.Path)
                {
                    unit.PathIndex = 0;
                    unit.Path.Add(path);
                }
            }

            // ��ü�� �̵� ���� �� �Ѱ��� ���� �ʵ��� ����.
            if (orderNum == (int)EOrder.Move)
            {
                if (dirIndex == _endPosDir.Length - 1)
                {
                    dirIndex = 0;
                }
                else
                {
                    dirIndex++;
                }

                xPos = _endPosDir[dirIndex].x;
                yPos = _endPosDir[dirIndex].y;
            }
        }

    }

}
