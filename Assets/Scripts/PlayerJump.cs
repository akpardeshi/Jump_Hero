using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
    public static PlayerJump g_PlayerJumpInstance ;

    Rigidbody2D g_Rigidbody ;
    Animator g_Animator ;

    [SerializeField]
    float g_FloatForceX ;
    [SerializeField]
    float g_FloatForceY ;

    float g_FloatTersholdX ;
    float g_FloatTersholdY ;

    float g_FloatMaxForceX;
    float g_FloatMaxForceY;

    bool g_BoolSetPower ;
    bool g_BoolDidJump ;

    // Power Bar
    Slider g_PowerBar;
    float g_FloatPowerBarTreshold;
    float g_FloatPowerBarValue;
    
    void Awake ()
    {
        m_MakeInstance () ;

        g_Rigidbody = this.GetComponent<Rigidbody2D>();
        g_Animator = this.GetComponent<Animator>();

        g_PowerBar = GameObject.Find("Slider").GetComponent<Slider>();
        g_PowerBar.minValue = 0.0f;
        g_PowerBar.maxValue = 10.0f;
        g_PowerBar.value = g_FloatPowerBarValue;
    }

    void m_MakeInstance ()
    {
        if ( g_PlayerJumpInstance == null ) 
        {
            g_PlayerJumpInstance = this ;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        g_FloatTersholdX = 7.0f;
        g_FloatTersholdY = 14.0f; 
        g_FloatMaxForceX = 6.5f;
        g_FloatMaxForceY = 13.5f;

        g_FloatPowerBarTreshold = 10.0f;
        g_FloatPowerBarValue = 0.0f;
    }

    void m_SetPower()
    {
        if ( g_BoolSetPower )
        {
            g_FloatForceX += g_FloatTersholdX * Time.deltaTime;
            g_FloatForceY += g_FloatTersholdY * Time.deltaTime;

            g_FloatForceX = Mathf.Clamp( g_FloatForceX, 0, g_FloatMaxForceX );
            g_FloatForceY = Mathf.Clamp( g_FloatForceY, 0, g_FloatMaxForceY);

            g_FloatPowerBarValue += g_FloatPowerBarTreshold * Time.deltaTime;
            g_PowerBar.value = g_FloatPowerBarValue;
        }
    }

    public void m_SetPower ( bool l_BoolSetPower )
    {
        g_BoolSetPower = l_BoolSetPower ;

        if ( !l_BoolSetPower )
        {
            m_Jump();
        }
    }

    void m_Jump()
    {
        g_Rigidbody.velocity = new Vector2( g_FloatForceX, g_FloatForceY );
        g_FloatForceX = 0.0f;
        g_FloatForceY = 0.0f;
        g_BoolDidJump = true;

        g_Animator.SetBool( "IsJumping", g_BoolDidJump);

        g_FloatPowerBarValue = 0.0f;
        g_PowerBar.value = g_FloatPowerBarValue;
    }

    void OnTriggerEnter2D( Collider2D trigger )
    {
        if ( g_BoolDidJump )
        {
            g_BoolDidJump = false;
            g_Animator.SetBool( "IsJumping", g_BoolDidJump);

            if ( trigger.CompareTag("Platform"))
            {
                if ( GameManager.g_GameManager != null)
                {
                    GameManager.g_GameManager.m_CreateNewPlatform();
                    GameManager.g_GameManager.m_Lerp( trigger.transform.position.x);
                }

                if ( ScoreManager.g_ScoreManager != null )
                {
                    ScoreManager.g_ScoreManager.m_IncrementScore();
                }
            }             

            if ( trigger.CompareTag("GameOver"))
            {        
                GameObject.Find("ScoreText").SetActive(false);
                GameObject.Find("GameOverManager").GetComponent<GameOverPanelManager>().m_ShowGameOverPanel();
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_SetPower();
    }
}
