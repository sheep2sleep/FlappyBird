using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public GameObject bulletTemplate;
    public float HP = 100f;
    public float MaxHP = 100f;
    public float speed = 5f;
    public float fireRate = 10f;

    protected Vector3 initPosion;
    protected bool death = false;
    protected bool isFlying = false;
    protected float fireTimer = 0f;

    //�����޲��޷���ֵ��ί��--����֪ͨ
    public delegate void DeathNotify();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (death) return;
        if (!isFlying) return;

        //�����ʱ��ÿ֡����
        fireTimer += Time.deltaTime;
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
            bullt.transform.position = transform.position;

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
}
