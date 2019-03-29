using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneButton : MonoBehaviour
{
     public void ChangeScene(int scene_number)
    {
        SceneManager.LoadScene(scene_number);
        if(scene_number == 1)
        {
            GameManager.Instance.is_game_on = true;
            Debug.Log("GAME ON");
            GameManager.Instance.is_pause = false;
            Debug.Log("GAME PLAYING");
            Time.timeScale = 1;
        }
        if(scene_number >2)
        {
            GameManager.Instance.is_game_on = false;
            Debug.Log("GAME OVER");
        }
    }
}
