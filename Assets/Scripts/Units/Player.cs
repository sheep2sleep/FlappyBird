using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    private float timer = 0;

    /// <summary>
    /// �̳е��麯����ִ��ÿ֡�����߼�
    /// </summary>
    public override void OnUpdate()
    {
        if (this.death) return;

        timer += Time.deltaTime;

        //���̿���С���ƶ�
        Vector2 pos = this.transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        this.transform.position = pos;

        //ctrl���������С�񿪻�
        if (Input.GetButton("Fire1"))
        {
            this.Fire();
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
            Die();
        }
        //�������з��ӵ�
        if(bullet != null && bullet.side == SIDE.ENEMY)
        {
            Damage(bullet.power); 
        }
    }
}
