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
    private List<SpawnRule> curRules = new List<SpawnRule>();

    public UnityAction<LEVEL_RESULT> OnLevelEnd;

    private float timeSinceLevelStart = 0;

    private float levelStartTime = 0;

    public float bossTime = 60f;

    private Boss boss = null;

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
        StartCoroutine(RunLevel());
        levelStartTime = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// ����ĳ���ؿ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator RunLevel()
    {
        UIManager.Instance.ShowLevelStart(string.Format("LEVEL {0} {1}", this.LevelID, this.Name));
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < Rules.Count; i++)
        {
            SpawnRule rule = Instantiate<SpawnRule>(Rules[i]);
            curRules.Add(rule);
        }
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
                boss = (Boss)UnitManager.Instance.CreateEnemy(this.Boss.gameObject);
                boss.target = Game.Instance.player;
                boss.Fly();//�˴��Ż�Ѹ��弤��
                boss.OnDeath += Boss_OnDeath;
            }
        }
    }

    /// <summary>
    /// Boss������ִ�е��¼�
    /// </summary>
    /// <param name="sender"></param>
    private void Boss_OnDeath(Unit sender)
    {
        stopCreateEnemy();
        Game.Instance.Score += sender.dieScore;
        this.result = LEVEL_RESULT.SUCCESS;
        if (this.OnLevelEnd != null)
            this.OnLevelEnd(this.result);
    }

    /// <summary>
    /// ֹͣ���ɵ���
    /// </summary>
    public void stopCreateEnemy()
    {
        foreach(SpawnRule rule in curRules)
        {
            rule.canSpawn = false;
        }
    }
}
