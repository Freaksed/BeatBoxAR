using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoopsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _buttonGroup;
    private Button[] _loopBtns;
    private Button[] _deleteBtns;
    private AudioSource[] _sources;
    private AudioClip[] _loops;
    private AudioClip _loop;
    private bool _isRecording;
    private float _clipLength;

    private void Awake()
    {
        _loopBtns = _buttonGroup.GetComponentsInChildren<Button>().Where(b => b.name == "Loop")
            .OrderBy(b => b.transform.parent.name).ToArray();
        _deleteBtns = _buttonGroup.GetComponentsInChildren<Button>().Where(b => b.name == "Delete")
            .OrderBy(b => b.transform.parent.name).ToArray();
        _loops = new AudioClip[_loopBtns.Length];
        _sources = new AudioSource[_loops.Length];

        for (var i = 0; i < _sources.Length; i++)
        {
            _sources[i] = gameObject.AddComponent<AudioSource>();
            _sources[i].loop = true;
            _sources[i].playOnAwake = false;
            _sources[i].Stop();
        }
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        foreach (var loopBtn in _loopBtns)
        {
            loopBtn.onClick.AddListener(() => ToggleRecording(Array.IndexOf(_loopBtns, loopBtn)));
        }

        foreach (var deleteBtn in _deleteBtns)
        {
            deleteBtn.onClick.AddListener(() => DeleteRecording(Array.IndexOf(_deleteBtns, deleteBtn)));
        }
    }

    private void DeleteRecording(int id)
    {
        _sources[id].Stop();
        _sources[id].clip = null;
        _loops[id] = null;
        _loopBtns[id].image.color = Color.white;
    }

    private void Update()
    {
        if (_isRecording)
        {
            _clipLength += Time.deltaTime;
        }
    }

    private void ToggleRecording(int id)
    {
        if (_loops[id] == null)
        {
            _isRecording = !_isRecording;
            if (_isRecording)
            {
                _loopBtns[id].image.color = Color.yellow;
                _sources[id].Stop();
                Destroy(_loop);
                _loop = Microphone.Start(Microphone.devices[0], true, 10, 44100);
            }
            else
            {
                Microphone.End(Microphone.devices[0]);
                _loop.LoadAudioData();
                var samples = new float[_loop.samples * _loop.channels];
                _loop.GetData(samples, 0);

                var desiredLength = Mathf.FloorToInt(_clipLength * _loop.frequency);
                var newSamples = samples.Take(desiredLength).ToArray();
                var tmp = AudioClip.Create($"Loop {id}", newSamples.Length, _loop.channels, _loop.frequency, false);
                tmp.SetData(newSamples, 0);
                tmp.LoadAudioData();
                _loops[id] = tmp;

                _sources[id].clip = _loops[id];
                _sources[id].Play();

                _loop = null;
                Destroy(_loop);
            }
        }
        else
        {
            if (_sources[id].isPlaying) _sources[id].Stop();
            else _sources[id].Play();
        }

        _clipLength = 0;
        _loopBtns[id].image.color = _sources[id].isPlaying ? Color.green : Color.red;
    }
}