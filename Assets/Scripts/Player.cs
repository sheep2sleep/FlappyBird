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
        Die();
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
            Die();
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
