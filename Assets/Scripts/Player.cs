using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{    
    //����һ��ί��--����
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
        //���̿���С���ƶ�
        Vector2 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = pos;

        //ctrl���������С�񿪻�
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
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
    /// ��ײ��������
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        Element bullet = other.gameObject.GetComponent<Element>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (bullet == null && enemy == null)
        {
            return;
        }
        Debug.LogFormat("{0}������{1} {2}", gameObject.name, other.gameObject.name, Time.time);

        //����������
        if(enemy != null)
        {
            HP = 0;
            Die();
        }

        //�������з��ӵ�
        if(bullet != null && bullet.side == SIDE.ENEMY)
        {
            HP = HP - bullet.power;
            if (HP <= 0)
            {
                Die();
            }   
        }
    }

    
 
}
