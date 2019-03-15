using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRaycast : MonoBehaviour
{



    private Ray ray;
    private RaycastHit hit;

    public Transform tr;
    public Transform vrCam;


    public Transform center;
    public Transform port;
    public Transform ground;
    public Transform desert;
    public Transform mountain;

    public Transform ui;
    public Transform[] ui_Points;             //0.center 1.port 2.ground 3.desert 4.mountain
    public Transform other_Point;

    AudioSource godMove;

    public string mypos = "Temple";

    UiCenterMovement uiCenterMovement;
    void Start()
    {
        vrCam = this.transform;

        uiCenterMovement = GameObject.Find("UI").GetComponent<UiCenterMovement>();
        godMove = this.GetComponent<AudioSource>();
    }

    public void HeadRayCast_Find()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(tr.position, forward, Color.green);
        if (Physics.Raycast(tr.position, forward, out hit, 1 << 11))
        {
            godMove.Play();   //이동 사운드 재생
            if (hit.collider.CompareTag("TEMPLE")) 
            {
                mypos = "Temple";
                uiCenterMovement.centerMovement = true;  //center에 위치한 UI 활성화

                tr.position = center.localPosition;
                ui.position = new Vector3(0, 0, 0);
                tr.rotation = new Quaternion(0, 0, 0, 0);
                ui.transform.parent = vrCam;              

                uiCenterMovement.Center_Ui_Active();      
            }
            else if (hit.collider.CompareTag("PORT"))
            {
                mypos = "Port";
                uiCenterMovement.centerMovement = false;
                tr.position = port.localPosition;

                other_Point.transform.parent = ui.transform;

                ui.transform.parent = ui_Points[1].transform;
                ui.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag("GROUND"))
            {
                mypos = "Ground";
                uiCenterMovement.centerMovement = false;
                tr.position = ground.localPosition;
                other_Point.transform.parent = ui.transform;

                ui.transform.parent = ui_Points[2].transform;
                ui.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag("MOUNTAIN"))
            {
                mypos = "Mountain";
                uiCenterMovement.centerMovement = false;
                tr.position = mountain.localPosition;
                other_Point.transform.parent = ui.transform;

                ui.transform.parent = ui_Points[4].transform;
                ui.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (hit.collider.CompareTag("DESERT"))
            {
                mypos = "Desert";
                uiCenterMovement.centerMovement = false;
                tr.position = desert.localPosition;
                other_Point.transform.parent = ui.transform;

                ui.transform.parent = ui_Points[3].transform;
                ui.transform.GetChild(0).gameObject.SetActive(true);
            }

            tr.LookAt(center.position);
            ui.transform.localPosition = Vector3.zero;
            ui.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}