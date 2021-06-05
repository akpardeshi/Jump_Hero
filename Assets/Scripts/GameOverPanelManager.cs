using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelManager : MonoBehaviour
{
    public static GameOverPanelManager g_GameOverPanelManager;

    [SerializeField]
    GameObject g_GameOverPanel;
    Animator g_GameOverAnimation;

    [SerializeField]
    Button g_PlayAgainButton;

    [SerializeField]
    Button g_BackButton;

    Text g_FinalScore;

    void Awake ()
    {
        if ( g_GameOverPanelManager == null )
        {
            g_GameOverPanelManager = this;
        }        

        g_FinalScore = GameObject.Find("FinalScore").GetComponent<Text>();

        g_GameOverPanel.SetActive(false);    

        g_GameOverAnimation = g_GameOverPanel.GetComponent<Animator>();

        g_PlayAgainButton.onClick.AddListener ( () => m_PlayAgain ()) ;
        g_BackButton.onClick.AddListener( () => m_BackToMenu ()) ;
    }

    public void m_ShowGameOverPanel()
    {
        g_GameOverPanel.SetActive(true);
        int l_IntScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().m_GetScore();
        g_FinalScore.text = "Score : \n" + "" + l_IntScore.ToString();
    }

    public void m_PlayAgain()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void m_BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
