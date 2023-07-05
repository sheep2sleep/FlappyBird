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
    private float t;//��ʱ��
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
        //Todo��δ���ϼ���Ч������ΪҪͬ���޸�generateTime
        speedUp += Time.deltaTime * Time.deltaTime * 10f;
        //Debug.Log(speedUp);
        //�ܵ��Զ������ƶ�
        this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
        t += Time.deltaTime;
        if (t > generateTime * maxCount)
        {
            t = 0;
            InitPipeline();
        }
    }

    /// <summary>
    /// ��ʼ���ܵ�
    /// </summary>
    public void InitPipeline()
    {
        float y = Random.Range(minRange, maxRange);
        this.transform.localPosition = new Vector3(0, y, 0);//�ܵ��Զ�����
    }

}
