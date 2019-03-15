using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//손의 애니메이션과 Raycast의 관리를 위한 스크립트 손에 넣어주어야한다.
public class HandController : MonoBehaviour
{

    public Animator anim;
    private RaycastController rightRaycast;
    public Transform throwPoint;
    public Transform throwPoint2;
    public Transform throwPoint3;

    // Use this for initialization
    void Start ()
    {
        rightRaycast = GetComponent<RaycastController>();
    }

    //지시 상태로 Raycast를 켜준다.
    public void ToFinger()
    {
        anim.SetInteger("State", 1);
        if (rightRaycast != null)
            rightRaycast.LaserOn();
    }

    //일반 상태로 Raycast를 꺼준다.
    public void ToNormal()
    {
        anim.SetInteger("State", 0);
        if(rightRaycast!=null)
            rightRaycast.LaserOff();
    }

    //구를 소지한 상태로 Raycast를 꺼준다.
    public void FingerToGrabbig()
    {
        anim.SetInteger("State", 2);
        rightRaycast.LaserOff();
    }

    //번개를 소지한 상태로 Raycast를 꺼준다.
    public void FingerToGrabver()
    {
        anim.SetInteger("State", 3);
        rightRaycast.LaserOff();
    }
}
