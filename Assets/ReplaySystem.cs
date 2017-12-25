using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

    private const int bufferFrames = 1000;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];

    private Rigidbody rigidbody;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        //MyKeyFrame testKey = new MyKeyFrame(1.0f, Vector3.up, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.recording)
        {
            Record();
        }
        else
        {
            PlayBack();
        }
        
    }

    void PlayBack()
    {
        rigidbody.isKinematic = true;
        int frame = Time.frameCount % bufferFrames;
        print("reading Frame " + frame);
        transform.position = keyFrames[frame].position;
        transform.rotation = keyFrames[frame].rotation;
    }

    private void Record()
    {
        rigidbody.isKinematic = false;
        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;
        print("writing Frame " + frame);
        keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
    }
}

/// <summary>
/// A structure for storing time, position and rotation
/// </summary>
public struct MyKeyFrame
{
    public float frameTime;
    public Vector3 position;
    public Quaternion rotation;

    public MyKeyFrame(float frameTime, Vector3 position, Quaternion rotation)
    {
        this.frameTime = frameTime;
        this.position = position;
        this.rotation = rotation;
    }
}