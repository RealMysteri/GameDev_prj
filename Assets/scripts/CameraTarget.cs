using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform PreviousTarget;
    public Transform NextTarget;
    public CameraFollow cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.movetowards(NextTarget);
            }
            else
            {
                cam.movetowards(PreviousTarget);
            }
        }
    }
}
