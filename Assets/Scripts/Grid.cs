using UnityEngine;
using CustomMath;

public class Grid : MonoBehaviour
{
    [SerializeField] public bool isGridActive = false;

    [SerializeField] public static int Size = 10;
    [SerializeField] public static float Delta = 1f;
    [SerializeField] public static Vec3[,,] grid = new Vec3[Size, Size, Size];

    private void Start()
    {
        isGridActive = true;

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {
                    grid[x, y, z] = new Vec3(x, y, z) * Delta;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!isGridActive)
        {
            return;
        }

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {
                    Gizmos.DrawSphere(new Vec3(x, y, z) * Delta, 0.1f);
                }
            }
        }
    }
}