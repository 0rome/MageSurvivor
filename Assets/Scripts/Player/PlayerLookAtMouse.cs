using UnityEngine;

public class PlayerLookAtMouse : MonoBehaviour
{
    private Camera mainCamera;
    void Awake()
    {
        mainCamera = Camera.main;
    }   
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
}
