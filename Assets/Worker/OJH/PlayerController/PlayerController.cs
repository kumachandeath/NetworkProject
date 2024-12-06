using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EOrder { Move, Attack };
public class PlayerController : MonoBehaviourPun, IDamageable
{
    [SerializeField] private PlayerData _playerData;

    private const int _linePosCount = 5;

    private Vector2 _lineStartPoint;

    private Vector2 _lineEndPoint;

    private Vector2 _movePoint;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private float _lineZPos;

    [SerializeField] private float _lineWidth;

    private float _time;                                              // Unit�� ���� �ֱ� üũ.

    [SerializeField] private Slider _hpSlider;

    private bool _isfinish;

    [SerializeField] private GameObject _winImage;

    [SerializeField] private GameObject _loseImage;

    [SerializeField] private AudioSource _audio;

    [SerializeField] private AudioClip[] _audioClips;

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

    // Unit ��ȯ Position ��ġ
    private int _unitPosIndex = 0;

    private void OnEnable()
    {
        _playerData.OnHpChanged += UpdateHp;
    }

    private void OnDisable()
    {
        _playerData.OnHpChanged -= UpdateHp;
    }

    void Start()
    {
        _playerData.Units = new List<UnitData>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = _lineWidth;
        _lineRenderer.endWidth = _lineWidth;
        _lineRenderer.startColor = Color.green;
        _lineRenderer.endColor = Color.green;
    }

    void Update()
    {
        // ������ �� ����ִ� ���¿����� 
        if (photonView.IsMine && GameSceneManager.Instance.IsFinish == false)
        {
            GameEnd();
            SelectUnits();
            CheckCommand();
            CreateUnit();
        }
    }

    private void GameEnd()
    {

        if (GameSceneManager.Instance.CurPlayerCount == 1 && GameSceneManager.Instance.IsFinish == false)
        {
            Debug.Log("win!!");
            _winImage.SetActive(true);
            GameSceneManager.Instance.IsFinish = true;
        }
        else if (_playerData.HP <= 0 && GameSceneManager.Instance.IsFinish == false)
        {
            Debug.Log("Lose!!");
            _loseImage.SetActive(true);
            GameSceneManager.Instance.IsFinish = true;
            GameSceneManager.Instance.CurPlayerCount--;
            int playerCount = GameSceneManager.Instance.CurPlayerCount;
            photonView.RPC("UpdatePlayerCount", RpcTarget.Others, playerCount);

        }

    }

