using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Main.Localizations;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Extension.Editor.LocalizationTools
{
    public class LocalizationToolsWindow : EditorWindow
    {
        private int _selectedLocaleIndex;
        private Localization _selectedLocalizationInstance;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _originalLocalization;
        private string[] _localeNames;
        private List<RowItem> _rowItems;
        
        private Vector2 _tableScrollPos;
        private Rect _tableScrollViewRect;
        private bool _selectedOriginal;
        private string _newKeyName;

        [MenuItem ("Tools/Localization Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(LocalizationToolsWindow)) as LocalizationToolsWindow;
            window.titleContent = new GUIContent { text = "Localization Editor" };
            window.Focus();
            window.Init();
            window.Show();
        }

        private void OnEnable()
        {
            Init();
        }

        private void OnEnterPress()
        {
            GUI.FocusControl(null);
            Repaint();
        }

        private void Init()
        {
            Debug.Log("Init");
            _localizations = LocalizationTools.Instance.Localizations;
            _originalLocalization = LocalizationTools.Instance.OriginalLocalization;
            _localeNames = _localizations.Values.Select(x => x.Info.name).ToArray();
            _selectedLocalizationInstance = new Localization(_originalLocalization);
            _selectedOriginal = true;
            RefreshView();
            MarkOriginalTab();
        }

        private void OnGUI()
        {
            if (_localeNames.Length == 0) Init();
            
            Event e = Event.current;

            if (e.type == EventType.KeyDown && (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter))
            {
                OnEnterPress();
            }

            GUILayout.ExpandWidth(false);
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Localizations: ", GUILayout.ExpandWidth(false));
            var selectedLocaleIndex = GUILayout.Toolbar (_selectedLocaleIndex, _localeNames, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(128));
            GUILayout.EndHorizontal();
            GUILayout.Space(8);
            DrawToolbar();
            GUILayout.Space(8);
            DrawLocalizationInfo();
            GUILayout.Space(8);
            DrawLocalizationTable();
            
            if (selectedLocaleIndex != _selectedLocaleIndex)
            {
                SwitchLocale(selectedLocaleIndex);
            }
        }

        private void RefreshView()
        {
            _rowItems = new List<RowItem>();
            
            foreach (var (key, localizedItem) in _selectedLocalizationInstance.LocalizedItems)
            {
                _rowItems.Add(new RowItem(localizedItem));    
            }
        }

        private void Reset()
        {
            Init();
        }

        private void Save()
        {
            LocalizationTools.Instance.SaveLocalization(_selectedLocalizationInstance, _selectedOriginal);
            Init();
        }

        private void AddNewKey()
        {
            if (string.IsNullOrEmpty(_newKeyName))
                throw new Exception("Localization Key cannot be empty.");
            if (_selectedLocalizationInstance.LocalizedItems.ContainsKey(_newKeyName))
                throw new Exception("Localization Key already exist.");
            LocalizationTools.Instance.AddNewKey(_newKeyName);
            _selectedLocalizationInstance.LocalizedItems.Add(_newKeyName, new LocalizedItem {Key = _newKeyName});
        }

        private void DrawLocalizationTable()
        {

            GUILayout.BeginHorizontal();
                GUILayout.Label("#",GUILayout.Width(32));
                GUILayout.Label("Key",GUILayout.Width(200));
                GUILayout.Label("Description",GUILayout.Width(200));
                GUILayout.Label("Original",GUILayout.Width(300));
                if (!_selectedOriginal)
                {
                    GUILayout.Label("Text");
                }
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            _tableScrollPos = GUILayout.BeginScrollView(_tableScrollPos, false, true);

                var number = 0;
                // foreach (var (key, localizedItem) in _selectedLocalizationInstance.LocalizedItems)
                foreach (var rowItem in _rowItems)
                {
                    var localizedItem = rowItem.LocalizedItem;
                    GUILayout.BeginHorizontal();
                        GUILayout.Label((++number).ToString(), GUILayout.Width(32));
                        EditorGUIUtility.labelWidth = 32;
                        localizedItem.Key = GUILayout.TextField(localizedItem.Key, GUILayout.Width(200));
                        EditorGUIUtility.labelWidth = 64;
                        localizedItem.Description = GUILayout.TextField(localizedItem.Description, GUILayout.Width(200));
                        EditorGUIUtility.labelWidth = 50;
                        localizedItem.Original = GUILayout.TextField(localizedItem.Original,
                            _selectedOriginal ? GUILayout.ExpandWidth(true) : GUILayout.Width(300));

                        if (!_selectedOriginal)
                        {
                            EditorGUIUtility.labelWidth = 60;
                            localizedItem.Text = GUILayout.TextField(localizedItem.Text);
                        }

                        if (GUILayout.Button(rowItem.SureRemoveKey ? "?" : "X", GUILayout.Width(20)))
                        {
                            if (rowItem.SureRemoveKey == false)
                            {
                                rowItem.AskForRemove(); 
                            }
                            else
                            {
                                LocalizationTools.Instance.RemoveKey(rowItem.LocalizedItem.Key);
                            }
                        }
                        
                    GUILayout.EndHorizontal();
                }

            GUILayout.EndScrollView();
        }

        private void MarkOriginalTab()
        {
            for (var i = 0; i < _localeNames.Length; i++)
            {
                if (_localeNames[i] == _originalLocalization.Info.name) _localeNames[i] += " *";
            }
        }

        private void DrawToolbar()
        {
            GUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 88;
            _newKeyName = EditorGUILayout.TextField("New key name:",_newKeyName, GUILayout.ExpandWidth(false), GUILayout.Width(200));
            if (GUILayout.Button("Add new key",GUILayout.Width(100))) AddNewKey();
            GUILayout.Space(16);
            if (GUILayout.Button("Save",GUILayout.Width(100))) Save();
            if (GUILayout.Button("Reset",GUILayout.Width(100))) Reset();
            GUILayout.EndHorizontal();
        }

        private void DrawLocalizationInfo()
        {
            var info = _selectedLocalizationInstance.Info;
            GUI.changed = false;
            GUILayout.Label("Info ", GUILayout.ExpandWidth(false));
            GUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 40;
                info.name = EditorGUILayout.TextField("Name", info.name, textFieldOptions);
                GUILayout.Space(16);
                EditorGUIUtility.labelWidth = 60;
                info.fullName = EditorGUILayout.TextField("FullName", info.fullName, textFieldOptions);
            GUILayout.EndHorizontal();
            
            if (GUI.changed) { }
        }

        private void SwitchLocale(int newIndex)
        {
            Debug.Log("SwitchLocale");
            _selectedLocaleIndex = newIndex;
            var selectedLocalization = _localizations.Where((x, i) => i == newIndex).Single().Value;
            _selectedLocalizationInstance = new Localization(selectedLocalization);
            _selectedOriginal = selectedLocalization.Info.name == _originalLocalization.Info.name;
            RefreshView();
        }

        private GUILayoutOption[] textFieldOptions = {
            GUILayout.ExpandWidth(false),
        };
    }

    class RowItem
    {
        public readonly LocalizedItem LocalizedItem;
        private static int _rowIdGenerator;
        private int _id;
        private bool _sureRemoveKey;

        public RowItem(LocalizedItem localizedItem)
        {
            _id = ++_rowIdGenerator;
            LocalizedItem = localizedItem;
        }

        public int Id => _id;
        
        public bool SureRemoveKey => _sureRemoveKey;

        public void AskForRemove()
        {
            _sureRemoveKey = true;
            _ = CloseQuestionAfter(1f);
        }

        private async UniTask CloseQuestionAfter(float time)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time));
            _sureRemoveKey = false;
        } 
    } 
}