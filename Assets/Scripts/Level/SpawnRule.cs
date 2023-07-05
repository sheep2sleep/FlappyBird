using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRule : MonoBehaviour
{
    public Unit Monster;
    public float InitTime;
    public float Period;
    public int MaxNum;
    public bool canSpawn = true;

    public int HP;
    public int Attack;

    private float timeSinceLevelStart = 0;

    private float levelStartTime = 0;

    private int num = 0;
    private float timer = 0;

    public ItemDropRule dropRule;
    private ItemDropRule rule;

    // Use this for initialization
    private void Start()
    {
        canSpawn = true;
        this.levelStartTime = Time.realtimeSinceStartup;
        if (dropRule != null)
            rule = Instantiate<ItemDropRule>(dropRule);
    }

    // Update is called once per frame
    private void Update()
    {
        timeSinceLevelStart = Time.realtimeSinceStartup - this.levelStartTime;

        //刷怪数量到了就不刷了
        if (num >= MaxNum) return;

        if (timeSinceLevelStart > InitTime && canSpawn)
        {//开始刷怪
            timer += Time.deltaTime;

            if (timer > Period)
            {
                timer = 0;
                Enemy enemy = UnitManager.Instance.CreateEnemy(this.Monster.gameObject);
                enemy.MaxHP = this.HP;
                enemy.Attack = this.Attack;
                enemy.OnDeath += Enemy_OnDeath;
                num++;
            }
        }
    }

    /// <summary>
    /// 敌人死亡规则
    /// </summary>
    /// <param name="sender"></param>
    private void Enemy_OnDeath(Unit sender)
    {
        //死亡加分
        Game.Instance.Score += sender.dieScore;
        //死亡随机掉落
        if (rule != null)
            rule.Execute(sender.transform.position);
    }
}
