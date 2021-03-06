using UnityEngine;

public class Smoking : MonoBehaviour
{
    public float speed;
    int _i;
    float _a;
    float _direction;
    bool _isMove;

    Player _player;
    Animator _animator;
    Rigidbody2D _rigid;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.gravityScale = 0;
        speed = Random.Range(1, 15);
    }

    private void Update()
    {
        _direction = _player.transform.position.x - transform.position.x;
        //_i = direction switch
        //{
        //    > 0 => 1,
        //    < 0 => -1,
        //    _ => _i
        //};

        var aPos = new Vector2(speed * _i, 0) * Time.deltaTime;
        var bPos = (Vector2) transform.position;
        if (_isMove)
        {
            transform.position = bPos + aPos;
        }
    }

    public void Move()
    {
        Debug.Log("무브");
        _animator.SetBool("isMove", true);
        _isMove = true;
        Jump();
        _rigid.gravityScale = 0.5f;
    }

    public void End()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Jump()
    {
        _i = _direction switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => _i
        };
        _a = Random.Range(2, 10);
        _rigid.AddForce(Vector2.up * _a, ForceMode2D.Impulse);
    }
}