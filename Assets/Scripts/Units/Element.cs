using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public SIDE side;
    public Vector3 direction = Vector3.zero;
    public float destroyTime = 3f;
    public float speed = 10f;
    public float power = 1;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    /// <summary>
    /// �麯������ʼ
    /// </summary>
    public virtual void OnStart()
    {
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    /// <summary>
    /// �麯����ÿ֡�����¼�
    /// </summary>
    public virtual void OnUpdate()
    {
        //�ӵ��ƶ�
        transform.position += direction * speed * Time.deltaTime;
        //�ӵ��뿪��Ļ�ռ������
        if (!GameUtil.Instance.InScreen(this.transform.position))
        {
            Destroy(gameObject);
        }
    }

}
