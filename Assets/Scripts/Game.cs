using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoSingleton<Game>
{
    public Player player;
    public int currentLevelId = 1;

    private GAME_STATUS status;//当前游戏状态-字段
    public GAME_STATUS Status//当前游戏状态-属性
    {
        get { return status; }
        set { status = value; UIManager.Instance.UpdateUI(); }
    }

    public int score;//当前分数-字段
    public int Score//当前分数-属性
    {
        get { return score; }
        //更新分数时自动更新UI
        set { score = value; UIManager.Instance.UpdateScore(score); }
    }

    public Animator groundAmi;

    private void Awake()
    {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //Application.targetFrameRate = 60;
        //#else 
        //Application.targetFrameRate = -1;
        //#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        //修改帧率为60帧
        Application.targetFrameRate = 60;
        Status = GAME_STATUS.Ready;
        player.OnDeath += Player_OnDeath;//绑定委托函数
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    /// <summary>
    /// 点击开始游戏按钮
    /// </summary>
    public void StartGame()
    {
        player.Init();
        Status = GAME_STATUS.InGame;        
        UnitManager.Instance.Clear();
        player.Fly();

        LoadLevel();

        groundAmi.speed = 1;
    }

    /// <summary>
    /// 加载关卡
    /// </summary>
    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(currentLevelId);
        
        LevelManager.Instance.level.OnLevelEnd = OnLevelEnd;
    }

    /// <summary>
    /// 为UnityEvent绑定执行事件
    /// </summary>
    /// <param name="result"></param>
    private void OnLevelEnd(Level.LEVEL_RESULT result)
    {
        if (result == Level.LEVEL_RESULT.SUCCESS)
        {
            if (currentLevelId < LevelManager.Instance.levels.Count)
            {
                this.currentLevelId++;
                this.LoadLevel();
            }
            else
            {
                Status = GAME_STATUS.GameOver;
            }
            
        }
        else
        {
            this.Status = GAME_STATUS.GameOver;
        }
    }    

    /// <summary>
    /// 玩家死亡委托执行
    /// </summary>
    private void Player_OnDeath(Unit sender)
    {
        if(player.life <= 0)
        {
            Status = GAME_STATUS.GameOver;
            UnitManager.Instance.Clear();
        }
        else
        {
            //玩家生命数量不为0直接复活
            player.Rebirth();
        }
        
        groundAmi.speed = 0;
    }

    /// <summary>
    /// 玩家积分事件执行
    /// </summary>
    private void Player_OnScore(int score)
    {
        Score += score;
    }

    /// <summary>
    /// 回到主界面
    /// </summary>
    public void Restart()
    {
        Status = GAME_STATUS.Ready;
        //pipelineManager.Restart();
        player.Init();
        Score = 0;
        groundAmi.speed = 1;
    }


}
