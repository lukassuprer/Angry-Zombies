using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreBoard : MonoBehaviour
{
    public int maxScoreBoardEntries = 5;
    public Transform highScoresHolder = null;
    public GameObject scoreBoardEntryObject = null;
    public UIManager uiManager;
    public PlayerController playerController;

    [SerializeField] private string testEntryName = "New Name";
    [SerializeField] private int testEntryScore = 0;
    public ScoreBoardEntry testEntryData = new ScoreBoardEntry();
    private string savePath => $"{Application.dataPath}/highscores.json";
    private void Start(){
        ScoreBoardSaveData savedScores =  GetSavedScores();

        UpdateUI(savedScores);

        SaveScores(savedScores);
    }
    void Update()
    {
        if(playerController.health <= 0 && GameManager.addedScore == false || GameManager.gameExit == true && GameManager.addedScore == false){
            AddEntry(new ScoreBoardEntry(){
                entryName = uiManager.playerName,
                entryScore = GameManager.score
            });
            GameManager.addedScore = true;
        }
    }
    [ContextMenu("AddTestEntry")]
    public void AddTestEntry(){
        AddEntry(new ScoreBoardEntry(){
            entryName = testEntryName,
            entryScore = testEntryScore
        });
    }
    public void AddEntry(ScoreBoardEntry scoreBoardEntry){
        ScoreBoardSaveData savedScores = GetSavedScores();
        bool scoreAdded = false;

        for(int i = 0; i < savedScores.highscores.Count; i++){
            if(scoreBoardEntry.entryScore > savedScores.highscores[i].entryScore){
                savedScores.highscores.Insert(i, scoreBoardEntry);
                scoreAdded = true;
                break;
            }
        }
        if(!scoreAdded && savedScores.highscores.Count < maxScoreBoardEntries){
            savedScores.highscores.Add(scoreBoardEntry);
        }
        if(savedScores.highscores.Count > maxScoreBoardEntries){
            savedScores.highscores.RemoveRange(maxScoreBoardEntries, savedScores.highscores.Count - maxScoreBoardEntries);
        }
        UpdateUI(savedScores);
        SaveScores(savedScores);
    }
    private void UpdateUI(ScoreBoardSaveData savedScores){
        foreach(Transform child in highScoresHolder){
            Destroy(child.gameObject);
        }
        foreach(ScoreBoardEntry highscore in savedScores.highscores){
            Instantiate(scoreBoardEntryObject, highScoresHolder).GetComponent<ScoreBoardEntryUI>().Initialize(highscore);
        }
    }
    private ScoreBoardSaveData GetSavedScores(){
        if(!File.Exists(savePath)){
            File.Create(savePath).Dispose();
            return new ScoreBoardSaveData();
        }

        using(StreamReader stream = new StreamReader(savePath)){
            string json = stream.ReadToEnd();
            return JsonUtility.FromJson<ScoreBoardSaveData>(json);
        }
    }
    private void SaveScores(ScoreBoardSaveData scoreBoardSaveData){
        using(StreamWriter stream = new StreamWriter(savePath)){
            string json = JsonUtility.ToJson(scoreBoardSaveData, true);
            stream.Write(json);
        }
    }
}
