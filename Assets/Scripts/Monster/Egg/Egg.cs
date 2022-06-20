using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable Unity.InefficientPropertyAccess
public class Egg : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint1;
    public GameObject shootPoint2;
    public GameObject hpBar;
    public GameObject[] attacks;

    public float speed;
    public float foundRange;
    public float hp;
    public float attackRange;
    public float jumpPower;

    private int[] _attackType;

    private int _i;
    private int _attackCount;
    private int _ifSmoking;

    public bool isGround;
    private bool _isMov;
    private bool _isAttack;
    private bool _isFound;

    private float _gravity;
    private float _bSpeed;
    private float _foundTime;
    private float _walkTime;
    private float _direction;
    private float _mxHp;

    public Animator animator;
    private Player _player;
    private Image _hpGauge;

    private RaycastHit2D _hitInfo;
    private static readonly int AnimAttack1R = Animator.StringToHash("attack1R");
    private static readonly int AnimAttack1 = Animator.StringToHash("attack1");
    private static readonly int AnimAttack2R = Animator.StringToHash("attack2R");
    private static readonly int AnimAttack2 = Animator.StringToHash("attack2");
    private static readonly int AnimAttack3 = Animator.StringToHash("attack3");
    private static readonly int AnimIsSit = Animator.StringToHash("isSit");
    private static readonly int AnimIsSmoking = Animator.StringToHash("isSmoking");
    private static readonly int AnimIsWalk = Animator.StringToHash("isWalk");
    private static readonly int AnimIsDie = Animator.StringToHash("isDie");

    private void Start()
    {
        hpBar.SetActive(false);
        _mxHp = hp;
        _attackType = new[] { 1, 2, 1, 1, 2, 1, 2, 1, 1, 1, 2, 2, 1, 1, 1, 2 };
        _i = 1;
        isGround = false;
        _bSpeed = speed;
        _isMov = true;
        _player = FindObjectOfType<Player>();
        _hpGauge = hpBar.GetComponent<Image>();
        _isAttack = true;
    }


    
    private void Update()
    {
        /*
        var bPos = (Vector2)transform.position;
        var aPos = new Vector2(speed * _i, 0) * Time.deltaTime;
        var gPos = new Vector2(0, gravity) * Time.deltaTime;
        */
        _walkTime += Time.deltaTime;
        _foundTime += Time.deltaTime;
        var bPos = (Vector2)transform.position;
        var aPos = new Vector2(speed * _i, 0) * Time.deltaTime;
        var gPos = new Vector2(0, _gravity) * Time.deltaTime;
        transform.position = bPos + gPos;

        if (!_isFound)
        {
            if (_walkTime>2)
            {
                _isMov = false;
                Debug.Log(_i);
                animator.SetBool(AnimIsWalk, false);
                if (_i==-1)
                {
                    _i = 1;
                    
                }
                else if(_i==1)
                {
                    _i = -1;
                }
                _foundTime = 0;
            }
            if (_foundTime > 3)
            {
                animator.SetBool(AnimIsWalk, true);
                _isMov = true;
                _walkTime = 0;
            }
        }
        /*_foundTime += Time.deltaTime;
        var dist = Vector2.Distance(transform.position, _player.transform.position);
        _direction = _player.transform.position.x - transform.position.x;
        var bPos = (Vector2)transform.position;
        var aPos = new Vector2(speed * _i, 0) * Time.deltaTime;
        var gPos = new Vector2(0, _gravity) * Time.deltaTime;
        _hpGauge.fillAmount = hp / _mxHp;
        transform.position = bPos + gPos;
        if (hp <= 0)
        {
            animator.SetBool(AnimIsDie, true);
        }
        
        
        if (_foundTime >= 3)
        {
            if (!_isFound)
            {
                hpBar.SetActive(false);
                transform.rotation = Quaternion.Euler(0, _i == -1 ? 0 : 180, 0);

                transform.position = bPos + aPos;
                animator.SetBool(AnimIsWalk, true);
                _walkTime += Time.deltaTime;
                if (_walkTime >= 2)
                {
                    _i = _i * -1;
                    animator.SetBool(AnimIsWalk, false);
                    _walkTime = 0;
                    _foundTime = 0;
                }
            }
        }

        //인식했을떄
        if (dist <= foundRange)
        {
            _isFound = true;
        }
        else if (_direction >= 10)
        {
            _isFound = false;
            _isMov = true;
        }

        if (_isFound)
        {
            hpBar.SetActive(true);
            animator.SetBool(AnimIsWalk, false);
            _foundTime = 0;
            if (_isAttack)
            {
                switch (_direction)
                {
                    case > 0:
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        if (dist > attackRange)
                        {
                            _i = 1;
                            transform.position = bPos + aPos + gPos;
                            animator.SetBool(AnimIsWalk, true);
                        }
                            Debug.Log(_direction);
                        break;
                    }
                    case < 0:
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        if (dist > attackRange)
                        {
                            _i = -1;
                            transform.position = bPos + aPos + gPos;
                            animator.SetBool(AnimIsWalk, true);
                        }

                        break;
                    }
                }

                if (dist <= attackRange)
                {
                    switch (_attackType[_attackCount])
                    {
                        case 1:
                            animator.SetBool(AnimAttack1R, true);
                            _isAttack = false;
                            break;
                        case 2:
                            animator.SetBool(AnimAttack2R, true);
                            _isAttack = false;
                            break;
                    }
                }

                Debug.Log(_attackType[_attackCount]);
            }
        }
        */
        //중력
        if (isGround == false)
        {
            _gravity = -3;
        }
        else
        {
            _gravity = 0;
        }
        
        //이동통제
        if (_isMov == false)
        {
            speed = 0;
        }
        else
        {
            speed = _bSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ground"))
        {
            isGround = true;
        }

        if (other.CompareTag("PlayerAttack"))
        {
            Damage damage = other.gameObject.GetComponent<Damage>();
            hp -= damage.dmg;
            Debug.Log("맞음");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            isGround = false;
        }
    }


    public void A1()
    {
        animator.SetBool(AnimAttack1R, false);
        animator.SetBool(AnimAttack1, true);
    }

    public void A1_1()
    {
        Instantiate(attacks[0], transform.position, transform.rotation);
    }

    public void A1_2()
    {
        Instantiate(attacks[1], transform.position, transform.rotation);
    }

    public void A1_3()
    {
        Instantiate(attacks[2], transform.position, transform.rotation);
    }

    public void A1_4()
    {
        //attack1_3.SetActive(false);
    }

    public void A1End()
    {
        animator.SetBool(AnimAttack1, false);
        if (_attackCount <= 14)
        {
            _attackCount += 1;
        }
        else
        {
            _attackCount = 0;
        }

        _isAttack = true;
    }

    public void A2()
    {
        animator.SetBool(AnimAttack2R, false);
        animator.SetBool(AnimAttack2, true);
    }

    public void A2_1()
    {
        Instantiate(attacks[3], transform.position, transform.rotation);
    }

    public void A2_2()
    {
        Instantiate(attacks[4], transform.position, transform.rotation);
    }

    public void A2_3()
    {
        Instantiate(attacks[5], transform.position, transform.rotation);
    }

    public void A2_4()
    {
        Instantiate(attacks[6], transform.position, transform.rotation);
    }

    public void A2_5()
    {
        Instantiate(attacks[7], transform.position, transform.rotation);
    }

    public void A2_6()
    {
        Instantiate(attacks[8], transform.position, transform.rotation);
    }

    public void A2_7()
    {
        Instantiate(attacks[9], transform.position, transform.rotation);
    }

    public void A2_8()
    {
        Instantiate(attacks[10], transform.position, transform.rotation);
    }

    public void A2_9()
    {
        Instantiate(attacks[11], transform.position, transform.rotation);
    }

    public void A3()
    {
        animator.SetBool(AnimAttack2, false);
        animator.SetBool(AnimAttack3, true);
    }

    public void A3_1()
    {
        Instantiate(attacks[12], transform.position, transform.rotation);
    }

    public void A3_2()
    {
        Instantiate(attacks[13], transform.position, transform.rotation);
    }

    public void A3_3()
    {
        Instantiate(attacks[14], transform.position, transform.rotation);
    }

    public void A3End()
    {
        animator.SetBool(AnimAttack3, false);
        _ifSmoking = Random.Range(0, 2);
        if (_ifSmoking == 0)
        {
            _isAttack = true;
        }
        else if (_ifSmoking == 1)
        {
            animator.SetBool(AnimIsSit, true);
        }

        if (_attackCount <= 14)
        {
            _attackCount += 1;
        }
    }

    public void A4()
    {
        animator.SetBool(AnimIsSit, false);
        animator.SetBool(AnimIsSmoking, true);
    }

    public void A4End()
    {
        animator.SetBool(AnimIsSmoking, false);
        _isAttack = true;
        for (var i = 0; i < 3; i++)
        {
            switch (_direction)
            {
                case > 0:
                    Instantiate(prefab, shootPoint2.transform.position, Quaternion.identity);
                    break;
                case < 0:
                    Instantiate(prefab, shootPoint1.transform.position, Quaternion.identity);
                    break;
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}