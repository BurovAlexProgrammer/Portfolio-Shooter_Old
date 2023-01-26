using System;
using System.Collections.Generic;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static _Project.Scripts.Extension.Common;
using static _Project.Scripts.Main.Services.Services;
using static _Project.Scripts.Main.StatisticData;
using Button = UnityEngine.UI.Button;

namespace _Project.Scripts.Main.Menu
{
    public class MenuStatisticView : MenuView
    {
        [SerializeField] private Button _buttonBack;
        [SerializeField] private List<TextField> _textFields;

        private void Awake()
        {
            _buttonBack.onClick.AddListener(MenuController.GoToPrevMenu);
        }

        private void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }

        public override UniTask Show()
        {
            for (var i = 0; i < _textFields.Count; i++)
            {
                var recordName = Enum.Parse<RecordName>(_textFields[i].Key);
                var intValue = 0;
                var stringValue = "";

                switch (recordName)
                {
                    case RecordName.AverageGameSessionDuration:
                        intValue = Mathf.RoundToInt(StatisticService.GetFloatValue(recordName));
                        _textFields[i].ValueText.text = intValue.Format(StringFormat.Time);
                        break;
                    case RecordName.LongestGameSessionDuration:
                        intValue = Mathf.RoundToInt(StatisticService.GetFloatValue(recordName));
                        _textFields[i].ValueText.text = intValue.Format(StringFormat.Time);
                        break;
                    default:
                        stringValue = StatisticService.GetRecord(recordName);
                        _textFields[i].ValueText.text = stringValue;
                        break;
                }
               
            }

            return base.Show();
        }
        
    }
}