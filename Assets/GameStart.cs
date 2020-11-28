using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject startObject = null;
    [SerializeField] private GameObject gameInfo = null;

    [SerializeField] private Surface surface = null;
    [SerializeField] private GameObject player = null;

    private void Update()
    {
        StartInputCheck();
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
            player.SetActive(true);
        }
    }
}
