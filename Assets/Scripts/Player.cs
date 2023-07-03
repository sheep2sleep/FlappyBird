using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public float speed;
    public float fireRate;
    public GameObject bulletTemplate;

    private Vector3 initPosion;
    private bool isFlying = false;
    private bool death = false;

    public float HP = 100;



    //�����޲��޷���ֵ��ί��
    public delegate void DeathNotify();
    //����һ��ί��
    public event DeathNotify OnDeath;

    //ʹ���¼����ݻ�����Ϣ
    public UnityAction<int> OnScore;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
        initPosion = transform.position;
    }


    float fireTimer = 0;
    // Update is called once per frame
    void Update()
    {
        if (death) return;
        if (!isFlying) return;

        fireTimer += Time.deltaTime;

        //���̿���С���ƶ�
        Vector2 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = pos;

        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    /// <summary>
    /// С�񿪻�
    /// </summary>
    public void Fire()
    {
        if(fireTimer > 1 / fireRate)
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

    /// <summary>
    /// �л�״̬Ϊ����
    /// </summary>
    public void Die()
    {
        if(death == false)
        {
            death = true;
            if (OnDeath != null)//��ί���Ѱ�ʱ
            {
                OnDeath();//����ί��
            }
        }
    }

    /// <summary>
    /// ��ײ��������ײ��
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Die();
    }

    /// <summary>
    /// ��ײ��������
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        Element bullet = other.gameObject.GetComponent<Element>();
        if (bullet == null)
        {
            return;
        }
        Debug.LogFormat("{0}������{1} {2}", gameObject.name, other.gameObject.name, Time.time);
        if(bullet.side == SIDE.ENEMY)
        {
            HP = HP - bullet.power;
            if (HP <= 0)
            {
                Die();
            }   
        }
    }

    /// <summary>
    /// ���¿�ʼ��Ϸʱ��ʼ��С��״̬
    /// </summary>
    public void Restart()
    {
        transform.position = initPosion;
        death = false;
        Idle();
    }
 
}
