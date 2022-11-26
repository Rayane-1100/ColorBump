using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour

{
    Transform player;
    [SerializeField] Slider DistanceSlider;

    float initDistance;
    // Start is called before the first frame update
    void Start()
    {
        // Find the Player in the scene
        player = GameObject.Find("Ball").transform;

        // Calculate the initial distance
        initDistance = transform.position.z - player.position.z ;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate Distance
        float currentDistance = transform.position.z - player.position.z;

        // Set the Slider value
        DistanceSlider.value = (initDistance - currentDistance) / initDistance;
    }
}
