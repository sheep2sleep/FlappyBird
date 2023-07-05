using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Element
{
    //��������Ŀ��
    public Transform target;
    private bool running = false;

    public GameObject fxExpold;

    private float existTimer = 0f;

    /// <summary>
    /// ������ʼ
    /// </summary>
    public override void OnStart()
    {
        //��������������������Զ����ٵüӱ�ը����
        //���Բ�ʹ��ԭstart�е�Destory����������Ӽ�ʱ��
    }

    /// <summary>
    /// ����ÿ֡����
    /// </summary>
    public override void OnUpdate()
    {
        if (!running)
            return;

        if (target != null)
        {
            //�Զ���ը
            existTimer += Time.deltaTime;
            if (existTimer > destroyTime)
            {
                Explod();
                existTimer = 0;
            }
            //Ŀ��λ�þ����Լ�λ�õķ���
            Vector3 dir = (target.position - this.transform.position);
            if (dir.magnitude < 0.1)
            {
                Explod();
            }
            //��������ת��Ŀ�귽��
            this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            //��֡�������˶�
            this.transform.position += speed * Time.deltaTime * dir.normalized;
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void Launch()
    {
        running = true;
    }

    /// <summary>
    /// ������ը
    /// </summary>
    private void Explod()
    {
        Destroy(this.gameObject);
        Instantiate(fxExpold, this.transform.position, Quaternion.identity);

        //�ٴ��ж��Ƿ�������
        if (target != null && (target.position - this.transform.position).magnitude < 0.1)
        {
            Player p = target.GetComponent<Player>();
            p.Damage(power);
        }
    }

}
