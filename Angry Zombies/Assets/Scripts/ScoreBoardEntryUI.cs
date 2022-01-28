using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardEntryUI : MonoBehaviour
{
    public TextMeshProUGUI entryNameText;
    public TextMeshProUGUI entryScoreText;

    public void Initialize(ScoreBoardEntry scoreBoardEntry){
        entryNameText.text = scoreBoardEntry.entryName;
        entryScoreText.text = scoreBoardEntry.entryScore.ToString();
    }
}
