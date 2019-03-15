using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateCtrl : FSM_Base {

    public enum eState 
    {
        None,
        Clear,
        Cloud,
        Heat,
        Rain,
        Snow,
        HeavyRain,
        HeavySnow,
        ThunderCloud,
        Drought,
        Fire,
        Chief
    }

    public eState currentState;
    public eState nextState;

    float fire_Tcnt = 30.0f;
    public int randIdx;
    public int clearToHeat;
    public int cloudToRain;
    public int cloudToSnow;
    public int cloudToThender;
    public int mapNum;
    public float weather_Sec = 15.0f;   // ����Ʈ ���� �� ��� �ð�

    public GameObject instantiate_weather;
    public GameObject instantiate_alarm;
    public GameObject[] weather_Instantiate;         //0.����, 1.��������, 2.���� ��, 3.���� ��, 4.���� �� 5. ���� �� 6.����, 7. ���κ���, 8. �ҹ湰 ,9. ����
    public Transform village_Point;               // 0.Desert, 1.Ground, 2.Mountain, 3.Port
    public Transform LandPoint;
    public GameObject[] fire;
    public GameObject[] alarm;
    private WeatherCtrl weatherCtrl;
    private Country_Effect country_Effect;
    Transform tr_Pos;
    int r, r2, r3;

    protected override string GetMethodNameFromCurrentState () 
    {
        return "State_" + currentState.ToString();
    }

    public void ChangeState (eState state) 
    {
        if (currentState != state) {
            currentState = state;
            NextState();
        }
    }

    public eState GetCurrentState () { return currentState; }

    private void Start () 
    {

        weatherCtrl = GameObject.Find("WeatherCtrl").GetComponent<WeatherCtrl>();
        country_Effect = GetComponent<Country_Effect>();
        tr_Pos = this.transform;
        ChangeState(eState.Clear);
    }

    void Update () 
    {

    }


    IEnumerator State_Clear () 
    {


        while (currentState == eState.Clear) {
            if (weatherCtrl.create_Weather[mapNum] == true) {
                weatherCtrl.create_Weather[mapNum] = false;
                Destroy(instantiate_weather);
                Destroy(instantiate_alarm);
                if (Random.Range(0, 100) <= clearToHeat) {
                    // ����� clearToHeat % Ȯ����

                    ChangeState(eState.Heat);

                } else {
                    // �������� clearToHeat % Ȯ����
                    ChangeState(eState.Cloud);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    IEnumerator State_Cloud ()                               //SUN�� �ݶ��̴��� ������ true�� ���� ����ǰ�
    {


        SoundMgr.inst.Play("Alarm", tr_Pos.position);
        instantiate_weather = Instantiate(weather_Instantiate[0], village_Point.position, Quaternion.identity);   //cloud����
        instantiate_alarm = Instantiate(alarm[0], new Vector3(village_Point.position.x,
                                                              village_Point.position.y + 30.0f,
                                                              village_Point.position.z), Quaternion.identity);
        yield return new WaitForSeconds(weather_Sec);


        randIdx = Random.Range(0, 100);
        if(currentState == eState.Cloud){
            if (randIdx >= cloudToRain && randIdx <= 39)   //0~39
            {
                ChangeState(eState.Rain);
            } else if (randIdx > cloudToSnow && randIdx <= 79) //40~79
              {
                ChangeState(eState.Snow);
            } else if (randIdx > cloudToThender && randIdx <= 99) // 80~99
              {
                ChangeState(eState.ThunderCloud);
            }
        }else{
            ChangeState(eState.Clear);
        }


        weatherCtrl.create_Weather[mapNum] = false;
    }

    IEnumerator State_Heat ()      //���� .������ ���� CLOUD �ױ�
    {
        while (currentState == eState.Heat) {
            SoundMgr.inst.Play("Sun", tr_Pos.position);
            SoundMgr.inst.Play("Alarm", tr_Pos.position);
            instantiate_weather = Instantiate(weather_Instantiate[6], village_Point.position, Quaternion.identity);
            instantiate_alarm = Instantiate(alarm[0], new Vector3(village_Point.position.x,
                                                                  village_Point.position.y + 30.0f,
                                                                  village_Point.position.z), Quaternion.identity);
            yield return new WaitForSeconds(weather_Sec);
            Destroy(instantiate_alarm);

            while (currentState == eState.Heat) {
               
                if (Random.Range(0, 100) > 50) {
                    ChangeState(eState.Drought);  //����

                } else {
                    ChangeState(eState.Fire);  //�� ȭ��
                }

            }

        }
    }

    IEnumerator State_Rain () {

        yield return null;

        SoundMgr.inst.Play("Rain", tr_Pos.position);
        Destroy(instantiate_alarm);

        while (currentState == eState.Rain) {
            instantiate_weather = Instantiate(weather_Instantiate[2], village_Point.position, Quaternion.identity);
            instantiate_alarm = Instantiate(alarm[0], new Vector3(village_Point.position.x,
                                                                  village_Point.position.y + 30.0f,
                                                                  village_Point.position.z), Quaternion.identity);
            yield return new WaitForSeconds(weather_Sec);
            Debug.Log(currentState);
            if (currentState == eState.Rain) {
                ChangeState(eState.HeavyRain);
            } else {
                ChangeState(eState.Clear);
            }

        }
    }

    IEnumerator State_Snow () {
        yield return null;

        Destroy(instantiate_alarm);

        while (currentState == eState.Snow) {
            instantiate_weather = Instantiate(weather_Instantiate[3], village_Point.position, Quaternion.identity);
            instantiate_alarm = Instantiate(alarm[0], new Vector3(village_Point.position.x,
                                                                  village_Point.position.y + 30.0f,
                                                                  village_Point.position.z), Quaternion.identity);
            yield return new WaitForSeconds(weather_Sec);
            Debug.Log(currentState);
        if (currentState == eState.Snow){
            ChangeState(eState.HeavySnow);
        }else {
            ChangeState(eState.Clear);
        }

        }
    }

    IEnumerator State_Drought ()            // ����!!! 
    {
        SoundMgr.inst.Stop("Sun");
        SoundMgr.inst.Play("Drought", tr_Pos.position);
        instantiate_weather = Instantiate(weather_Instantiate[8], LandPoint.position, Quaternion.identity);
        instantiate_alarm = Instantiate(alarm[1], new Vector3(village_Point.position.x,
                                                              village_Point.position.y + 30.0f,
                                                              village_Point.position.z), Quaternion.identity);

        yield return new WaitForSeconds(weather_Sec);
        Destroy(instantiate_weather);
        Destroy(instantiate_alarm);

        weatherCtrl.create_Weather[mapNum] = false;
        ChangeState(eState.Clear);
        yield return null;
    }

    IEnumerator State_Fire ()            // ���� �� !!! 
    {
        SoundMgr.inst.Stop("Sun");
        SoundMgr.inst.Play("E_Fire_idle", tr_Pos.position);

        instantiate_alarm = Instantiate(alarm[0], new Vector3(village_Point.position.x,
                                                      village_Point.position.y + 30.0f,
                                                      village_Point.position.z), Quaternion.identity);

        r = Random.Range(0, 4);
        r2 = Random.Range(0, 4);
        r3 = Random.Range(0, 4);
        fire[r].SetActive(true);
        fire[r2].SetActive(true);
        fire[r3].SetActive(true);

        while(true)
        {
            fire_Tcnt -= Time.deltaTime;

            if(fire_Tcnt <= 0)
            {
                Destroy(instantiate_alarm);
                fire[r].SetActive(false);
                fire[r2].SetActive(false);
                fire[r3].SetActive(false);
                weatherCtrl.create_Weather[mapNum] = false;
                ChangeState(eState.Clear);
                break;
            }
            yield return null;
        }

    }

    IEnumerator State_HeavyRain () {
        
        SoundMgr.inst.Play("Heavy_Rain", tr_Pos.position);
        Destroy(instantiate_alarm);

        instantiate_weather = Instantiate(weather_Instantiate[4], village_Point.position, Quaternion.identity);
        instantiate_alarm = Instantiate(alarm[1], new Vector3(village_Point.position.x,
                                                              village_Point.position.y + 30.0f,
                                                              village_Point.position.z), Quaternion.identity);


        yield return new WaitForSeconds(weather_Sec);
        weatherCtrl.create_Weather[mapNum] = false;
        Destroy(instantiate_alarm);

        ChangeState(eState.Clear);

    }

    IEnumerator State_HeavySnow () {
        
        SoundMgr.inst.Play("StomSnow", tr_Pos.position);
        Destroy(instantiate_alarm);

        instantiate_weather = Instantiate(weather_Instantiate[5], village_Point.position, Quaternion.identity);
        instantiate_alarm = Instantiate(alarm[1], new Vector3(village_Point.position.x,
                                                              village_Point.position.y + 30.0f,
                                                              village_Point.position.z), Quaternion.identity);

        yield return new WaitForSeconds(weather_Sec);
        weatherCtrl.create_Weather[mapNum] = false;
        Destroy(instantiate_alarm);

        ChangeState(eState.Clear);
    }

    IEnumerator State_ThunderCloud () {
        yield return new WaitForSeconds(weather_Sec);
        Destroy(instantiate_alarm);
        SoundMgr.inst.Play("Thunder_Cloud", tr_Pos.position);

        instantiate_weather = Instantiate(weather_Instantiate[1], village_Point.position, Quaternion.identity);
        instantiate_alarm = Instantiate(alarm[1], new Vector3(village_Point.position.x,
                                                              village_Point.position.y + 30.0f,
                                                              village_Point.position.z), Quaternion.identity);
        yield return new WaitForSeconds(weather_Sec);
        weatherCtrl.create_Weather[mapNum] = false;
        Destroy(instantiate_alarm);

        ChangeState(eState.Clear);

    }

}