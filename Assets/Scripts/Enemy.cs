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
    public float destroyTime = 10f;
    public GameObject bulletTemplate;

    private Vector3 initPosion;
    private bool isFlying = false;
    private bool death = false;

    private float initY = 0;
    public Vector2 range;

    public ENEMY_TYPE enemyType;

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
        Destroy(this.gameObject, destroyTime);

        initY = Random.Range(range.x, range.y);
        transform.localPosition = new Vector3(0, initY, 0);

    }

    float fireTimer = 0;
    // Update is called once per frame
    void Update()
    {
        if (death) return;
        if (!isFlying) return;

        //�����Զ������ʱ��
        fireTimer += Time.deltaTime;

        float y = 0;
        if(enemyType == ENEMY_TYPE.SWING_ENEMY)
        {
            y = Mathf.Sin(Time.timeSinceLevelLoad) * 3f;
        }
        //�����Լ��ƶ�
        transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, initY + y);
        Fire();
    }

    /// <summary>
    /// ���˿���
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

    /// <summary>
    /// �л�״̬Ϊ����
    /// </summary>
    public void Die()
    {
        if (death == false)
        {
            death = true;
            ani.SetTrigger("Die");
            if (OnDeath != null)//��ί���Ѱ�ʱ
            {
                OnDeath();//����ί��
            }
            Destroy(this.gameObject,0.2f);
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
        Debug.LogFormat("{0}������{1} {2}", this.gameObject.name, other.gameObject.name, Time.time);
        if (bullet.side == SIDE.PLAYER)
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
