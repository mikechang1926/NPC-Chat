using UnityEngine;

public class BoundaryCreator : MonoBehaviour
{
    public GameObject plane;
    public float boundaryHeight = 5f;
    public float boundaryThickness = 0.5f;

    void Start()
    {
        if (plane == null)
        {
            Debug.LogError("Please assign the Plane GameObject in the inspector!");
            return;
        }

        CreateBoundaries();
    }

    void CreateBoundaries()
    {
        Vector3 planeSize = plane.GetComponent<Renderer>().bounds.size;

        // Create parent object for better hierarchy organization
        GameObject boundaryParent = new GameObject("BoundaryWalls");
        boundaryParent.transform.parent = plane.transform; // Optional: parent to plane

        // Positions of boundary walls relative to plane's position
        Vector3[] positions = new Vector3[]
        {
            new Vector3(0, boundaryHeight / 2, planeSize.z / 2 + boundaryThickness / 2),    // Front wall
            new Vector3(0, boundaryHeight / 2, -planeSize.z / 2 - boundaryThickness / 2),   // Back wall
            new Vector3(planeSize.x / 2 + boundaryThickness / 2, boundaryHeight / 2, 0),    // Right wall
            new Vector3(-planeSize.x / 2 - boundaryThickness / 2, boundaryHeight / 2, 0)    // Left wall
        };

        // Scales of boundary walls
        Vector3[] scales = new Vector3[]
        {
            new Vector3(planeSize.x + boundaryThickness * 2, boundaryHeight, boundaryThickness), // Front/Back
            new Vector3(planeSize.x + boundaryThickness * 2, boundaryHeight, boundaryThickness), // Front/Back
            new Vector3(boundaryThickness, boundaryHeight, planeSize.z),                         // Right/Left
            new Vector3(boundaryThickness, boundaryHeight, planeSize.z)                          // Right/Left
        };

        for (int i = 0; i < positions.Length; i++)
        {
            GameObject wall = new GameObject("Wall_" + i);
            wall.transform.parent = boundaryParent.transform;
            wall.transform.position = plane.transform.position + positions[i];
            wall.transform.localScale = scales[i];

            BoxCollider boxCollider = wall.AddComponent<BoxCollider>();
            boxCollider.isTrigger = false;

            // No renderer needed, walls will be invisible by default (colliders only)
        }
    }
}
