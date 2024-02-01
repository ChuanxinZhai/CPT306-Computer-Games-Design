using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMovement : MonoBehaviour
{
    // This is the movement direction
    public int moveUp = 1;
    // This is the sea height
    public float seaHeight = 0.5f;
    // This is the wave speed
    public float waveSpeed = 0.5f;

    void UpdateHeight() {
        if (this.transform.position.y > seaHeight)
            moveUp = -1;
        else if (this.transform.position.y < -seaHeight)
            moveUp = 1;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + moveUp * Time.deltaTime * waveSpeed, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeight();
    }
}
