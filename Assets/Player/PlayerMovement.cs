using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float meleeAttackMoveStopRadius = .5f;
    [SerializeField]
    float rangedAttackMoveStopRadius = 5f;
    [SerializeField]
    float walkMoveStopRadius = .2f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    bool isInDirectMode; // TODO consider making static later

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        ResetClickTargetToCurrentPosition();
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // TODO: add option to menu later
        if (Input.GetKeyDown(KeyCode.G))
        {
            isInDirectMode = !isInDirectMode;
            ResetClickTargetToCurrentPosition();
        }
       
        if(isInDirectMode)
        {
            ProcessDirectMovement();
        } 
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    private void ProcessMouseMovement()
    {
        // Get mouse movement and shorten it based on target before processing player move
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.Hit.point;
            switch (cameraRaycaster.LayerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, meleeAttackMoveStopRadius);
                    break;
                default:
                    break;
            }
        }

        MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 distFromPlayerToClickPoint = currentDestination - transform.position;
        if (distFromPlayerToClickPoint.magnitude >= 0)
        {
            thirdPersonCharacter.Move(currentDestination - transform.position, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    private void ResetClickTargetToCurrentPosition()
    {
        currentDestination = transform.position;
    }

    /// <summary>
    /// Helper method that shortens a destination Vector3 by the given shortening factor.
    /// </summary>
    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reduction = (destination - transform.position).normalized * shortening;
        return destination - reduction;
    }

    // Called when Gizmos are drawn
    private void OnDrawGizmos()
    {
        // Draw gizmo movement
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination, .1f);
        Gizmos.DrawSphere(clickPoint, .15f);

        // Draw melee attack sphere
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, meleeAttackMoveStopRadius);

        // Draw ranged attack sphere
        Gizmos.color = new Color(255f, 0f, 120, .5f);
        Gizmos.DrawWireSphere(transform.position, rangedAttackMoveStopRadius);
    }
}

