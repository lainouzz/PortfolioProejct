using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class crouchPP : MonoBehaviour
{
    public PostProcessVolume crouchVolume;

    public PlayerScriptableObject playerData;
    // Start is called before the first frame update
    void Start()
    {
        crouchVolume.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        crouchVolume.enabled = playerData.isCrouching;
    }
}
