using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow player")]
    public GameObject player;
    public float offsetX = 5;
    public float offsetY = 2;
    public float offsetSmoothing = 4;
    public float offsetSensitivity = 0.5f;

    [Header("Establishing shot")]
    [SerializeField] private float targetSize = 5;
    [SerializeField] private float establishingShotDuration = 2f;
    [SerializeField] private float establishingShotDelay = 1f;

    private Vector3 playerPosition;
    private Vector3 lastPosition;
    private Vector3 targetPosition;
    private float lastMoveDir;

    // Establishing shot
    private bool playerActive = false;
    private new Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        StartCoroutine(EstablishingShot());
    }

    // Update is called once per frame 
    // Sammenlign camerapos  
    void Update()
    {
        if (playerActive)
        {
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            targetPosition = playerPosition;
            targetPosition.y += offsetY;
            if (playerPosition.x > lastPosition.x + offsetSensitivity)
            {
                lastMoveDir = 1;
                lastPosition = playerPosition;
            }
            else if (playerPosition.x < lastPosition.x - offsetSensitivity)
            {
                lastMoveDir = -1;
                lastPosition = playerPosition;
            }
            targetPosition.x += offsetX * lastMoveDir;

            transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing * Time.deltaTime);
        }
    }

    public IEnumerator EstablishingShot()
    {
        yield return new WaitForSeconds(establishingShotDelay);

        float initialSize = camera.orthographicSize;
        Vector3 initialPosition = transform.position;

        float lerpFactor;
        float timeElapsed = 0;

        targetPosition = player.transform.position;
        targetPosition.z = transform.position.z;
        targetPosition.y += offsetY;
        while (timeElapsed < establishingShotDuration)
        {
            lerpFactor = timeElapsed / establishingShotDuration;
            timeElapsed += Time.deltaTime;
            camera.orthographicSize = Mathf.Lerp(initialSize, targetSize, lerpFactor);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, lerpFactor);
            yield return null;
        }
        playerActive = true;
    }
}
