using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform thePlayer;

    private Vector3 lastPlayerPosition;
    private Vector3 velocity = Vector3.zero;
    
    private float distanceToMove;
    private float smoothTime = .2f;

    void Start()
    {
        lastPlayerPosition = thePlayer.position;
    }

    private void LateUpdate()
    {
        distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        lastPlayerPosition = thePlayer.transform.position;

        //distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
        //Vector3 targetPosition = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //lastPlayerPosition = thePlayer.transform.position;
    }
}
