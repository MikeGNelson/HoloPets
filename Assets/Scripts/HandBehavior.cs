using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandBehavior : MonoBehaviour//IMixedRealityHandJointHandler
{
    public Handedness myHandedness = Handedness.Right;
    public Handedness leftHandedness = Handedness.Left;

    public List<HandEvents> indexHandEvents = new List<HandEvents>();
    public List<HandEvents> middleHandEvents = new List<HandEvents>();
    public List<HandEvents> ringHandEvents = new List<HandEvents>();
    public List<HandEvents> palmHandEvents = new List<HandEvents>();

    public List<HandEvents> l_indexHandEvents = new List<HandEvents>();
    public List<HandEvents> l_middleHandEvents = new List<HandEvents>();
    public List<HandEvents> l_ringHandEvents = new List<HandEvents>();
    public List<HandEvents> l_palmHandEvents = new List<HandEvents>();

    public int maxSize = 25;

    public SpeechCommands speechCommands;

    public GameObject indexPointer;
    public GameObject middlePointer;
    public GameObject ringPointer;
    public GameObject palmPointer;

    public GameObject indexPointerL;
    public GameObject middlePointerL;
    public GameObject ringPointerL;
    public GameObject palmPointerL;

    public TextMeshProUGUI indexText;
    public TextMeshProUGUI ringText;

    public TextMeshProUGUI indexTextL;
    public TextMeshProUGUI ringTextL;

    float lastCommand = 0f;
    float lastCommand2 = 0f;
    float delay = 1f;

    public struct HandEvents
    {
        public Vector3 position;
        public float time;

        public HandEvents(Vector3 _position, float _time)
        {
            this.position = _position;
            this.time = _time;
        }
    }

    private void Update()
    {

        //Get average distance to palm of first
        //float distIndex = Vector3.Distance(indexHandEvents[0].position, palmHandEvents[0].position);
        //float distMid = Vector3.Distance(middleHandEvents[0].position, palmHandEvents[0].position);
        //float distRing = Vector3.Distance(ringHandEvents[0].position, palmHandEvents[0].position);

        //float avgFirst = (distIndex + distMid + distRing) / 3f;

        //Get average distance of palm of last
        //float dist2Index = Vector3.Distance(indexPointer.transform.position, palmPointer.transform.position);
        //float dist2Mid = Vector3.Distance(middlePointer.transform.position, palmPointer.transform.position);
        //float dist2Ring = Vector3.Distance(ringPointer.transform.position, palmPointer.transform.position);

        //float avgSecond = (dist2Index + dist2Mid + dist2Ring) / 3f;

        //indexText.text = (Mathf.Round(avgSecond * 1000f) / 1000).ToString();
        ////ringText.text = (Mathf.Round(avgFirst * 1000f) / 1000).ToString();
        //var indexRenderer = indexPointer.GetComponent<Renderer>();
        //if (avgSecond >= 0.05f)
        //{
        //    Debug.Log("Bigger");
        //    indexRenderer.material.SetColor("_Color", new Color(1, 1, 1));
        //}

        //else
        //{
        //    Debug.Log("Smaller");
        //    //var indexRenderer = indexPointer.GetComponent<Renderer>();
        //    indexRenderer.material.SetColor("_Color", new Color(0, 0, 0));
        //}

        //if (avgSecond <= .02 && avgFirst > .03)
        //{
        //    speechCommands.ChangeColor();
        //    //var indexRenderer = indexPointer.GetComponent<Renderer>();
        //    //indexRenderer.material.SetColor("_Color", Color.blue);
        //}
        //else
        //{
        //    //var indexRenderer = indexPointer.GetComponent<Renderer>();
        //    //indexRenderer.material.SetColor("_Color", Color.red);
        //}
    }


    void RightHand()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, myHandedness, out MixedRealityPose pose))
        {
            // ... 

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, myHandedness, out MixedRealityPose midPose))
            {
                // ...

                if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, myHandedness, out MixedRealityPose pointerPose))
                {
                    // ...

                    if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, myHandedness, out MixedRealityPose palmPose))
                    {
                        
                        HandEvents handEvent = new HandEvents(pose.Position, Time.time);
                        indexHandEvents.Add(handEvent);

                        if (indexHandEvents.Count >= maxSize)
                        {
                            indexHandEvents.RemoveAt(0);
                        }

                        HandEvents midHandEvent = new HandEvents(midPose.Position, Time.time);

                        middleHandEvents.Add(midHandEvent);

                        if (middleHandEvents.Count >= maxSize)
                        {
                            middleHandEvents.RemoveAt(0);
                        }

                        HandEvents pointerHandEvent = new HandEvents(pointerPose.Position, Time.time);

                        ringHandEvents.Add(pointerHandEvent);

                        if (ringHandEvents.Count >= maxSize)
                        {
                            ringHandEvents.RemoveAt(0);
                        }

                        HandEvents palmHandEvent = new HandEvents(palmPose.Position, Time.time);

                        palmHandEvents.Add(palmHandEvent);

                        if (palmHandEvents.Count >= maxSize)
                        {
                            palmHandEvents.RemoveAt(0);
                        }

                                        

                        indexPointer.transform.position = pose.Position;
                        middlePointer.transform.position = midPose.Position;
                        ringPointer.transform.position = pointerPose.Position;
                        palmPointer.transform.position = palmPose.Position;

                                        

                        //Get average distance to palm of first (right)
                        float distIndexPalmR = Vector3.Distance(indexHandEvents[0].position, palmHandEvents[0].position);
                        float distMidPalmR = Vector3.Distance(middleHandEvents[0].position, palmHandEvents[0].position);
                        float distRingPalmR = Vector3.Distance(ringHandEvents[0].position, palmHandEvents[0].position);

                        float avgFirst = (distIndexPalmR + distMidPalmR + distRingPalmR) / 3f;

                        //Get average distance of palm of last (right)
                        float dist2IndexPalmR = Vector3.Distance(indexHandEvents[indexHandEvents.Count - 1].position, palmHandEvents[palmHandEvents.Count - 1].position);
                        float dist2MidPalmR = Vector3.Distance(middleHandEvents[middleHandEvents.Count - 1].position, palmHandEvents[palmHandEvents.Count - 1].position);
                        float dist2RingPalmR = Vector3.Distance(ringHandEvents[ringHandEvents.Count - 1].position, palmHandEvents[palmHandEvents.Count - 1].position);

                        float avgSecond = (dist2IndexPalmR + dist2MidPalmR + dist2RingPalmR) / 3f;

                                        
                        indexText.text = (Mathf.Round(avgSecond * 1000f) / 1000).ToString();
                        ringText.text = (Mathf.Round(avgFirst * 1000f) / 1000).ToString();

                                        

                        var indexRenderer = indexPointer.GetComponent<Renderer>();
                        var middleRenderer = middlePointer.GetComponent<Renderer>();
                        var ringRenderer = ringPointer.GetComponent<Renderer>();
                        var palmRenderer = palmPointer.GetComponent<Renderer>();

                                       
                                        

                        //Right hand check if hand becomes a fist
                        if (avgSecond <= avgFirst / 1.7 && avgFirst >= .03)
                        {
                            float currTime = Time.time;
                            if (currTime - lastCommand > delay)
                            {
                                speechCommands.Return();
                            }
                            lastCommand = currTime;

                            //var indexRenderer = indexPointer.GetComponent<Renderer>();
                            indexRenderer.material.SetColor("_Color", Color.white);
                            middleRenderer.material.SetColor("_Color", Color.white);
                            ringRenderer.material.SetColor("_Color", Color.white);
                            palmRenderer.material.SetColor("_Color", Color.white);
                        }
                        else
                        {



                            float currTime = Time.time;
                            if (currTime - lastCommand > delay)
                            {
                                //var indexRenderer = indexPointer.GetComponent<Renderer>();
                                indexRenderer.material.SetColor("_Color", Color.black);
                                middleRenderer.material.SetColor("_Color", Color.black);
                                ringRenderer.material.SetColor("_Color", Color.black);
                                palmRenderer.material.SetColor("_Color", Color.black);
                            }




                        }

                                      


                                    
                    }
                }
            }
        }
    }

    void LeftHand()
    {
        
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, myHandedness, out MixedRealityPose palmPose))
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, leftHandedness, out MixedRealityPose poseL))
            {
                // ... 

                if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, leftHandedness, out MixedRealityPose midPoseL))
                {
                    // ...

                    if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, leftHandedness, out MixedRealityPose pointerPoseL))
                    {
                        // ...

                        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, leftHandedness, out MixedRealityPose palmPoseL))
                        {
                                        

                                        

                            HandEvents l_handEvent = new HandEvents(poseL.Position, Time.time);
                            l_indexHandEvents.Add(l_handEvent);

                            if (l_indexHandEvents.Count >= maxSize)
                            {
                                l_indexHandEvents.RemoveAt(0);
                            }

                            HandEvents l_midHandEvent = new HandEvents(midPoseL.Position, Time.time);

                            l_middleHandEvents.Add(l_midHandEvent);

                            if (l_middleHandEvents.Count >= maxSize)
                            {
                                l_middleHandEvents.RemoveAt(0);
                            }

                            HandEvents l_pointerHandEvent = new HandEvents(pointerPoseL.Position, Time.time);

                            l_ringHandEvents.Add(l_pointerHandEvent);

                            if (l_ringHandEvents.Count >= maxSize)
                            {
                                l_ringHandEvents.RemoveAt(0);
                            }

                            HandEvents l_palmHandEvent = new HandEvents(palmPoseL.Position, Time.time);

                            l_palmHandEvents.Add(l_palmHandEvent);

                            if (l_palmHandEvents.Count >= maxSize)
                            {
                                l_palmHandEvents.RemoveAt(0);
                            }

                                        
                            palmPointer.transform.position = palmPose.Position;

                            indexPointerL.transform.position = poseL.Position;
                            middlePointerL.transform.position = midPoseL.Position;
                            ringPointerL.transform.position = pointerPoseL.Position;
                            palmPointerL.transform.position = palmPoseL.Position;

                                       
                            //Get distance from palm to palm
                            float palmToPalm = Vector3.Distance(palmHandEvents[0].position, l_palmHandEvents[0].position);
                            float palmToPalm2 = Vector3.Distance(palmHandEvents[palmHandEvents.Count - 1].position, l_palmHandEvents[l_palmHandEvents.Count - 1].position);

                                        
                            indexTextL.text = (Mathf.Round(palmToPalm2 * 1000f) / 1000).ToString();
                            ringTextL.text = (Mathf.Round(palmToPalm * 1000f) / 1000).ToString();


                            var indexRendererL = indexPointerL.GetComponent<Renderer>();
                            var middleRendererL = middlePointerL.GetComponent<Renderer>();
                            var ringRendererL = ringPointerL.GetComponent<Renderer>();
                            var palmRendererL = palmPointerL.GetComponent<Renderer>();
                                        

                            //Hands comming together
                            if (palmToPalm <= .05f && palmToPalm2 <= 0.05f)
                            {
                                float currTime = Time.time;
                                if (currTime - lastCommand2 > delay)
                                {
                                    speechCommands.ChangeColor();
                                }
                                lastCommand2 = currTime;

                                indexRendererL.material.SetColor("_Color", Color.red);
                                middleRendererL.material.SetColor("_Color", Color.red);
                                ringRendererL.material.SetColor("_Color", Color.red);
                                palmRendererL.material.SetColor("_Color", Color.red);
                            }

                            else
                            {
                                float currTime = Time.time;
                                if (currTime - lastCommand2 > delay)
                                {
                                    indexRendererL.material.SetColor("_Color", Color.blue);
                                    middleRendererL.material.SetColor("_Color", Color.blue);
                                    ringRendererL.material.SetColor("_Color", Color.blue);
                                    palmRendererL.material.SetColor("_Color", Color.blue);
                                }

                            }


                        }
                    }
                }
            }
                   
        }
    }
    //void IMixedRealityHandJointHandler.OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    void FixedUpdate()
    {

        RightHand();
        LeftHand();
        

    }
}

