using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Element
{
    //��������Ŀ��
    public Transform target;
    private bool running = false;

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
                //this.Explod();
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

}
