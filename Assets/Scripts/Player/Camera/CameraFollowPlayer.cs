using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player;
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    private void Update()
    {
        var cameraMoveDir = (player.position - transform.position).normalized;
        var distance = Vector2.Distance(player.position,transform.position);
        const float cameraMoveSpeed = 30f;

        if (distance > 0)
        {
            var newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
            var distanceAfterMoving = Vector2.Distance(newCameraPosition, player.position);

            transform.position = distanceAfterMoving > distance ? player.position : newCameraPosition;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

    }

}
