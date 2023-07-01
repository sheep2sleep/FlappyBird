using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject template;
    private float generateTime;//不公开但通过Pipeline获取
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
    /// 开始生成管道
    /// </summary>
    public void StartRun()
    {
        runner = StartCoroutine(GeneratePipelines());
    }

    /// <summary>
    /// 生成多个管道的协程
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
    /// 生成管道
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
    /// 停止生成管道
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
    /// 重启游戏时将管道状态初始化
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
