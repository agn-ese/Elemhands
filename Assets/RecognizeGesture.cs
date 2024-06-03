using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizeGesture : MonoBehaviour
{
    public void GestureRecognized(GestureCompletionData data)
    {
        Debug.Log(data.gestureID + ", " + data.gestureName + ", " + data.similarity);
    }
}
