using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
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

    public TextMeshProUGUI tmpCur;
    public TextMeshProUGUI tmpFin;
    public TextMeshProUGUI tmpBest;

    public int score;//当前分数-字段
    public int Score//当前分数-属性
    {
        get { return score; }
        //更新分数时自动更新UI
        set { score = value; tmpCur.text = score.ToString(); tmpFin.text = score.ToString(); }
    }   

    public PipelineManager pipelineManager;
    public UnitManager unitManager;

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
        player.OnScore = Player_OnScore;//绑定事件
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
        Status = GAME_STATUS.InGame;
        //Debug.LogFormat("Status: {0}", Status);

        pipelineManager.StartRun();
        unitManager.Begin();
        player.Fly();

        groundAmi.speed = 1;
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
    private void Player_OnDeath()
    {
        Status = GAME_STATUS.GameOver;
        pipelineManager.Stop();
        unitManager.Stop();
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
        pipelineManager.Restart();
        player.Restart();
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
