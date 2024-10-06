using System;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{

    private Rigidbody _rb;
    public float speed = 20f, finalSpeed = 50f, rotateSpeed = 350f;
    private bool isClicked;
    private static int CountCars = 0;

    private float cursorPointX, cursorPointY;
    [NonSerialized] public Vector3 FinalPosition;

    public enum Axis
    {
        Vertical, Horizontal
    }
    public Axis carAxis;

    private enum Direction
    {
        Right, Left, Bottom, Top, None
    }
    private Direction carDirectionX = Direction.None;
    private Direction carDirectionY = Direction.None;

    public Text Count, countMoney;
    public GameObject Start;

    private AudioSource _audio;
    public AudioClip AudioStart, AudioCrash;


    void Awake()
    {
        CountCars++;
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!StartGame.IsGameStarted) return;
        cursorPointX = Input.mousePosition.x;
        cursorPointY = Input.mousePosition.y;
    }

    private void OnMouseUp()
    {
        if (!StartGame.IsGameStarted) return;

        if (Input.mousePosition.x - cursorPointX < 0)
            carDirectionX = Direction.Left;
        else
            carDirectionX = Direction.Right;

        if (Input.mousePosition.y - cursorPointY < 0)
            carDirectionY = Direction.Bottom;
        else
            carDirectionY = Direction.Top;

        isClicked = true;

        Count.text = Convert.ToString(Convert.ToInt32(Count.text) - 1);

        _audio.Stop();
        _audio.clip = AudioStart;
        _audio.Play();

    }

    void Update()
    {
        if (Count.text == "0" && CountCars > 0 && !isClicked)
            Start.GetComponent<StartGame>().LoseGame();

        if (FinalPosition.x != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, FinalPosition, finalSpeed * Time.deltaTime);
            Vector3 lookAtPos = FinalPosition - transform.position;
            lookAtPos.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookAtPos), Time.deltaTime * rotateSpeed);
        }

        if (transform.position == FinalPosition)
        {
            PlayerPrefs.SetInt("CarCoins", PlayerPrefs.GetInt("CarCoins") + 1);
            countMoney.text = Convert.ToString(Convert.ToInt32(countMoney.text) + 1);
            CountCars--;

            if(CountCars == 0) Start.GetComponent<StartGame>().WinGame();

            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isClicked && FinalPosition.x == 0)
        {
            Vector3 whichWay = carAxis == Axis.Horizontal ? Vector3.forward : Vector3.left;
            speed = Math.Abs(speed);
            if (carDirectionX == Direction.Left && carAxis == Axis.Horizontal)
                speed *= -1;
            else if (carDirectionY == Direction.Bottom && carAxis == Axis.Vertical)
                speed *= -1;

            _rb.MovePosition(transform.position + whichWay * speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Barrier"))
        {
            if (_audio.clip != AudioCrash && !_audio.isPlaying)
            {
                _audio.Stop();
                _audio.clip = AudioCrash;
                _audio.Play();
            }

            if (carAxis == Axis.Horizontal && isClicked)
            {
                float adding = carDirectionX == Direction.Left ? 0.5f : -0.5f;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z + adding);
            }

            if (carAxis == Axis.Vertical && isClicked)
            {
                float adding = carDirectionY == Direction.Top ? 0.5f : -0.5f;
                transform.position = new Vector3(transform.position.x + adding, 0, transform.position.z);
            }

            isClicked = false;
        }
    }

}