using UnityEngine;

public class AchievementMedal : MonoBehaviour
{
    new Transform transform;
    [SerializeField] Material[] materials;

    void Awake()
    {
        transform = GetComponent<Transform>();
        GetComponent<MeshRenderer>().material = materials[0];
    }

    void Update()
    {
        transform.Rotate(Time.deltaTime * 90f * Vector3.up);
    }
}