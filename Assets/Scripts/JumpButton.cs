using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    public void OnPointerDown ( PointerEventData data )
    {
        if ( PlayerJump.g_PlayerJumpInstance != null )
        {
            PlayerJump.g_PlayerJumpInstance.m_SetPower(true);
        }
    }

    public void OnPointerUp  ( PointerEventData data )
    {
        if ( PlayerJump.g_PlayerJumpInstance != null )
        {
            PlayerJump.g_PlayerJumpInstance.m_SetPower(false);
        }
    }
}
