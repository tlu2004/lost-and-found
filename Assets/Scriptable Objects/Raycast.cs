using UnityEngine;

public class BoxRaycastTest : MonoBehaviour
{
    public float distance = 100f;
    public LayerMask mask = ~0;

    public Transform player;      // drag player here
    public float moveSpeed = 50f;

    Vector3 target;
    bool moving = false;

    void Update()
    {
        HandleClick();
        MovePlayer();
    }

    void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("mouse click");
        if (Physics.Raycast(ray, out RaycastHit hit, distance, mask))
        {
            if (hit.collider is BoxCollider)
            {
                target = hit.point;
                moving = true;
            }
        }
    }

    void MovePlayer()
    {
        if (!moving || player == null) return;

        player.position = Vector3.MoveTowards(
            player.position,
            target,
            moveSpeed * Time.deltaTime
            
        );
        Debug.Log("movin");

        if (Vector3.Distance(player.position, target) < 0.05f)
            moving = false;
    }
}