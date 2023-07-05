using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float speed;
    public float generateTime;
    public static int maxCount = 6;
    public float minRange;
    public float maxRange;
    private float t;//计时器
    private float speedUp;

    // Start is called before the first frame update
    void Start()
    {
        InitPipeline();
        speedUp = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Todo：未用上加速效果，因为要同步修改generateTime
        speedUp += Time.deltaTime * Time.deltaTime * 10f;
        //Debug.Log(speedUp);
        //管道自动向左移动
        this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
        t += Time.deltaTime;
        if (t > generateTime * maxCount)
        {
            t = 0;
            InitPipeline();
        }
    }

    /// <summary>
    /// 初始化管道
    /// </summary>
    public void InitPipeline()
    {
        float y = Random.Range(minRange, maxRange);
        this.transform.localPosition = new Vector3(0, y, 0);//管道自动右移
    }

}
