using UnityEngine;

public class Ball : MonoBehaviour{

    // config params
    [SerializeField] Paddle paddle;
    [SerializeField] float ballDisFromPaddle = .7f;
    [SerializeField] float launchLeftRight = 2f;
    [SerializeField] float launchUpward = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randonFactor = 0.2f;

    // Cached Component References
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    //state
    Vector2 ballPos;
    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
       
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(launchLeftRight, launchUpward);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        ballPos = new Vector2(paddle.transform.position.x, ballDisFromPaddle);
        transform.position = ballPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randonFactor),
             Random.Range(0f, randonFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
         
    }
}
