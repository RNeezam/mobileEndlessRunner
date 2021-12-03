using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetPlayer;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targetPlayer.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y , offset.z + targetPlayer.position.z);
        transform.position = Vector3.Lerp(newPosition, newPosition, 10 * Time.deltaTime);
    }
}
