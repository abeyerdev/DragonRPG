using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Called after other updates thus object positions should be finalized.
    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
