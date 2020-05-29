using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBoarderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY =10f;
    public float maxY = 60f;
    public float minX = -100f;
    public float maxX = 20f;
    public float minZ = -50f;
    public float maxZ = 50f;
    
    void Update()
    {
        if (GameManager.IsGameOver)
        {
            this.enabled = false;
        }
        
        
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;
        if (!doMovement)
            return;
        if (Input.GetKey("w")|| Input.mousePosition.y >= Screen.height -panBoarderThickness )
        {
            transform.Translate(Vector3.forward*panSpeed*Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <=  panBoarderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;

    }
}
