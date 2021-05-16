using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private TouchControl touchControls;
    private PhotonView PV;
    private int cellSize;
    private int offset;

    public void Awake()
    {
        PV = GetComponent<PhotonView>();
        //cellSize = Screen.width / cellWidthCount;
        cellSize = 15;
        offset = cellSize / 2;
        touchControls = new TouchControl();
    }

    public void OnEnable()
    {
        touchControls.Enable();
    }

    public void OnDisable()
    {
        touchControls.Disable();
    }

    public void Update()
    {
        if (PV.IsMine /*GameStateController.Current == GameState.MY_TURN*/)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 touchPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        Debug.Log("touch at " + touchPosition);
        if (touchPosition.x != 0 && touchPosition.y != 0)
        {
            Vector3 screenCoordinates = new Vector3((int)(touchPosition.x / cellSize) * cellSize + offset,
                (int)(touchPosition.y / cellSize) * cellSize + offset,
                Camera.main.nearClipPlane);
            Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(screenCoordinates);
            worldCoordinates.z = 0;
            Debug.Log("w touch at " + worldCoordinates);
            transform.position = worldCoordinates;
        }
    }
}
