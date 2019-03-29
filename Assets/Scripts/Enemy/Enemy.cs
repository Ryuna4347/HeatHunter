using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{


    public int hp;
    // Use this for initialization
    void Awake()
    { 
    }
    

    public void ReduceHP(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GetComponent<MonsterMove>().SetMonState(MonsterState.Dead);
        }
    }
}
