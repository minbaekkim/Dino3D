using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown (PointerEventData eventData) 
	{
        if(!PlayerController.Instance.IsDead)
            PlayerController.Instance.PlayerJump();
	}
}
