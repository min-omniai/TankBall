using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PingpongBlock : Block
{
    public enum MoveDirection
    {
        X,
        Y,
        None
    }

    [FormerlySerializedAs("_pingpongType")]
    public Define.PingpongType Kind = Define.PingpongType.None;
    [FormerlySerializedAs("_moveDirectionType")]
    public MoveDirection Direction = MoveDirection.None;
    [FormerlySerializedAs("_value")]
    public int Value = 1;
    [FormerlySerializedAs("_isMoving")]
    public bool IsMoving = false;
    private GameObject _glow = null;

    private bool _plusDir = true;
    private float _startPos = 0f;
    private float _endPos = 0f;
    private float _speed = 3f;
    private Rigidbody2D _rigidbody2D = null;


    public override void Init()
    {
        _glow = transform.GetChild(0).gameObject;
        _glow.SetActive(false);

        switch (Kind)
        {
            case Define.PingpongType.Plus:
                GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("PlusBlock");
                break;

            case Define.PingpongType.Multiply:
                GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("MultiplyBlock");
                break;
        }

        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.simulated = true;
            _rigidbody2D.mass = 1f;
            _rigidbody2D.drag = 0f;
            _rigidbody2D.angularDrag = 0f;
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (IsMoving)
        {
            switch (Direction)
            {
                case MoveDirection.X:
                    if (transform.position.x < 0)
                        _plusDir = true;
                    else
                        _plusDir = false;

                    _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                    _startPos = transform.position.x;
                    _endPos = -transform.position.x;

                    _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    break;

                case MoveDirection.Y:
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Managers.Game.GameStatePlay)
        {
            if (IsMoving)
            {
                switch (Direction)
                {
                    case MoveDirection.X:
                        if (_plusDir)
                        {
                            if (transform.position.x < _endPos)
                                _rigidbody2D.velocity = Vector2.right * _speed;
                            else
                                _plusDir = false;
                        }
                        else
                        {
                            if (transform.position.x > _startPos)
                                _rigidbody2D.velocity = Vector2.left * _speed;
                            else
                                _plusDir = true;
                        }
                        break;

                    case MoveDirection.Y:
                        break;
                }
            }
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }


    public void Reset()
    {
        switch (Direction)
        {
            case MoveDirection.X:
                transform.position = new Vector3(_startPos, transform.position.y, 0);
                break;

            case MoveDirection.Y:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            _glow.SetActive(true);
            _glow?.GetComponent<PingpongBlockGlow>().Timer(.1f);

            Ball ball = other.transform.GetComponent<Ball>();

            switch (Kind)
            {
                case Define.PingpongType.Plus:
                    ball.IncreaseNumber(ball.Number + Value);
                    break;

                case Define.PingpongType.Multiply:
                    ball.IncreaseNumber(ball.Number * Value);
                    break;
            }
        }
    }
}
