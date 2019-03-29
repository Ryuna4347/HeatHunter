using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
        GameManager.Instance.is_pause = true;
        Debug.Log("PAUSE");         
    }
}
