using UnityEngine;

public class MeshColliderChecker : MonoBehaviour
{
    [SerializeField] MeshCollider[] meshes;
    [SerializeField] Material magenta;
    [SerializeField] Material cyan;
    [SerializeField] bool areColliding;

    private void Update()
    {
        areColliding = false;

        foreach (var point in meshes[0].pointsInsideMesh)
        {
            if (meshes[1].CheckPointsAgainstAnotherMesh(point))
            {
                areColliding = true;
            }
        }

        if (areColliding)
        {
            meshes[0].GetComponent<MeshRenderer>().material = cyan;
            meshes[1].GetComponent<MeshRenderer>().material = cyan;
        }
        else
        {
            meshes[0].GetComponent<MeshRenderer>().material = magenta;
            meshes[1].GetComponent<MeshRenderer>().material = magenta;
        }
    }
}