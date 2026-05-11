using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    private enum Direction {  Left, Right };
    [SerializeField] private Direction direction;
    [SerializeField] private Material goodMat, badMat;
    private bool flagPassed = false;
    public static event GameManager.TimerEvent RacePenalty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControl.player != null &&
            PlayerControl.player.position.z < transform.position.z &&
            flagPassed == false)
        {
            Direction passingDirection = Direction.Right;
            if (PlayerControl.player.position.x < transform.position.x)
                passingDirection = Direction.Left;

            flagPassed = true;
            Debug.LogError("Player passed on: " + passingDirection);
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if(passingDirection == direction )
            {
                renderer.material = goodMat;
            }
            else
            {
                renderer.material = badMat;
                RacePenalty.Invoke();
            }

            if (passingDirection == direction)
            {
                Debug.LogError("passed on correct side");
            }
            else
            {
                Debug.LogError("passed on other side");
            }
        }
    }
}
