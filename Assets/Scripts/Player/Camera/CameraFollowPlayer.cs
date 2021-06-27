using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player;
    private PlayerHealthManager playerHealthManager;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;
 
    // A measure of magnitude for the shake. Tweak based on your preference
    private const float ShakeMagnitude = 0.1f;
 
    // A measure of how quickly the shake effect should evaporate
    private const float DampingSpeed = 2f;
    
    
    private void Start()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        player = playerHealthManager.transform;
        playerHealthManager.OnHitReceived += SetShakeDuration;
    }
    
    private void Update()
    {
        Shake();
        SetCameraPosition();
    }

    private void SetCameraPosition()
    {
        var cameraMoveDir = (player.position - transform.position).normalized;
        var distance = Vector2.Distance(player.position, transform.position);
        const float cameraMoveSpeed = 70f;
        if (distance > 0)
        {
            var newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
            var distanceAfterMoving = Vector2.Distance(newCameraPosition, player.position);

            transform.position = distanceAfterMoving > distance ? player.position : newCameraPosition;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void Shake()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = transform.position + Random.insideUnitSphere * ShakeMagnitude;
            shakeDuration -= Time.deltaTime * DampingSpeed;
        }
    }

    private void SetShakeDuration()
    {
        shakeDuration = 0.2f;
    }

}
