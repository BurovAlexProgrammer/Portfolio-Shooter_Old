using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

public class MiniGameLevel : MonoBehaviour
{
    [Inject] private BasePlayer _player;
    void Start()
    {
        _player.CameraHolder.SetCamera();
    }
}
