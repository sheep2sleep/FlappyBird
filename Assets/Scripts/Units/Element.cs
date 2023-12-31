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
    /// 虚函数，开始
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
    /// 虚函数，每帧更新事件
    /// </summary>
    public virtual void OnUpdate()
    {
        //子弹移动
        transform.position += direction * speed * Time.deltaTime;
        //子弹离开屏幕空间后销毁
        if (!GameUtil.Instance.InScreen(this.transform.position))
        {
            Destroy(gameObject);
        }
    }

}
