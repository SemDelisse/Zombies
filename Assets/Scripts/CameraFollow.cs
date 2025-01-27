using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private float XOffset = 0f;
    [SerializeField] private float YOffSet = 0f;
    [SerializeField] private float ZOffset = 10f;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update() {
        Vector3 newPos = new Vector3(target.position.x + XOffset, target.position.y + YOffSet, target.position.z + ZOffset);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}