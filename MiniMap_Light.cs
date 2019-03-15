using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_Light : MonoBehaviour
{

    public GameObject vr_Camera;
    public HeadRaycast headRay;

    public int test;
    void Start()
    {
        vr_Camera = GameObject.Find("VRCamera");
        headRay = GameObject.Find("VRCamera").GetComponent<HeadRaycast>();

    }

    void Update()
    {
        Light_Rotation();
    }

    public void Light_Rotation()
    {
        //회전값 동기
        Vector3 cam_Pos = vr_Camera.transform.forward;
        float angle = Mathf.Atan2(cam_Pos.x, cam_Pos.z) * Mathf.Rad2Deg;

        switch (headRay.mypos)
        {
            case "Temple":
                this.transform.localRotation = Quaternion.Euler(0, angle, 0);
                break;
            case "Port":
                this.transform.localRotation = Quaternion.Euler(0, angle + 90, 0);
                break;
            case "Ground":
                this.transform.localRotation = Quaternion.Euler(0, angle + 290, 0);
                break;
            case "Mountain":
                this.transform.localRotation = Quaternion.Euler(0, angle + 383, 0);
                break;
            case "Desert":
                this.transform.localRotation = Quaternion.Euler(0, angle + 190, 0);
                break;


        }
    }
}