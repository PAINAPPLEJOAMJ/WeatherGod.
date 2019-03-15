using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class ViveCtrl : MonoBehaviour {

    private SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean triggerAction;
    public GameObject fire;

    Transform tr;
    RaycastHit hitInfo;

    void Start () {

        fire.SetActive(false);
        tr = GetComponent<Transform>();
    }

    void Update () {

        if (Physics.Raycast(tr.position, tr.forward, out hitInfo, Mathf.Infinity, 1 << 15)) {

            fire.SetActive(true);

            if (triggerAction.GetState(handType)) 
                {
                if (CompareTag("START")) {

                    SceneManager.LoadScene("Play");
                } 
                else if (CompareTag("QUIT")) {

                }
            }
        }
        // 클릭을 하면 a가 증가한다
        //cTime = 0;
        /*
        if (cTime >= 3) {
            a = 0;
            clor = new Color(0, 0, 0, a);

            if (a >= 255) {
            }
            */
    }
}