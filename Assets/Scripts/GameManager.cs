using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public static GameManager g_GameManager ;

    [SerializeField]
    GameObject g_Player;

    [SerializeField]
    GameObject g_Platform;
    float g_FloatMinX;
    float g_FloatMaxX;
    float g_FloatMinY;
    float g_FloatMaxY;

    float g_FloatOffset;

    bool g_BoolIsLerping;
    float g_FloatLerpX;

    void Awake()
    {
        if ( g_GameManager == null ) g_GameManager = this;        
    }

    void m_CreateInitialPlatform ()
    {
        Vector3 l_Temp = new Vector3( Random.Range ( g_FloatMinX, g_FloatMinX + g_FloatOffset ), Random.Range ( g_FloatMinY, g_FloatMaxY ), 0.0f );
        GameObject l_TempGameObject = Instantiate( g_Platform , l_Temp, Quaternion.identity);
        l_TempGameObject.transform.parent = this.transform;

        l_Temp.y += 2.0f;
        Instantiate( g_Player, l_Temp, Quaternion.identity);

        l_Temp = new Vector3( Random.Range ( g_FloatMaxX, g_FloatMaxX - g_FloatOffset ), Random.Range ( g_FloatMinY, g_FloatMaxY ), 0.0f );
        l_TempGameObject = Instantiate( g_Platform , l_Temp, Quaternion.identity);
        l_TempGameObject.transform.parent = this.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        g_FloatMinX = -2.5f;
        g_FloatMaxX = 2.5f;
        g_FloatMinY = -4.7f;
        g_FloatMaxY = -3.7f;
        g_FloatOffset = 1.2f;
        m_CreateInitialPlatform ()  ;

        g_BoolIsLerping = false;
        g_FloatLerpX = 0.0f;
    }

    public void m_CreateNewPlatform()
    {
        float l_CameraX = Camera.main.transform.position.x;

        float l_NewMaxX = ( g_FloatMaxX * 2) + l_CameraX;
        
        Vector3 l_Temp = new Vector3 ( Random.Range( l_NewMaxX, l_NewMaxX + 1.2f), Random.Range( g_FloatMinY, g_FloatMaxY), 0.0f);
        
        Instantiate( g_Platform, l_Temp, Quaternion.identity);
    }

    public void m_Lerp( float l_LerpPosition )
    {
        g_FloatLerpX = l_LerpPosition + g_FloatMaxX;
        g_BoolIsLerping = true;
    }

    void m_LerpTheCamera()
    {
        if ( !g_BoolIsLerping ) return;

        float l_PositionX = Camera.main.transform.position.x;
        l_PositionX = Mathf.Lerp( l_PositionX, g_FloatLerpX, Time.deltaTime);
        Camera.main.transform.position = new Vector3( l_PositionX, Camera.main.transform.position.y, Camera.main.transform.position.z );

        if ( Camera.main.transform.position.x >= (g_FloatLerpX - 0.07f))
        {
            g_BoolIsLerping = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_LerpTheCamera();
    }
}
