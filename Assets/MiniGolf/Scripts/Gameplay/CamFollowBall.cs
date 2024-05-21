using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowBall : MonoBehaviour
{
    private GameObject golfBall;
    // Start is called before the first frame update
    void Start()
    {
        golfBall = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = new Vector3(golfBall.transform.position.x, golfBall.transform.position.y + 2f, -10f);
        transform.position = pos;
    }

    //IEnumerator ShowLevel()
    //{

    //}
}
