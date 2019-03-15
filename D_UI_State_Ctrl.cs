using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D_UI_State_Ctrl : MonoBehaviour {

    private Image population_Var;
    private Image food_Var;
    private Image achieve_Var;

    public StateCtrl.eState currentState;
    StateCtrl stateCtrl;
    public Transform curr;

    public D_PF_System Pf_System;

    public float maxA = 1680.0f;
    public float maxP = 420.0f;
    public float maxF = 1260.0f;


    public float percentage_A;
    public float percentage_P;
    public float percentage_F;


    public Image[] up_Down_Icon;  // 0.population_Up, 1.Population_Down_Icon 2.Food_Up_Icon 
                                      // 3.Food_Down_Icon 4.Achieve_Up_Icon 5. Achieve_Down_Icon

    public Image[] weather_Image; // 0. Heat 1.Cloud 2. Rain 3.Snow 4.Drought 5. Cloud_thunder 6.Fire


    void Start () {
        //각각의 Var이미지 파일에 접근함
        population_Var = transform.Find("Population_Var").GetComponent<Image>();
        food_Var = transform.Find("Food_Var").GetComponent<Image>();
        achieve_Var = transform.Find("Achieve_Var").GetComponent<Image>();

        //업 다운 아이콘 State_UI오브젝트에 차일드 접근
        for (int i = 0; i < up_Down_Icon.Length; i++)
        {
            up_Down_Icon[i] = this.transform.GetChild(i).GetComponent<Image>();
        }


        stateCtrl = curr.gameObject.GetComponent<StateCtrl>();

    }


	void Update () {

        Var_FillAmount();   //Var그래프 수치별 변동 메소드
        UpdateWeatherIcon();
        Up_Down_Checking();

    }

    void Var_FillAmount()
    {
        percentage_A  = Mathf.InverseLerp(0, maxA, Pf_System.A);  // 최대값에서 최소값 정해주고 현재 P,F,A각각 값이 0~1사이의 비율중 어느정도 되는지 표현
        percentage_P  = Mathf.InverseLerp(0, maxP, Pf_System.P);
        percentage_F  = Mathf.InverseLerp(0, maxF,  Pf_System.F);

        achieve_Var.fillAmount = percentage_A;
        food_Var.fillAmount = percentage_F;
        population_Var.fillAmount = percentage_P;
    }

    void UpdateWeatherIcon () {
        if (currentState != stateCtrl.GetCurrentState()) {
            for (int i = 0; i < weather_Image.Length; i++) {
                weather_Image[i].enabled = false;
            }
        }
            switch (stateCtrl.GetCurrentState()) 
            {
            case StateCtrl.eState.Clear:
                break;
            case StateCtrl.eState.Heat:
                weather_Image[0].enabled = true;
                break;
            case StateCtrl.eState.Cloud:
                weather_Image[1].enabled = true; 
                break;
            case StateCtrl.eState.Rain:
                weather_Image[2].enabled = true;
                break;
            case StateCtrl.eState.HeavyRain:
                weather_Image[2].enabled = true;
                break;
            case StateCtrl.eState.Snow:
                weather_Image[3].enabled = true;
                break;
            case StateCtrl.eState.HeavySnow:
                weather_Image[3].enabled = true;
                break;
            case StateCtrl.eState.Drought:
                weather_Image[4].enabled = true;
                break;
            case StateCtrl.eState.ThunderCloud:
                weather_Image[5].enabled = true;
                break;
            case StateCtrl.eState.Fire:
                weather_Image[6].enabled = true;
                break;
            }
    
            currentState = stateCtrl.GetCurrentState();
    }

 
     void Up_Down_Checking ()    //날씨 up_down 확인    
     {

            if (currentState == StateCtrl.eState.Clear || currentState == StateCtrl.eState.None)
            {
                up_Down_Icon[1].enabled = false;    //p_down
                up_Down_Icon[3].enabled = false;    //f_down
                up_Down_Icon[5].enabled = false;    //a_down
                up_Down_Icon[0].enabled = true;    //p_up
                up_Down_Icon[2].enabled = true;    //f_up
                up_Down_Icon[4].enabled = true;    //a_up

            }
        switch (currentState) 
        {

        case StateCtrl.eState.Heat:
            for (int i = 0; i < up_Down_Icon.Length; i++) 
            {
                up_Down_Icon[i].enabled = false;
            }
            break;

        case StateCtrl.eState.Cloud:
            for (int i = 0; i < up_Down_Icon.Length; i++) 
            {
                up_Down_Icon[i].enabled = false;
            }
            break;

        case StateCtrl.eState.Rain:
            for (int i = 0; i < up_Down_Icon.Length; i++) 
            {
                up_Down_Icon[i].enabled = false;
            }
            break;

        case StateCtrl.eState.Snow:
            for (int i = 0; i < up_Down_Icon.Length; i++) 
            {
                up_Down_Icon[i].enabled = false;
            }
            break;

        case StateCtrl.eState.Drought:
            up_Down_Icon[0].enabled = false;
            up_Down_Icon[1].enabled = true;
            up_Down_Icon[2].enabled = false;
            up_Down_Icon[3].enabled = true;
            up_Down_Icon[4].enabled = false;
            up_Down_Icon[5].enabled = true;
            break;

        case StateCtrl.eState.HeavyRain:
            up_Down_Icon[0].enabled = false;
            up_Down_Icon[1].enabled = true;
            up_Down_Icon[2].enabled = false;
            up_Down_Icon[3].enabled = true;
            up_Down_Icon[4].enabled = false;
            up_Down_Icon[5].enabled = true;

            break;

        case StateCtrl.eState.HeavySnow:
            up_Down_Icon[0].enabled = false;
            up_Down_Icon[1].enabled = true;
            up_Down_Icon[2].enabled = false;
            up_Down_Icon[3].enabled = true;
            up_Down_Icon[4].enabled = false;
            up_Down_Icon[5].enabled = true;
            break;

        case StateCtrl.eState.ThunderCloud:
            up_Down_Icon[0].enabled = false;
            up_Down_Icon[1].enabled = true;
            up_Down_Icon[4].enabled = false;
            up_Down_Icon[5].enabled = true;
            break;

        case StateCtrl.eState.Chief:
            up_Down_Icon[0].enabled = false;
            up_Down_Icon[1].enabled = true;
            up_Down_Icon[2].enabled = false;
            up_Down_Icon[3].enabled = true;
            up_Down_Icon[4].enabled = false;
            up_Down_Icon[5].enabled = true;
            break;

        case StateCtrl.eState.Fire:
            up_Down_Icon[2].enabled = false;
            up_Down_Icon[3].enabled = true;
            up_Down_Icon[4].enabled = false;
            up_Down_Icon[5].enabled = true;
            break;

        default:
            break;
        }

     } 
}
