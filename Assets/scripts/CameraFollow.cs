using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed;
    private float currentposx;
    private Vector3 velocity = Vector3.zero;

    public Transform player;

    public float aheadistance;
    public float cameraspeed;
    private float lookahead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentposx,transform.position.y,transform.position.z),ref velocity ,speed);

        transform.position = new Vector3(player.position.x + lookahead, player.position.y, player.position.z);
        lookahead = Mathf.Lerp(lookahead, (aheadistance * player.localScale.x), Time.deltaTime * cameraspeed);
    }

    public void movetowards(Transform _newspace)
    {
        currentposx = _newspace.position.x;
    }
}
