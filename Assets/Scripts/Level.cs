using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int LevelID;
    public string Name;

    public Boss Boss;

    public List<SpawnRule> Rules = new List<SpawnRule>();

    public UnitManager unitManager;

    //public UnityAction<LEVEL_RESULT> OnLevelEnd;

    private float timeSinceLevelStart = 0;

    private float levelStartTime = 0;

    public float bossTime = 60f;

    private float timer = 0;

    private Boss boss = null;

    public Player currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Rules.Count; i++)
        {
            SpawnRule rule = Instantiate<SpawnRule>(Rules[i]);
            rule.unitManager = this.unitManager;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;

        //if (this.result != LEVEL_RESULT.NONE)
        //    return;

        if (timeSinceLevelStart > bossTime)
        {
            if (boss == null)
            {
                boss = (Boss)unitManager.CreateEnemy(this.Boss.gameObject);
                boss.target = currentPlayer;
                boss.Fly();
                //boss.OnDeath += Boss_OnDeath;
            }
        }
    }
}
