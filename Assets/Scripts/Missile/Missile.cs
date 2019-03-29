using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private float timer = 0.7f;
    public int damage = 1;
    public PlayerMove playerMove;
    public Vector3 localScaleParent; //Player의 localScale에 따라 탄환의 방향을 결정

    public void set_damage(int power)
    {
        damage = power;
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy") {
            coll.gameObject.GetComponent<Enemy>().ReduceHP(damage);
            DestroyThis();
         }
    }
    void OnEnable() { //처음 생성되었을때 발사방향을 구하기 위해서
        localScaleParent = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale;
        if (localScaleParent.x < 0)
        { //좌향발사시 미사일 반전
            Vector2 temp = GetComponent<Transform>().localScale;
            temp.x *= -1;
            GetComponent<Transform>().localScale = temp;
        }
    }
    // GameObject m_player = GameObject.FindGameObjectWithTag("Player");
    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            DestroyThis();
        }
        timer -= Time.deltaTime;

        if (localScaleParent.x < 0) //캐릭터의 좌측에서 나온다면 미사일도 왼쪽으로 날아감.
        {
            transform.Translate(Vector2.left * 15f * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * 15f * Time.deltaTime);
        }
    }
}
