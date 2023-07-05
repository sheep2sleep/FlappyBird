using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelGameOver;
    public GameObject uiLevelStart;
    public GameObject uiLevelEnd;

    public TextMeshProUGUI tmpCur;
    public TextMeshProUGUI tmpFin;
    public TextMeshProUGUI tmpBest;
    public TextMeshProUGUI uiLife;
    public TextMeshProUGUI uiLevelName;
    public TextMeshProUGUI uiLevelStartName;

    public Slider hpBar;

    

    // Start is called before the first frame update
    void Start()
    {
        panelReady.SetActive(true);
        hpBar.value = Game.Instance.player.HP;
    }

    // Update is called once per frame
    void Update()
    {
        //血条插值更新
        if (Game.Instance.Status == GAME_STATUS.InGame)
        {
            hpBar.value = Mathf.Lerp(hpBar.value, Game.Instance.player.HP, 0.1f);
        }else if(Game.Instance.Status == GAME_STATUS.GameOver)
        {
            UpdateBestScore();
        }
        if (Game.Instance.player != null)
        {
            uiLife.text = Game.Instance.player.life.ToString();
        }
    }
    /// <summary>
    /// 开始新的一关
    /// </summary>
    /// <param name="name"></param>
    public void ShowLevelStart(string name)
    {
        uiLevelStartName.text = name;
        uiLevelName.text = name;
        uiLevelStart.SetActive(true);
       
    }

    /// <summary>
    /// 每帧更新分数
    /// </summary>
    /// <param name="Score"></param>
    public void UpdateScore(int Score)
    {
        tmpCur.text = Game.Instance.Score.ToString();
        tmpFin.text = Game.Instance.Score.ToString();
    }

    /// <summary>
    /// 更新结算分数
    /// </summary>
    /// <param name="Score"></param>
    public void UpdateBestScore()
    {
        int bestScore = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        bestScore = Math.Max(bestScore, Game.Instance.Score);
        PlayerPrefs.SetInt("BestScore", bestScore);
        tmpBest.text = bestScore.ToString();
    }

    /// <summary>
    /// 根据当前status更新UI
    /// </summary>
    public void UpdateUI()
    {
        panelReady.SetActive(Game.Instance.Status == GAME_STATUS.Ready);
        panelInGame.SetActive(Game.Instance.Status == GAME_STATUS.InGame);
        panelGameOver.SetActive(Game.Instance.Status == GAME_STATUS.GameOver);
        hpBar.maxValue = Game.Instance.player.MaxHP;
        hpBar.value = Game.Instance.player.HP;
    }
}
