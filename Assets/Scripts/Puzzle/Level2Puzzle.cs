using UnityEngine;

public class Level2Puzzle : MonoBehaviour
{
    public GameObject wall;
    public float lowerSpeed = 2f;
    public float targetYPosition = -3f;

    private bool isTriggered = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pendant"))
        {
            isTriggered = true;
        }
    }

    void Update()
    {
        if (isTriggered && wall.transform.position.y > targetYPosition)
        {
            Vector3 newPosition = wall.transform.position;
            newPosition.y = Mathf.Max(newPosition.y - lowerSpeed * Time.deltaTime, targetYPosition);
            wall.transform.position = newPosition;
        }
    }
}
