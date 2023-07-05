using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoSingleton<Game>
{
    public enum GAME_STATUS
    {
        Ready,
        InGame,
        GameOver
    }

    private GAME_STATUS status;//当前游戏状态-字段
    public GAME_STATUS Status//当前游戏状态-属性
    {
        get { return status; }
        set { status = value; UpdataUI(); }
    }

    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelGameOver;
    public Player player;
    public int currentLevelId = 1;

    public TextMeshProUGUI tmpCur;
    public TextMeshProUGUI tmpFin;
    public TextMeshProUGUI tmpBest;
    public TextMeshProUGUI uiLife;
    public TextMeshProUGUI uiLevelName;
    public Slider hpBar;

    public int score;//当前分数-字段
    public int Score//当前分数-属性
    {
        get { return score; }
        //更新分数时自动更新UI
        set { score = value; tmpCur.text = score.ToString(); tmpFin.text = score.ToString(); }
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
        //血条插值更新
        if(Status == GAME_STATUS.InGame)
        {
            hpBar.value = Mathf.Lerp(hpBar.value, player.HP, 0.1f);
        }
        if(player != null){
            uiLife.text = player.life.ToString();
        }

    }

    /// <summary>
    /// 点击开始游戏按钮
    /// </summary>
    public void StartGame()
    {
        Status = GAME_STATUS.InGame;
        
        UnitManager.Instance.Clear();
        player.Fly();
        hpBar.value = player.HP;

        LoadLevel();

        groundAmi.speed = 1;
    }

    /// <summary>
    /// 加载关卡
    /// </summary>
    private void LoadLevel()
    {
        LevelManager.Instance.LoadLevel(currentLevelId);
        uiLevelName.text = string.Format("LEVEL {0} {1}", LevelManager.Instance.level.LevelID.ToString(), LevelManager.Instance.level.Name.ToString());
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
            this.currentLevelId++;
            this.LoadLevel();
        }
        else
        {
            this.Status = GAME_STATUS.GameOver;
        }
    }

    /// <summary>
    /// 根据当前status更新UI
    /// </summary>
    public void UpdataUI()
    {
        panelReady.SetActive(Status == GAME_STATUS.Ready);
        panelInGame.SetActive(Status == GAME_STATUS.InGame);
        panelGameOver.SetActive(Status == GAME_STATUS.GameOver);
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
        
        UnitManager.Instance.Clear();
        UpdateScore();
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
        hpBar.value = player.HP;
        Score = 0;
        groundAmi.speed = 1;
    }

    public void UpdateScore()
    {
        int bestScore = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        bestScore = Math.Max(bestScore, Score);
        PlayerPrefs.SetInt("BestScore", bestScore);
        tmpBest.text = bestScore.ToString();
    }
}
