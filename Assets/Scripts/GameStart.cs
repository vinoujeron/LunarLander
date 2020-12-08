using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject startObject = null;
    [SerializeField] private GameObject gameInfo = null;

    [Header("Dependencies")]
    [SerializeField] private Surface surface = null;
    [SerializeField] private EndlessBackground endlessBackground = null;
    [SerializeField] private Vector2 menuBackgroundDelta = Vector2.zero;

    [Header("Player")]
    [SerializeField] private Vector3 startVelocity = Vector3.zero;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject menuPlayer = null;


    private void Update()
    {
        StartInputCheck();
        endlessBackground.MoveMenuBackground(menuBackgroundDelta);
    }

    private void StartInputCheck()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Are you really want to quit?
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Start animation
            startObject.SetActive(false);
            gameInfo.SetActive(true);

            surface.CreateSurface();

            menuPlayer.SetActive(false);
            player.SetActive(true);
            player.GetComponent<Rigidbody2D>().velocity = startVelocity;
            player.transform.position = startPosition;
        }
    }
}
