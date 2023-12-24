
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction ;

    private List<Transform> Segments = new List<Transform>();

    public Transform snakePrefab;

    public int InitialSize = 4;

    private void Start()
    {
        Reset();

    }
    private void Update() 
        
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (direction != Vector3.down) 
            { 
            direction = Vector3.up ;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction != Vector3.up) 
            {
                direction = Vector3.down ; 
            }
           
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction != Vector3.right) 
            {
            direction = Vector3.left;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            if (direction != Vector3.left)
            {
            direction = Vector3.right ;
            }
            
        }
    }

    private void FixedUpdate() {
        for (int i = this.Segments.Count-1; i > 0; i--)
        {
            Segments[i].position = Segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.snakePrefab);
        segment.position = this.Segments[Segments.Count - 1].position;

        Segments.Add(segment);
    }

    private void Reset()
    {
        for (int i = 1; i < this.Segments.Count; i++)
        {
            Destroy(Segments[i].gameObject);
        }

            Segments.Clear();
            Segments.Add(this.transform);

        for(int i= 1; i < this.InitialSize; i++)
        {
            Segments.Add(Instantiate(this.snakePrefab));
        }

        this.transform.position = Vector3.zero;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Respawn")
        {
            Grow();
        } else if (collision.tag=="Obstacle")
        {
            Reset();
        }
    }

}
