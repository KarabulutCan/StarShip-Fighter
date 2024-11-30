using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxSpeed;
    private float spriteHight;
    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        spriteHight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * parallaxSpeed * Time.deltaTime);

        if (transform.position.y< startPos.y - spriteHight)
        {
            transform.position = startPos;
        }
    }
}
