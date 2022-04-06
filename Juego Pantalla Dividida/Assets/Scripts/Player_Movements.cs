using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Movements : MonoBehaviour
{
    enum MovementStatus { FRONT, BACK, RIGHT, LEFT, NONE };

    MovementStatus moveStatus;
    MovementStatus rotationStatus;

    [SerializeField] private GameObject[] points;

    [SerializeField] private KeyCode moveKeyFront;
    [SerializeField] private KeyCode moveKeyBack;
    [SerializeField] private KeyCode moveKeyRight;
    [SerializeField] private KeyCode moveKeyLeft;

    [SerializeField] private float forceValue = 5f;

    private Rigidbody rb;

    private Vector3 vectorFront;
    private Vector3 vectorBack;
    private Vector3 vectorRight;
    private Vector3 vectorLeft;   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ChangeMoveStatus(MovementStatus.FRONT);
        ChangeRotationStatus(MovementStatus.FRONT);

        vectorFront = Vector3.forward;
        vectorBack = Vector3.back;
        vectorRight = Vector3.right;
        vectorLeft = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(moveKeyFront)) 
        {
            ChangeMoveStatus(MovementStatus.FRONT);
        }        
        else if (Input.GetKey(moveKeyBack))
        {
            ChangeMoveStatus(MovementStatus.BACK);
        }
        else if (Input.GetKey(moveKeyRight)) 
        {
            ChangeMoveStatus(MovementStatus.RIGHT);
        }
        else if (Input.GetKey(moveKeyLeft)) 
        {
            ChangeMoveStatus(MovementStatus.LEFT);
        }        
        else 
        {
            ChangeMoveStatus(MovementStatus.NONE);
        }

        if (moveStatus != MovementStatus.NONE) 
        {
            ChangeRotationStatus(moveStatus);           
        }
    }

    private void FixedUpdate()
    {
        Move();        
    }

    void ChangeMoveStatus(MovementStatus status) 
    {
        if (moveStatus != status) 
        {
            moveStatus = status;            
        }
    }

    void ChangeRotationStatus(MovementStatus status) 
    {
        if (rotationStatus != status) 
        {
            rotationStatus = status;

            Rotation();
        }
    }

    private void Move()
    {
        switch (moveStatus)
        {
            case MovementStatus.FRONT:

                rb.MovePosition(transform.position + (vectorFront * forceValue * Time.deltaTime));
                break;
            case MovementStatus.BACK:

                rb.MovePosition(transform.position + (vectorBack * forceValue * Time.deltaTime));
                break;
            case MovementStatus.RIGHT:

                rb.MovePosition(transform.position + (vectorRight * forceValue * Time.deltaTime));
                break;
            case MovementStatus.LEFT:

                rb.MovePosition(transform.position + (vectorLeft * forceValue * Time.deltaTime));
                break;
            default:
                break;
        }
    }

    void Rotation() 
    {
        switch (rotationStatus)
        {
            case MovementStatus.FRONT:

                transform.LookAt(points[0].transform);
                break;
            case MovementStatus.BACK:

                transform.LookAt(points[1].transform);
                break;
            case MovementStatus.RIGHT:

                transform.LookAt(points[2].transform);
                break;
            case MovementStatus.LEFT:

                transform.LookAt(points[3].transform);
                break;
            default:
                break;
        }
    }    
}
