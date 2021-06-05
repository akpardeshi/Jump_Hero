using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager g_ScoreManager;

    [SerializeField]
    Text g_ScoreText;

    int g_IntScore;

    void Awake()
    {
        if ( ScoreManager.g_ScoreManager == null ) ScoreManager.g_ScoreManager = this;

        g_ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    public void m_IncrementScore()
    {
        g_IntScore++;
        g_ScoreText.text = g_IntScore.ToString();
    }

    public int m_GetScore()
    {
        return this.g_IntScore;
    }
}
