using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class HandJoints : MonoBehaviour
{
    public GameObject fingerObject;
    public GameObject wristObject;

    public float dist = 0f;


    Vector3[] tipsL = new Vector3[5];
    Vector3 wristL = Vector3.zero;
    Vector3[] tipsR = new Vector3[5];
    Vector3 wristR = Vector3.zero;

    List<GameObject> fingerObjectsL = new List<GameObject>();
    List<GameObject> fingerObjectsR = new List<GameObject>();

    GameObject wristObjectL;
    GameObject wristObjectR;

    public GameObject chasePoint;
    public GameObject chaseLine;
    private Vector3 fingerTip, fingerMiddle;

    MixedRealityPose pose;



    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(fingerObject, this.transform);
            fingerObjectsL.Add(obj);
            GameObject obj2 = Instantiate(fingerObject, this.transform);
            fingerObjectsR.Add(obj2);

        }
        wristObjectL = Instantiate(wristObject, this.transform);
        wristObjectR = Instantiate(wristObject, this.transform);
    }

    void Update()
    {
        //only render if hand is tracked
        for (int i = 0; i < 5; i++)
        {
            fingerObjectsL[i].GetComponent<Renderer>().enabled = false;
            fingerObjectsR[i].GetComponent<Renderer>().enabled = false;

        }
        wristObjectL.GetComponent<Renderer>().enabled = false;
        wristObjectR.GetComponent<Renderer>().enabled = false;


        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose))
        {
            tipsL[0] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))
        {
            tipsL[1] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out pose))
        {
            tipsL[2] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out pose))
        {
            tipsL[3] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out pose))
        {
            tipsL[4] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left, out pose))
        {
            wristL = pose.Position;
        }



        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
        {
            tipsR[0] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexMiddleJoint, Handedness.Right, out pose))
        {
            fingerMiddle = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
        {
            tipsR[1] = pose.Position;
            fingerTip = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
        {
            tipsR[2] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
        {
            tipsR[3] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
        {
            tipsR[4] = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Right, out pose))
        {
            wristR = pose.Position;
        }

        for (int i = 0; i < 5; i++)
        {

            fingerObjectsL[i].transform.position = tipsL[i] + (tipsL[i] - wristL) * dist;
            fingerObjectsR[i].transform.position = tipsR[i] + (tipsR[i] - wristR) * dist;
            wristObjectL.transform.position = wristL;
            wristObjectR.transform.position = wristR;
        }

    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(fingerTip, (fingerTip - fingerMiddle).normalized, out hit))
        {
            chasePoint.transform.position = hit.point;
            chaseLine.GetComponent<LineRenderer>().SetPosition(0, fingerTip);
            chaseLine.GetComponent<LineRenderer>().SetPosition(1, hit.point);
        }
    }
}
