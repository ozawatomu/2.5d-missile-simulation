using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayRocketVelocity : MonoBehaviour
{
    public Rigidbody rocket;
    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        double rocketVelocity = rocket.velocity.magnitude;
        txt.text = string.Format("Velocity: {0:N0} m/s", rocketVelocity);
    }
}
