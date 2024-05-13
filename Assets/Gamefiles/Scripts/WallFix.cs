using UnityEngine;

public class WallFix : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Transform rightwall;
    [SerializeField] private Transform leftWall;
    // Start is called before the first frame update
    void Start()
    {
        float aspectRatio = (float)Screen.height / Screen.width;

        Camera mainCamera = Camera.main;
        float worldWidth = mainCamera.orthographicSize / aspectRatio;

        rightwall.transform.position = new Vector3(worldWidth + .5f, 0, 0);
        leftWall.transform.position = -rightwall.transform.position;

    }
}
