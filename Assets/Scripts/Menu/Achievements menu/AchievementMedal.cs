using UnityEngine;

public class AchievementMedal : MonoBehaviour
{
    new Transform transform;

    void Awake()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        transform.Rotate(Time.deltaTime * 90f * Vector3.up);
    }
}