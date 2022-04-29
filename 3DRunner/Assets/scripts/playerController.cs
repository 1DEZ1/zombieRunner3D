using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 dir;
    private int lineToMove = 1;
    public float lineDistace = 4;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private int health = 3;
    [SerializeField] public GameObject[] healthLength;

    void Start()
    {
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider coll)
    {

        health--;

        if (coll.gameObject.tag == "Zombie" && health == 2)
        {
            healthLength[0].SetActive(false);
        }
        if (coll.gameObject.tag == "Zombie" && health == 1)
        {
            healthLength[1].SetActive(false);
        }
        if (coll.gameObject.tag == "Zombie" && health == 0)
        {
            healthLength[2].SetActive(false);
        }

    }

    void Update()
    {
        if (health == 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (swipeController.swipeRight)
        {
            if (lineToMove < 2)
            {
                lineToMove++;
            }
        }
        if (swipeController.swipeLeft)
        {
            if (lineToMove > 0)
            {
                lineToMove--;
            }
        }

        if (swipeController.swipeUp && controller.isGrounded)
        {
            Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineToMove == 0)
        {
            targetPosition += Vector3.left * lineDistace;
        }
        else if (lineToMove == 2)
        {
            targetPosition += Vector3.right * lineDistace;
        }

        if (transform.position == targetPosition)
        {
            return;
        }

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Jump(){
        dir.y = jumpForce;
    }

    void FixedUpdate()
    {
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }


}
