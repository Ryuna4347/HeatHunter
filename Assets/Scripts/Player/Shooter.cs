using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{                      [SerializeField]
    private Animator BulletEffect;
    public static int missile_level;
    private float cool_down = 0f;
    private float cool_delay = 0.25f;
    public GameObject MissilePrefab_LV1;
    public GameObject MissilePrefab_LV2;
    public GameObject MissilePrefab_LV3;
    public GameObject bluebar; //버서크모드를 위한 바의 정보
    public GameObject chargebar; //버서크 바 위에 얼마나 파워를 충전했는지에 대한 정보
    int leftright; //탄환의 위치
    bool keyIsPressed = false;
    float pressedTime = 0;
    float pressingMaxTime = 2.0f;   

    // Update is called once per frame
    void Update()
    {
       
        if (keyIsPressed == false)
            cool_down -= Time.deltaTime;
        
        if (cool_down <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {                                                                 
            
                if (keyIsPressed == false)
                {
                  
                    keyIsPressed = true;
                    BulletEffect.SetBool("IsPressed", keyIsPressed);

                    pressedTime = Time.deltaTime;
                    if (pressedTime < 1.0f)
                    {
                        missile_level = 1;
                        chargebar.GetComponent<Image>().fillAmount = 0.33f;
                    }
                    else
                    {
                        missile_level = 2;
                        chargebar.GetComponent<Image>().fillAmount = 0.66f;
                    }
                    BulletEffect.SetInteger("missile_level", missile_level);

                }
                else
                {
                    pressedTime += Time.deltaTime;
                    if (pressedTime < 1.0f)
                    {
                        chargebar.GetComponent<Image>().fillAmount = 0.33f;
                    }
                    else if(pressedTime < 2.0f)
                    {
                        chargebar.GetComponent<Image>().fillAmount = 0.66f;
                    }
                    else
                    {
                        chargebar.GetComponent<Image>().fillAmount = 1.0f;
                    }
                }
            }
            else
            {
                // fire
            
                if( keyIsPressed == true)
                {
                    float direction= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale.x;

                    keyIsPressed = false;
                    BulletEffect.SetBool("IsPressed", keyIsPressed);

                    if (direction> 0) {
                        leftright = 1;
                    }
                    else if(direction<0){
                        leftright = -1;
                    }

                    cool_down = cool_delay;
                    float ratio = pressedTime / pressingMaxTime;
                    ratio = Mathf.Clamp(ratio, 0.0f, 1.0f);
                    bluebar.GetComponent<Image>().type = Image.Type.Filled;
                    bluebar.GetComponent<Image>().fillMethod = Image.FillMethod.Horizontal;
                    if (bluebar.GetComponent<Image>().fillAmount != 1.0f) //버서크바가 덜 찼을 경우
                    {
                        if (ratio < 0.5f)
                        {
                            bluebar.GetComponent<Image>().fillAmount += 0.04f;
                            Instantiate(MissilePrefab_LV1, transform.position + new Vector3(0.7f, 0, 0)*leftright, transform.rotation);
                        }
                        else if (ratio < 1.0f)
                        {
                            bluebar.GetComponent<Image>().fillAmount += 0.06f;
                            Instantiate(MissilePrefab_LV2, transform.position + new Vector3(0.7f, 0, 0) * leftright, transform.rotation);
                        }
                        else
                        {
                            Instantiate(MissilePrefab_LV3, transform.position + new Vector3(0.7f, 0, 0) * leftright, transform.rotation);
                            bluebar.GetComponent<Image>().fillAmount += 0.08f;
                        }
                    }
                    if (bluebar.GetComponent<Image>().fillAmount >= 1.0f) //버서크 바가 전부 다 찼을 경우 일정시간동안 3단계 탄알 발사
                    {
                        Instantiate(MissilePrefab_LV3, transform.position + new Vector3(0.7f, 0, 0) * leftright, transform.rotation);
                        StartCoroutine("ResetGuage");

                    }

                    chargebar.GetComponent<Image>().fillAmount = 0.0f; //에너지 모은 정보 초기화
                }
            }
        }
    }

    public IEnumerator ResetGuage()
    {
        yield return new WaitForSeconds(4.0f);
        bluebar.GetComponent<Image>().fillAmount = 0.0f;
        StopCoroutine("ResetGuage");
    }
}
