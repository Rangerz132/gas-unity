using UnityEngine;

public class ParabolaProjectile : Projectile
{
    [SerializeField] private float speed = 10f; // Constant speed
    [SerializeField] private float height;
    private Vector3 startPosition;
    private Vector3 controlPoint;
    private Vector3 endPosition;
    private float traveledDistance;
    private float totalDistance;

    protected override void Update()
    {
        base.Update();
        Move();
    }

    public override void Aim()
    {
        startPosition = transform.position;
        endPosition = targetPosition;
        // Calculate the midpoint and add height to it for the control point
        Vector3 midPoint = (startPosition + endPosition) / 2;
        controlPoint = new Vector3(midPoint.x, midPoint.y + height, midPoint.z);
        // Calculate the total distance along the curve
        totalDistance = EstimateArcLength();
        traveledDistance = 0;
    }

    public override void Move()
    {
        // Calculate the distance to cover this frame
        float distanceThisFrame = speed * Time.deltaTime;
        traveledDistance += distanceThisFrame;

        // Calculate the new position using Bezier curve
        float t = Mathf.Clamp01(traveledDistance / totalDistance);

        // Quadratic Bezier curve formula
        Vector3 newPos = CalculateBezierPoint(t);
        transform.position = newPos;

        // Optional: rotate to face the direction of movement
        Vector3 direction = (newPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Destroy the projectile when it reaches the end
        if (t >= 1)
        {
            Destroy(gameObject);
        }
    }

    private Vector3 CalculateBezierPoint(float t)
    {
        return Mathf.Pow(1 - t, 2) * startPosition +
               2 * (1 - t) * t * controlPoint +
               Mathf.Pow(t, 2) * endPosition;
    }

    private float EstimateArcLength()
    {
        int segments = 20;
        float arcLength = 0;
        Vector3 prevPoint = startPosition;

        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 point = CalculateBezierPoint(t);
            arcLength += Vector3.Distance(prevPoint, point);
            prevPoint = point;
        }

        return arcLength;
    }
}