using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BeatDetector : MonoBehaviour
{
    enum Direction
    {
        Up, Down
    }

    [SerializeField] private TMP_Text _bpmDisplay;
    private double _bpm;
    
    private float _lastPitch;
    private DateTime _lastBeat;
    [SerializeField]private List<float> _times;
    private float _gap = 0;
    private Direction _currentDirection = Direction.Up;

    private void Awake()
    {
        _times = new List<float>();
    }

    private void Start()
    {
        _lastPitch = transform.eulerAngles.x;
        _lastBeat = DateTime.Now;
    }

    private void Update()
    {
        _gap += Time.deltaTime;
        if (_currentDirection == Direction.Down && transform.eulerAngles.x < _lastPitch-30)
        {
            Debug.Log("Bottom");
            _currentDirection = Direction.Up;
            _times.Add(_gap);
            _gap = 0;
            if (_times.Count > 10) _times.RemoveAt(0);
            if (_times.Count == 0) return;
            _bpm = 60/(_times.Sum(t=>t)/_times.Count);
        }

        if (_currentDirection == Direction.Up && transform.eulerAngles.x > _lastPitch + 30)
        {
            Debug.Log("TOP");
            _currentDirection = Direction.Down;
        }
        _lastPitch = transform.eulerAngles.x;
        _bpmDisplay.text = $"BPM: {_bpm:0000}";
    }
}
