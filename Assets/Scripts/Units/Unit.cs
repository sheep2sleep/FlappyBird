using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public GameObject bulletTemplate;
    public SIDE side;
    public bool desoryOnDeath = false;
    public int dieScore = 1;
    public float HP = 100f;
    public float MaxHP = 100f;
    public int life = 3;
    public float speed = 5f;
    public Transform firePoint;
    public float fireRate = 10f;
    public float Attack;

    protected Vector3 initPosion;
    protected bool death = false;
    protected bool isFlying = false;
    protected float fireTimer = 0f;

    //声明无参无返回值的委托类--死亡通知
    public delegate void DeathNotify(Unit sender);
    //声明一个委托--死亡
    public event DeathNotify OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
        initPosion = transform.position;
        Init();

        OnStart();
    }

    /// <summary>
    /// 虚函数OnStart，执行初始化操作
    /// </summary>
    public virtual void OnStart()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (death) return;
        if (!isFlying) return;

        //开火计时器每帧更新
        fireTimer += Time.deltaTime;

        OnUpdate();
    }

    /// <summary>
    /// 虚函数OnUpdate，执行每帧更新工作
    /// </summary>
    public virtual void OnUpdate()
    {
    }

    /// <summary>
    /// 初始化小鸟状态
    /// </summary>
    public void Init()
    {
        Idle();
        transform.position = initPosion;
        death = false;
        HP = MaxHP;
    }

    /// <summary>
    /// 小鸟开火
    /// </summary>
    public void Fire()
    {
        if (fireTimer > 1 / fireRate)
        {
            GameObject bullt = Instantiate(bulletTemplate);
            bullt.transform.position = firePoint.position;
            //设置子弹方向
            bullt.GetComponent<Element>().direction = side == SIDE.PLAYER ? Vector3.right : Vector3.left;
            fireTimer = 0f;
        }
    }

    /// <summary>
    /// 切换状态为待机
    /// </summary>
    public void Idle()
    {
        rigidbodyBird.simulated = false;
        ani.SetTrigger("Idle");
        this.isFlying = false;
    }

    /// <summary>
    /// 切换状态为飞行
    /// </summary>
    public void Fly()
    {
        rigidbodyBird.simulated = true;
        ani.SetTrigger("Fly");
        this.isFlying = true;
    }

    /// <summary>
    /// 角色死亡
    /// </summary>
    public void Die()
    {
        if (death) return;
        life--;
        HP = 0;
        death = true;
        this.ani.SetTrigger("Die");
        if (OnDeath != null)//当委托已绑定时
        {
            OnDeath(this);//触发委托
        }
        if (desoryOnDeath)
        {
            Destroy(this.gameObject, 0.2f);
        } 
    }

    /// <summary>
    /// 角色受伤
    /// </summary>
    /// <param name="power"></param>
    public void Damage(float power)
    {
        Debug.Log("Unit:Damage power:" + power);
        HP -= power;

        if (HP <= 0)
        {
            this.Die();
        }         
    }
}
