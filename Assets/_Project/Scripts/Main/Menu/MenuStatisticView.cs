﻿using System;
using System.Collections.Generic;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.UI;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Service;
using UnityEngine;
using UnityEngine.UI;
using static _Project.Scripts.Extension.Common;
using static _Project.Scripts.Main.StatisticData;

namespace _Project.Scripts.Main.Menu
{
    public class MenuStatisticView : MenuView
    {
        [SerializeField] private Button _buttonBack;
        [SerializeField] private List<TextField> _textFields;

        private StatisticService _statisticService;
        
        private void Awake()
        {
            _statisticService = Context.GetService<StatisticService>();
            _buttonBack.onClick.AddListener(GoPrevMenu);
        }

        private void OnDestroy()
        {
            _buttonBack.onClick.RemoveListener(GoPrevMenu);
        }

        public override UniTask Show()
        {
            for (var i = 0; i < _textFields.Count; i++)
            {
                var recordName = Enum.Parse<RecordName>(_textFields[i].Key);
                var intValue = 0;

                switch (recordName)
                {
                    case RecordName.AverageGameSessionDuration:
                        intValue = Mathf.RoundToInt(_statisticService.GetFloatValue(recordName));
                        _textFields[i].ValueText.text = intValue.Format(StringFormat.Time);
                        break;
                    case RecordName.LongestGameSessionDuration:
                        intValue = Mathf.RoundToInt(_statisticService.GetFloatValue(recordName));
                        _textFields[i].ValueText.text = intValue.Format(StringFormat.Time);
                        break;
                    default:
                        var stringValue = _statisticService.GetRecord(recordName);
                        _textFields[i].ValueText.text = stringValue;
                        break;
                }
               
            }

            return base.Show();
        }
        
    }
}