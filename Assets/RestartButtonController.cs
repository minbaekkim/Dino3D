using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonController : MonoBehaviour
{
    public Image restartButton;
    public void DeActivateButton()
    {
        restartButton.raycastTarget = false;
    }
    public void ActivateButton()
    {
        restartButton.raycastTarget = true;
    }
}
