using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{
    public ENEMY_TYPE enemyType;
    public Vector2 range;
    public float destroyTime = 10f;

    private float initY = 0;

    //����һ��ί��--����
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

    // Update is called once per frame
    void Update()
    {
        //Ϊҡ��С�����ҡ��
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
}
