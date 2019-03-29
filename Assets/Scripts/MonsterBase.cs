using UnityEngine;
using System.Collections;

public class MonsterBase : MonoBehaviour {
    public Animator a;
    public Rigidbody2D rigid;

    public MonsterState monstate = MonsterState .Idle;

    public virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        a = GetComponentInChildren<Animator>();
    }

    public virtual void OnEnable()
    {
        StartCoroutine("FSMMain");
    }

    IEnumerator FSMMain()
    {
        while (true)
        {
            //Debug.Log(state.ToString());
            yield return StartCoroutine(monstate.ToString());
        }
    }

    public virtual IEnumerator Idle()
    {
        //Entry
        do
        {
            yield return null;
            //Process

        } while (monstate == MonsterState.Idle);
        //Exit
    }

    virtual public void SetMonState(MonsterState newState)
    {
        monstate = newState;
        a.SetInteger("monstate", (int)monstate);
    }

    public bool IsDead()
    {
        return (GetComponent<Enemy>().hp<=0);
    }
}