    public void GameExit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }


    [PunRPC]
    private void UpdatePlayerCount(int updateNum)
    {
        GameSceneManager.Instance.CurPlayerCount = updateNum;
    }
    

    // Unit�����ϱ�
    private void SelectUnits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //���� ���� ���콺 ��ư���� �� �ʱ�ȭ.
            _playerData.Units.Clear();
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

    // Unit ���ý� ���Sound play.
    private void PlaySelectSound()
    {
        EUnit unitType = _playerData.UnitType;
        switch (unitType)
        {
            case EUnit.Zealot:
                _audio.PlayOneShot(_audioClips[(int)EUnit.Zealot]);
                break;
            case EUnit.DarkTemplar:
                _audio.PlayOneShot(_audioClips[(int)EUnit.DarkTemplar]);
                break;
            case EUnit.Juggling:
                _audio.PlayOneShot(_audioClips[(int)EUnit.Juggling]);
                break;
            case EUnit.Ultralisk:
                _audio.PlayOneShot(_audioClips[(int)EUnit.Ultralisk]);
                break;
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
        bool isExist = false;

        Vector2 centerPos = new Vector2((_lineStartPoint.x + _lineEndPoint.x) / 2, (_lineStartPoint.y + _lineEndPoint.y) / 2);
        float width = Mathf.Abs(_lineStartPoint.x - _lineEndPoint.x);
        float height = Mathf.Abs(_lineStartPoint.y - _lineEndPoint.y);
        Vector2 size = new Vector3(width, height);

        Collider2D[] coliders = Physics2D.OverlapBoxAll(centerPos, size, 0);

        foreach (Collider2D hitCollider in coliders)
        {
            UnitData unit = hitCollider.GetComponent<UnitData>();
            // �� ���� Unit�� �ǵ� �� �ֵ��� ��.
            if (unit != null && unit.UnitType == _playerData.UnitType)
            {
                isExist = true;
                _playerData.Units.Add(unit);
            }
        }

        // Unit�� 1������ �����ϸ� ���SoundPlay
        if(isExist == true)
        {
            PlaySelectSound();
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
                UnitData _unit = target.GetComponent<UnitData>();
                PlayerData _player = target.GetComponent<PlayerData>();
                // ��ֹ��� ���� ������� ��� ���ϰ� Unit�̾�� �ϸ�, �� Unit�� �Ʊ������� �ƴ� �ٸ� �����̾����, Player ���� ������ ���ݴ������ x.
                if (target.tag != "Obstacle" && ((_unit != null && _unit.UnitType != _playerData.UnitType) || (_player != null && _player.UnitType != _playerData.UnitType)))
                {
                    _playerData.Target = target.gameObject;
                    CommandUnits(_playerData.Target.transform.position.x, _playerData.Target.transform.position.y, (int)EOrder.Attack);
                }
            }
            else
            {
                //������ ����� ���� ���̶�� 
                _playerData.Target = null;
                CommandUnits(_movePoint.x, _movePoint.y, (int)EOrder.Move);
            }
        }
    }

    // ������ ���� ���� �۵�.
    private void CommandUnits(float movePosX, float movePosY, int orderNum)
    {
        Debug.Log("Move!");
        if (_movePoint == Vector2.zero)
        {
            return;
        }

        int xPos = 0;
        int yPos = 0;

        int dirIndex = 0;
        int spawnIndex = 0;

        for (int i = 0; i < _playerData.Units.Count; i++)
        {
            UnitData unit = _playerData.Units[i];
            // ����order�� ��� Ÿ�� ����
            if (orderNum == (int)EOrder.Attack)
            {
                unit.AttackTarget = _playerData.Target;
                unit.HasReceivedMove = false;
            }
            else if (orderNum == (int)EOrder.Move)
            {
                unit.AttackTarget = null;
                unit.HasReceivedMove = true;
            }

            Vector2Int startPos = new Vector2Int((int)_playerData.Units[i].transform.position.x, (int)_playerData.Units[i].transform.position.y);
            Vector2Int endPos = new Vector2Int((int)movePosX + xPos, (int)movePosY + yPos);

            if (_playerData.Astar.DoAStar(startPos, endPos) == true)
            {
                unit.Path.Clear();
                foreach (Vector2Int path in _playerData.Astar.Path)
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
                    spawnIndex++;
                }
                else
                {
                    dirIndex++;
                }

                xPos = _endPosDir[dirIndex].x + spawnIndex;
                yPos = _endPosDir[dirIndex].y + spawnIndex;
            }
        }

    }

    private void CreateUnit()
    {
        if(ObjectPool.Instance != null)
        {
            _time += Time.deltaTime;

            if (_time >= _playerData.SpawnTime)
            {
                if(GameSceneManager.Instance.CurUnitCounts[(int)_playerData.UnitType] < GameSceneManager.Instance.UnitCounts[(int)_playerData.UnitType])
                {
                    if(_unitPosIndex == GameSceneManager.Instance.UnitCounts[(int)_playerData.UnitType])
                    {
                        _unitPosIndex = 0;
                    }
                    _playerData.UnitSpawner.Spawn((int)_playerData.UnitType, _unitPosIndex, _playerData.SpawnPos);
                    GameSceneManager.Instance.CurUnitCounts[(int)_playerData.UnitType]++;
                    _unitPosIndex++;
                }
                _time = 0;
            }
        }
    }

    public void GetDamage(int damage)
    {
        _playerData.HP -= damage;
    }

    public void UpdateHp()
    {
        _hpSlider.value = (float)_playerData.HP / (float)_playerData.MaxHp;
    }


}
