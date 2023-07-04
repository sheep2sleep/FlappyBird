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

    /// <summary>
    /// ����ÿ֡����
    /// </summary>
    public override void OnUpdate()
    {
        if (!running)
            return;

        if (target != null)
        {
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

        if (target != null)
        {
            Player p = target.GetComponent<Player>();
            p.Damage(power);
        }
    }

}
