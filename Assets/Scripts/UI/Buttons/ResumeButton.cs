using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour
{
    public void Resume()
    {
        Time.timeScale = 1;
        GameManager.Instance.is_pause = false;
        Debug.Log("RESUME");
            
    }
}
