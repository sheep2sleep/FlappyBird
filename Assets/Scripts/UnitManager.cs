using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject enemyTemplate;
    public GameObject enemyTemplate2;
    public GameObject enemyTemplate3;

    public float generateTime1 = 1f;
    public float generateTime2 = 1f;
    public float generateTime3 = 1f;

    public float minRange;
    public float maxRange;

    public List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        this.enemies.Clear();
    }

    Coroutine runner = null;

    public void Begin()
    {
        runner = StartCoroutine(GeneratEnemies());
    }

    public void Stop()
    {
        StopCoroutine(runner);
        this.enemies.Clear();
    }


    //敌人生成计时器
    int timer1 = 0;
    int timer2 = 0;
    int timer3 = 0;

    /// <summary>
    /// 生成多个敌人的协程
    /// </summary>
    /// <returns></returns>
    IEnumerator GeneratEnemies()
    {
        while (true)
        {
            //生成敌人1
            if(timer1 > generateTime1)
            {
                CreateEnemy(enemyTemplate);
                timer1 = 0;
            }
            //生成敌人2
            if(timer2 > generateTime2)
            {
                CreateEnemy(enemyTemplate2);
                timer2 = 0;
            }
            //生成敌人3
            if(timer3 > generateTime3)
            {
                CreateEnemy(enemyTemplate3);
                timer3 = 0;
            }
            timer1++;
            timer2++;
            timer3++;

            yield return new WaitForSeconds(1f);
        }   
    }

    /// <summary>
    /// 创建指定类型的敌人
    /// </summary>
    /// <param name="templates"></param>
    /// <returns></returns>
    public Enemy CreateEnemy(GameObject templates)
    {
        if (templates == null)
            return null;

        GameObject obj = Instantiate(templates, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        this.enemies.Add(p);

        return p;
    }
}
