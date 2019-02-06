using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text score;
    private void Start()
    {
        score = GetComponent<Text>();
        score.text = "Wynik:" + GameManager.Instance.Score;
    }
}
