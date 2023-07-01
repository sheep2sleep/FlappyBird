using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public float force;

    private Vector3 initPosion;
    private bool death = false;

    //定义无参无返回值的委托
    public delegate void DeathNotify();
    //声明一个委托
    public event DeathNotify OnDeath;

    //使用事件传递积分消息
    public UnityAction<int> OnScore;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
        initPosion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (death) return;
        if (Input.GetMouseButtonDown(0))
        {
            rigidbodyBird.velocity = Vector2.zero;
            rigidbodyBird.AddForce(new Vector2(0, force),ForceMode2D.Force);
        }
        else if(Input.touchCount>0)
        {
            Touch myTouch = Input.GetTouch(0);
            if(Input.touchCount == 1 && myTouch.phase == TouchPhase.Began)
            {
                rigidbodyBird.velocity = Vector2.zero;
                rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
            }
        }
    }

    /// <summary>
    /// 切换状态为待机
    /// </summary>
    public void Idle()
    {
        rigidbodyBird.simulated = false;
        ani.SetTrigger("Idle");
    }
    
    /// <summary>
    /// 切换状态为飞行
    /// </summary>
    public void Fly()
    {
        rigidbodyBird.simulated = true;
        ani.SetTrigger("Fly");
    }

    /// <summary>
    /// 切换状态为死亡
    /// </summary>
    public void Die()
    {
        if(death == false)
        {
            death = true;
            if (OnDeath != null)//当委托已绑定时
            {
                OnDeath();//触发委托
            }
        }
    }

    /// <summary>
    /// 碰撞到地面碰撞器
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    /// <summary>
    /// 碰撞到管道触发器
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("ScoreArea"))
        {
            if (OnScore != null)
            {
                OnScore(1);
            }
        }
        else
        {
            Die();
        }
    }

    /// <summary>
    /// 重新开始游戏时初始化小鸟状态
    /// </summary>
    public void Restart()
    {
        transform.position = initPosion;
        death = false;
        Idle();
    }
 
}
