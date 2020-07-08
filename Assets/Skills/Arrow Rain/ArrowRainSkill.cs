using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The Arrow Rain skill charge attack creates a certain point on the map and spawns a hail of arrows to fall continuously on that point.
 */
public class ArrowRainSkill : MonoBehaviour {
    [Tooltip("The base object for the arrow that will be spawning here")]
    [SerializeField] GameObject arrowPrefab;

    [Tooltip("The number of arrows we want to fire with this skill")]
    [SerializeField] int numberOfArrows = 20;

    [Tooltip("Adjusts a speed factor for the arrows falling towards the point")]
    [SerializeField] float arrowSpeed = 10f;

    [Tooltip("The horizontal area around the central point where arrows can spawn")]
    [SerializeField] float spawnRadius = 3f;

    [Tooltip("How high up the arrows start when they spawn")]
    [SerializeField] float spawnHeight = 8f;

    [Tooltip("Angular limit for the arrow rotation (in terms of degree from 0) for when arrows spawn")]
    [SerializeField] float rotationLimit = 25f;

    [SerializeField] float timeBetweenArrows = 0.2f;
    [SerializeField] float timeBetweenArrowsRandomFactor = 0.1f;

    List<GameObject> arrows;

    void Start() {
        // Instantiating here since I don't know if Unity classes can leak object states or not
        arrows = new List<GameObject>();
        StartCoroutine(SpawnAllArrows());
    }

    void Update() {
        foreach (GameObject arrow in arrows) {
            MoveArrow(arrow);
        }
    }

    private void SpawnArrow() {
        Vector2 currentPosition = transform.position;
        float spawnXPosition = Random.Range(currentPosition.x - spawnRadius, currentPosition.x + spawnRadius);
        float spawnYPosition = transform.position.y + spawnHeight;

        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = new Vector2(spawnXPosition, spawnYPosition);

        // Given the arrow's x distance from center, modify the Z rotation to point it towards the center
        float horizontalDifference = currentPosition.x - spawnXPosition;
        float zRotationOffset = (horizontalDifference / spawnRadius) * rotationLimit;
        arrow.transform.eulerAngles = new Vector3(
            arrow.transform.rotation.eulerAngles.x,
            arrow.transform.rotation.eulerAngles.y,
            arrow.transform.rotation.eulerAngles.z + zRotationOffset
        );

        arrows.Add(arrow);
    }

    private IEnumerator SpawnAllArrows() {
        for (int counter = 0; counter < numberOfArrows; counter++) {
            SpawnArrow();
            yield return new WaitForSeconds(
                Random.Range(timeBetweenArrows - timeBetweenArrowsRandomFactor, timeBetweenArrows + timeBetweenArrowsRandomFactor)
            );
        }
        // Arbitrary wait time, TODO - use a better method
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject arrow in arrows) {
            if (arrow != null) {
                Destroy(arrow);
            }
        }
        Destroy(gameObject);
    }

    private void MoveArrow(GameObject arrow) {
        if (arrow != null) {
            float step = arrowSpeed * Time.deltaTime;

            // move sprite towards the target location
            arrow.transform.position = Vector2.MoveTowards(arrow.transform.position, transform.position, step);
            if (arrow.transform.position == transform.position || Mathf.Approximately(step, 0)) {
                print("Arrow should be destroyed");
                Destroy(arrow);
            }
        }
    }
}
