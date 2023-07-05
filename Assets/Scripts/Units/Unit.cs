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

    //�����޲��޷���ֵ��ί����--����֪ͨ
    public delegate void DeathNotify(Unit sender);
    //����һ��ί��--����
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
    /// �麯��OnStart��ִ�г�ʼ������
    /// </summary>
    public virtual void OnStart()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (death) return;
        if (!isFlying) return;

        //�����ʱ��ÿ֡����
        fireTimer += Time.deltaTime;

        OnUpdate();
    }

    /// <summary>
    /// �麯��OnUpdate��ִ��ÿ֡���¹���
    /// </summary>
    public virtual void OnUpdate()
    {
    }

    /// <summary>
    /// ��ʼ��С��״̬
    /// </summary>
    public void Init()
    {
        Idle();
        transform.position = initPosion;
        death = false;
        HP = MaxHP;
    }

    /// <summary>
    /// С�񿪻�
    /// </summary>
    public void Fire()
    {
        if (fireTimer > 1 / fireRate)
        {
            GameObject bullt = Instantiate(bulletTemplate);
            bullt.transform.position = firePoint.position;
            //�����ӵ�����
            bullt.GetComponent<Element>().direction = side == SIDE.PLAYER ? Vector3.right : Vector3.left;
            fireTimer = 0f;
        }
    }

    /// <summary>
    /// �л�״̬Ϊ����
    /// </summary>
    public void Idle()
    {
        rigidbodyBird.simulated = false;
        ani.SetTrigger("Idle");
        this.isFlying = false;
    }

    /// <summary>
    /// �л�״̬Ϊ����
    /// </summary>
    public void Fly()
    {
        rigidbodyBird.simulated = true;
        ani.SetTrigger("Fly");
        this.isFlying = true;
    }

    /// <summary>
    /// ��ɫ����
    /// </summary>
    public void Die()
    {
        if (death) return;
        life--;
        HP = 0;
        death = true;
        this.ani.SetTrigger("Die");
        if (OnDeath != null)//��ί���Ѱ�ʱ
        {
            OnDeath(this);//����ί��
        }
        if (desoryOnDeath)
        {
            Destroy(this.gameObject, 0.2f);
        } 
    }

    /// <summary>
    /// ��ɫ����
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
