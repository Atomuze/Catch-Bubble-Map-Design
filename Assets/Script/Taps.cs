using UnityEngine;

public class Taps : MonoBehaviour
{
    private int frameCount = 0;
    void Update()
    {
        Vector2 pos = transform.position;
        float distanceSquare = (pos.x * pos.x + pos.y * pos.y);

        frameCount++;
        if (distanceSquare > 0)
        {
            Vector2 runDirection = pos / Mathf.Sqrt(distanceSquare) * 5f / ScenceSystem.speed;
            transform.position = pos - runDirection;
        }

        if (frameCount > ScenceSystem.speed)
        {
            destory();
        }
    }

    public void destory()
    {
        Destroy(gameObject);
    }
}
