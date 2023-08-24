using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float baseSpeed = 0.4f;
    [SerializeField] float randomScale = 0.1f;

    private float randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(baseSpeed, baseSpeed + randomScale);
    }

    // Update is called once per frame
    void Update()
    {   
        if (Time.timeScale > 0) 
        {
            transform.position += Vector3.right * randomSpeed;
        }
    }
}
