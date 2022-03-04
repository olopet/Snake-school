using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PeterOlofsson
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 _direction = Vector2.right;

        private List<Transform> _segments;

        public Transform segmentPrefab;

        public Text scoreText;
        public Text highScoreText;
        

        private int score = 0;
        private int highScore = 0;
        string highScoreKey;

        private void Start()
        {
            //Get the highScore from player prefs if it is there, 0 otherwise.
            highScore = PlayerPrefs.GetInt(highScoreKey, 0);
            highScoreText.text = highScore.ToString();

            _segments = new List<Transform>();
            _segments.Add(this.transform);
        }

        // Update is called once per frame
        void Update()
        {
            PlayerInput();
        }

        private void FixedUpdate()
        {

            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }

            this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f);

        }

        private void PlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _direction = Vector2.right;
            }
        }

        private void GrowSnake()
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = _segments[_segments.Count - 1].position;

            _segments.Add(segment);
            score += 1;
            scoreText.text = score.ToString();
        }

        private void ShrinkSnake()
        {
            Destroy(_segments[_segments.Count - 1].gameObject);
            _segments.RemoveAt(_segments.Count - 1);

            score -= 1;
            scoreText.text = score.ToString();
        }

        private void ResetState()
        {
            for (int i = 1; i < _segments.Count; i++)
            {
                Destroy(_segments[i].gameObject); 
            }

            _segments.Clear();
            _segments.Add(this.transform);

            Time.fixedDeltaTime = 0.06f;
            
            

            //Higscore
            if (score > highScore)
            {
               PlayerPrefs.SetInt(highScoreKey, score);
               PlayerPrefs.Save();
               highScoreText.text = score.ToString();
            }
            // Reset score
            score = 0;
            scoreText.text = score.ToString();

            transform.position = Vector3.zero;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Food")
            {
                GrowSnake();
            }

            else if (other.tag == "Obstacle")
            {
                ResetState();
            }

            else if (other.tag == "Debuff")
            {
                ShrinkSnake();
            }

            else if (other.tag == "Player")
            {
                ResetState();
            }
        }

    }
}
