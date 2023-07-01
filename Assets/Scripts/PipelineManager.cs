using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject template;
    private float generateTime;//��������ͨ��Pipeline��ȡ
    private int maxCount = Pipeline.maxCount;

    List<Pipeline> pipelines = new List<Pipeline>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Coroutine runner = null;

    /// <summary>
    /// ��ʼ���ɹܵ�
    /// </summary>
    public void StartRun()
    {
        runner = StartCoroutine(GeneratePipelines());
    }

    /// <summary>
    /// ���ɶ���ܵ���Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator GeneratePipelines()
    {
        for (int i = 0; i < maxCount; ++i)
        {
            CreatePipeline();
            yield return new WaitForSeconds(generateTime);
        }
    }

    /// <summary>
    /// ���ɹܵ�
    /// </summary>
    void CreatePipeline()
    {
        if (pipelines.Count < maxCount)
        {
            GameObject obj = Instantiate(template, this.transform);
            Pipeline p = obj.GetComponent<Pipeline>();
            generateTime = p.generateTime;
            pipelines.Add(p);
        }
    }

    /// <summary>
    /// ֹͣ���ɹܵ�
    /// </summary>
    public void Stop()
    {
        StopCoroutine(runner);
        for (int i = 0; i < pipelines.Count; ++i)
        {
            pipelines[i].enabled = false;
        }
    }

    /// <summary>
    /// ������Ϸʱ���ܵ�״̬��ʼ��
    /// </summary>
    public void Restart()
    {
        //Debug.Log("Count: " + pipelines.Count);
        for (int i = 0; i < pipelines.Count; ++i)
        {
            //Debug.Log("i: " + i);
            Destroy(pipelines[i].gameObject);
        }
        pipelines.Clear();
    }
}
