using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject enemyTemplate;
    public float generateTime = 1f;
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

    /// <summary>
    /// 生成多个敌人的协程
    /// </summary>
    /// <returns></returns>
    IEnumerator GeneratEnemies()
    {
        while (true)
        {
            CreateEnemy(enemyTemplate);
            yield return new WaitForSeconds(generateTime);
        }   
    }

    public Enemy CreateEnemy(GameObject templates)
    {
        if (templates == null)
            return null;

        GameObject obj = Instantiate(templates, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        this.enemies.Add(p);

        float y = Random.Range(minRange, maxRange);
        obj.transform.localPosition = new Vector3(0, y, 0);

        return p;
    }
}
