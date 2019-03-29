using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    public bool is_pause
    {
        get; set;
    }
    public bool is_game_on
    {
        get; set;
    }
    
}
