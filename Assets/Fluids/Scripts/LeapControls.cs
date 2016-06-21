using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
namespace Leap.Unity {

    public class LeapControls : MonoBehaviour {
        Vector3 position;
        bool isClosedFist;
        LeapProvider provider;
        Fluids newFluids;
    
	    // Use this for initialization
	    void Start () 
        {
            provider = FindObjectOfType<LeapProvider>() as LeapProvider;
            newFluids = FindObjectOfType<Fluids>() as Fluids;
            position = new Vector3(0, 0, 0);
            isClosedFist = false;
	    }
	
	    // Update is called once per frame
	    void Update () 
        {
            Frame frame = provider.CurrentFrame;
            foreach (Hand hand in frame.Hands)
            {
                if (hand.IsLeft)
                {
                    Vector3 position = hand.PalmPosition.ToVector3() +
                                         hand.PalmNormal.ToVector3() *
                                        (transform.localScale.y * .5f + .02f);
                    //Vector3 rotation = hand.Basis.rotation.ToQuaternion();
                    position.x = position.x + 0.5f;
                    position.y = position.y + 0.7f;
                    newFluids.impulsePosition = new Vector2(position.x, position.y);
                    //newFluids.impulseSize = hand.PinchStrength * 0.1f;
                    //Debug.Log(pobstacleSizeosition);

                    //Debug.Log("Left hand " + hand.PinchStrength);
                    
                }

                if(hand.IsRight)
                {
                    Vector3 position = hand.Direction.ToVector3() +
                                         hand.PalmNormal.ToVector3() *
                                        (transform.localScale.y * .5f + .02f);
                    //Vector3 rotation = hand.Basis.rotation.ToQuaternion();
                    position.x = position.x + 0.5f;
                    position.y = position.y + 0.7f;
                    newFluids.direction = new Vector2(position.x, position.y);
                    //newFluids.obstaclePosition = new Vector2(position.x, position.y);
                    //newFluids.obstacleSize = hand.PinchStrength * 0.1f;
                    //Debug.Log(position);
                    //Debug.Log("Right hand " + hand.PinchStrength);   
                }
            }

	    }
    }
}