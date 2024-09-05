using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // These two cameras are used to demonstrate 3d effect
    // (ortho is Main Camera - NOT Main Camera Copy, which is used just for calculations)
    public Camera orthoCamera; 
    public Camera perspCamera;

    // Start is called before the first frame update
    void Start()
    {
        orthoCamera.enabled = true;
        perspCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        Debug.Log("Button pressed");
        orthoCamera.enabled = !orthoCamera.enabled;
        perspCamera.enabled = !perspCamera.enabled;
        //Camera.main = orthoCamera;
        // Camera.main.orthographic = !Camera.main.orthographic;
    }
}
