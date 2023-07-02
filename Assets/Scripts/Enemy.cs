using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public float speed;
    public float fireRate;
    public GameObject bulletTemplate;

    private Vector3 initPosion;
    private bool death = false;

    //�����޲��޷���ֵ��ί��
    public delegate void DeathNotify();
    //����һ��ί��
    public event DeathNotify OnDeath;

    //ʹ���¼����ݻ�����Ϣ
    public UnityAction<int> OnScore;

    // Start is called before the first frame update
    void Start()
    {
        Fly();
        initPosion = transform.position;
    }

    float fireTimer = 0;
    // Update is called once per frame
    void Update()
    {
        if (death) return;

        fireTimer += Time.deltaTime;

        //�����Լ��ƶ�
        transform.position += new Vector3(-Time.deltaTime * speed, 0);
        Fire();
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
    }

    /// <summary>
    /// �л�״̬Ϊ����
    /// </summary>
    public void Fly()
    {
        rigidbodyBird.simulated = true;
        ani.SetTrigger("Fly");
    }

    /// <summary>
    /// �л�״̬Ϊ����
    /// </summary>
    public void Die()
    {
        if (death == false)
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
    /// ��ײ���ܵ�������
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
            //Die();
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
