using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoSingleton<GameController>
{

    public bool isRun;

    public Text txtDiamond;
    public Text txtLevel;
    public GameObject popupNextLevel;

    public int gold;
    public int diamond;
    public int level;
    public float ratio = 0;
    private int index = 0;
    private List<int> levels = new List<int> { 5, 15, 40, 95, 1000 };
    private List<float> ratios = new List<float> { 0.0f, 1f, 2.5f, 4.0f, 10.0f };
    public List<ComradeType> players = new List<ComradeType>();
    void Start()
    {
        gold = 0;
        this.ratio = this.ratios[index];
        this.level = 0;
        txtDiamond.text = gold.ToString();
        this.txtLevel.text = this.level.ToString();
        players = new List<ComradeType>();
    }
    
    public void AddGold(int num)
    {
        gold += num;
        Debug.Log("Gold: " + gold);

    }
    public void AddLevel()
    {
        this.level++;
        Game.Instance.score++;
        this.txtLevel.text = this.level.ToString();
        if(this.levels.Contains(this.level))
        {
            Debug.LogError("Qua man cmnr");
            this.isRun = false;
            this.popupNextLevel.SetActive(true);
        }
    }
    public void OnNextLevel()
    {
        this.index++;
        this.ratio = this.ratios[this.index];
        this.isRun = true;
        this.popupNextLevel.SetActive(false);
    }
    public void AddPlayer(ComradeType type)
    {
        this.players.Add(type);
    }
    public void AddDiamond(int num)
    {
        diamond += num;
        Debug.Log("Diamond: " + diamond);
        txtDiamond.text = diamond.ToString();
    }

    [ContextMenu("StartGame")]
    public void StartGame()
    {
        isRun = true;
    }


    public void StopGame()
    {
        isRun = false;
    }

}
