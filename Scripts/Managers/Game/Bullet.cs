using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Define.ColorType _colorType = Define.ColorType.Red;
    private float _speed = 300f;
    private Vector3 _lastVelocity = Vector3.zero;
    private Vector2 _dir = Vector2.zero;

    private SpriteRenderer _spriteRenderer = null;
    private CircleCollider2D _circleCollider2D = null;
    private Rigidbody2D _rigidbody2D = null;


    public void Init(Define.ColorType colorType)
    {
        _colorType = colorType;

        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_circleCollider2D == null)
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _circleCollider2D.isTrigger = false;
            _circleCollider2D.usedByEffector = false;
            _circleCollider2D.offset = Vector2.zero;
            _circleCollider2D.radius = .25f;
        }

        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.simulated = true;
            _rigidbody2D.mass = 1f;
            _rigidbody2D.drag = 0f;
            _rigidbody2D.angularDrag = 0f;
            _rigidbody2D.gravityScale = 0f;
        }

        switch (_colorType)
        {
            case Define.ColorType.Red:
                _dir = transform.right;
                _spriteRenderer.sprite = Managers.Resource.Load<Sprite>("RedBullet");
                break;

            case Define.ColorType.Blue:
                _dir = -transform.right;
                _spriteRenderer.sprite = Managers.Resource.Load<Sprite>("BlueBullet");
                break;
        }

        gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (Managers.Game.GameStatePlay)
        {
            switch (_colorType)
            {
                case Define.ColorType.Red:
                    _rigidbody2D.velocity = _dir * _speed * Time.fixedDeltaTime;
                    break;

                case Define.ColorType.Blue:
                    _rigidbody2D.velocity = _dir * _speed * Time.fixedDeltaTime;
                    break;
            }

            _lastVelocity = _rigidbody2D.velocity;
        }
        else
        {
            GetComponent<Poolable>().Destroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            _dir = Vector2.Reflect(_lastVelocity.normalized, other.contacts[0].normal);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            bool isTrue = other.GetComponent<Grid>().CheckOccupation(_colorType);

            if (isTrue)
            {
                Managers.Game.CurrentStage.SetGridCount(_colorType);

                GetComponent<Poolable>().Destroy();
            }
        }
    }
}
