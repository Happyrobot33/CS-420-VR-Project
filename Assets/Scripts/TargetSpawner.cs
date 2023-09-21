using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//lists
using System.Linq;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject worldsPosition;
    public float spawnInterval = 1.0f;
    public int maxActiveTargets = 10;
    //list of targets
    List<GameObject> targets = new List<GameObject>();
    public float innerRadius = 5;
    public float outerRadius = 10;
    public float height = 5;
    // Start is called before the first frame update
    void Start()
    {
        //start the main coroutine
        StartCoroutine(SpawnTargets());
    }

    //main coroutine
    IEnumerator SpawnTargets()
    {
        //loop forever
        while (true)
        {
            //wait for the spawn interval
            yield return new WaitForSeconds(spawnInterval);
            //double check all targets exist, and remove any that don't
            targets = targets.Where(x => x != null).ToList();
            //generate a random spawn
            GenerateRandomSpawn();
        }
    }

    private void GenerateRandomSpawn()
    {
        //make sure there are less than the max active targets
        if (targets.Count >= maxActiveTargets)
        {
            //return for now
            return;
        }
        //get a random point in the spawn area
        Vector2 RandomCirclePoint = RandomInOutline(innerRadius, outerRadius);
        Vector3 randomPoint = new Vector3(RandomCirclePoint.x, 0, RandomCirclePoint.y);
        //get a random height
        float randomHeight = Random.Range(0, height);
        //set the random point's height to the random height
        randomPoint.y = randomHeight;
        //add the spawner's position to the random point
        randomPoint += transform.position;
        //create a new game object at the random point
        GameObject newTarget = Instantiate(targetPrefab, randomPoint, Quaternion.identity);
        //set the targets rotation to look at the spawner
        newTarget.transform.LookAt(transform);
        //set the target's parent to this object
        newTarget.transform.parent = worldsPosition.transform;
        //add the target to the targets array
        targets.Add(newTarget);
    }

    private Vector2 RandomInOutline(float innerRadius, float outerRadius)
    {
        //get a random point in the unit circle
        Vector2 randomPoint = Random.insideUnitCircle;
        //get the magnitude of the random point
        float magnitude = randomPoint.magnitude;
        //lerp the magnitude between the inner and outer radius
        magnitude = Mathf.Lerp(innerRadius, outerRadius, magnitude);
        //normalize the random point
        randomPoint.Normalize();
        //multiply the random point by the magnitude
        randomPoint *= magnitude;
        //return the random point
        return randomPoint;
    }

#if UNITY_EDITOR
    //gizmo code
    private void OnDrawGizmos()
    {
        //set transform to identity so we can draw the gizmos in the right place
        Gizmos.matrix = Matrix4x4.identity;
        //draw cylinders for the spawn area
        Gizmos.color = Color.red;
        DrawCylinder(transform, innerRadius, height);
        Gizmos.color = Color.green;
        DrawCylinder(transform, outerRadius, height);
    }

    private void DrawCylinder(Transform transform, float radius, float height)
    {
        int numLines = 24;
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        //rotate by 90 degrees so the cylinder is standing up
        rotation *= Quaternion.Euler(-90, 0, 0);
        //draw a cylinders top and bottom circle faces using lines
        for (int i = 0; i < 360; i += 360 / numLines)
        {
            //get the x and y positions of the current angle
            float x = Mathf.Cos(i * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(i * Mathf.Deg2Rad) * radius;

            //augment by rotation
            Vector3 rotatedPoint = rotation * new Vector3(x, y, 0);
            Vector3 rotatedPointWithHeight = rotation * new Vector3(x, y, height);

            //get the x and y positions of the next angle
            float nextX = Mathf.Cos((i + 360 / numLines) * Mathf.Deg2Rad) * radius;
            float nextY = Mathf.Sin((i + 360 / numLines) * Mathf.Deg2Rad) * radius;

            //augment by rotation
            Vector3 rotatedNextPoint = rotation * new Vector3(nextX, nextY, 0);
            Vector3 rotatedNextPointWithHeight = rotation * new Vector3(nextX, nextY, height);

            //Leading Edge
            Gizmos.DrawLine(rotatedPoint + position, rotatedNextPoint + position);
            //Trailing Edge
            Gizmos.DrawLine(rotatedPointWithHeight + position, rotatedNextPointWithHeight + position);

            //Sides
            Gizmos.DrawLine(rotatedPoint + position, rotatedPointWithHeight + position);
        }
    }
#endif
}
