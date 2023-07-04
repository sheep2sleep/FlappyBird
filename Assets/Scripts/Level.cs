using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public int LevelID;
    public string Name;

    public Boss Boss;

    public List<SpawnRule> Rules = new List<SpawnRule>();

    public UnitManager unitManager;

    public UnityAction<LEVEL_RESULT> OnLevelEnd;

    private float timeSinceLevelStart = 0;

    private float levelStartTime = 0;

    public float bossTime = 60f;

    private float timer = 0;

    private Boss boss = null;

    public Player currentPlayer;

    public enum LEVEL_RESULT
    {
        NONE,
        SUCCESS,
        FAILD
    }
    public LEVEL_RESULT result = LEVEL_RESULT.NONE;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Rules.Count; i++)
        {
            SpawnRule rule = Instantiate<SpawnRule>(Rules[i]);
            rule.unitManager = this.unitManager;
        }
        levelStartTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;
        

        if (this.result != LEVEL_RESULT.NONE)
            return;

        if (timeSinceLevelStart > bossTime)
        {
            if (boss == null && result == LEVEL_RESULT.NONE)
            {
                boss = (Boss)unitManager.CreateEnemy(this.Boss.gameObject);
                boss.target = currentPlayer;
                boss.Fly();//此处才会把刚体激活
                boss.OnDeath += Boss_OnDeath;
            }
        }
    }

    private void Boss_OnDeath(Unit sender)
    {
        this.result = LEVEL_RESULT.SUCCESS;
        if (this.OnLevelEnd != null)
            this.OnLevelEnd(this.result);
    }
}
