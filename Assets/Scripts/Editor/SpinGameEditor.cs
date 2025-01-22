using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VertigoCaseProject.Configs;
using Object = UnityEngine.Object;

public class SpinGameEditor : EditorWindow
{
    [Header("Spin Game Variables")]
    private SpinGameSO _spinGameSO;
    
    private int _sliceCount = 1;
    private int _roundCount = 1;
    
    private int _safeZoneInterval = 1;
    private int _superZoneInterval = 1;
    
    private List<RoundDataSO> _roundInfoSOs = new List<RoundDataSO>();

    [Header("Slice Variables")]
    private GameObject _slicePrefab;
    private Transform _sliceParent;

    private bool[] _roundFoldoutArray;
    private bool[] _sliceFoldoutArray;
    
    private Vector2 _mainScrollPosition;

    [MenuItem("Tools/Spin Game Editor")]
    public static void ShowWindow()
    {
        GetWindow<SpinGameEditor>("Spin Game Editor");
    }

    private void OnGUI()
    {
        _mainScrollPosition = EditorGUILayout.BeginScrollView(_mainScrollPosition);

        DrawHeader("Spin Game Settings", Color.cyan);
        DrawSpinGameSettingsPanel();

        EditorGUILayout.Space(10);

        DrawHeader("Slice Spawner", Color.cyan);
        DrawSliceSpawnerPanel();

        EditorGUILayout.Space(10);

        DrawHeader("Round Settings", Color.cyan);
        DrawRoundSettings();

        EditorGUILayout.EndScrollView();
    }

    private void DrawSpinGameSettingsPanel()
    {
        GUILayout.BeginVertical(EditorStyles.helpBox);

        _spinGameSO = DrawObjectField("Spin Game ScriptableObject", _spinGameSO);
        _sliceCount = Mathf.Clamp(EditorGUILayout.IntField("Slice Count", _sliceCount), 1, 20);
        _roundCount = Mathf.Clamp(EditorGUILayout.IntField("Round Count", _roundCount), 1, 100);
        _safeZoneInterval = (int)Mathf.Clamp(EditorGUILayout.IntField("Safe Zone Interval", _safeZoneInterval), 1, Mathf.Infinity);
        _superZoneInterval = (int)Mathf.Clamp(EditorGUILayout.IntField("Super Zone Interval", _superZoneInterval), 1, Mathf.Infinity);

        EnsureListSize(ref _roundInfoSOs, _roundCount);

        EditorGUILayout.Space();

        if (GUILayout.Button("Create New SpinGameSO", GUILayout.Height(30)))
        {
            CreateNewScriptableObject("NewSpinGameSO", "Assets/ScriptableObjects/SpinGameSOs",out SpinGameSO createdSpinGameSO);
            _spinGameSO = createdSpinGameSO;
        }
        
        if (GUILayout.Button("Load SpinGameSO", GUILayout.Height(30)))
        {
            LoadFromSpinGameSO();
        }
        if (GUILayout.Button("Set SpinGameSO", GUILayout.Height(30)))
        {
            SaveToSpinGameSO();
        }

        GUILayout.EndVertical();
    }

    private void DrawSliceSpawnerPanel()
    {
        GUILayout.BeginVertical(EditorStyles.helpBox);

        _slicePrefab = DrawObjectField("Slice Prefab", _slicePrefab);
        _sliceParent = DrawObjectField("Slice Parent", _sliceParent);

        EditorGUILayout.Space();

        if (GUILayout.Button("Spawn Slices", GUILayout.Height(30)))
        {
            if (_slicePrefab == null || _sliceParent == null)
            {
                Debug.LogError("Slice Prefab or Parent is not set!");
                return;
            }
            SpawnSlices();
        }

        GUILayout.EndVertical();
    }

    private void DrawRoundSettings()
    {
        EnsureListSize(ref _roundInfoSOs, _roundCount);
        EnsureFoldoutArraySize();

        for (int i = 0; i < _roundCount; i++)
        {
            var roundInfo = _roundInfoSOs[i];
            string errorMessage = ValidateRound(roundInfo);
            string zoneTypeWarning = ZoneTypeWarning(i);
            

            Color roundColor = string.IsNullOrEmpty(errorMessage) && string.IsNullOrEmpty(zoneTypeWarning)
                ? GetRoundColor(roundInfo?.roundZoneType ?? RoundZoneType.StandardZone)
                : Color.red;
            
            GUI.color = roundColor;
            _roundFoldoutArray[i] = EditorGUILayout.Foldout(
                _roundFoldoutArray[i],
                !string.IsNullOrEmpty(errorMessage) 
                    ? $"Round {i} - {errorMessage}" 
                    : (!string.IsNullOrEmpty(zoneTypeWarning) 
                        ? $"Round {i} - {zoneTypeWarning}" 
                        : $"Round {i}"),
                true
            );
            
            if (_roundFoldoutArray[i])
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);

                GUI.color = Color.white;

                // RoundInfoSO referansı
                _roundInfoSOs[i] = (RoundDataSO)EditorGUILayout.ObjectField(
                    $"Round Data {i}",
                    roundInfo,
                    typeof(RoundDataSO),
                    false
                );

                // Eğer yoksa yeni oluştur
                if (_roundInfoSOs[i] == null && GUILayout.Button("Create New Round Info SO", GUILayout.Height(30)))
                {
                    CreateNewScriptableObject($"RoundDataSO_{i}", "Assets/ScriptableObjects/RoundDataSOs",out RoundDataSO createdRoundInfoSO);
                    _roundInfoSOs[i] = createdRoundInfoSO;
                }

                // RoundInfoSO varsa => slices'ı çiz
                if (_roundInfoSOs[i] != null)
                {
                    // RoundInfoSO üzerinde slice sayısını sabitle
                    SerializedObject serializedRoundInfo = new SerializedObject(_roundInfoSOs[i]);
                    SerializedProperty roundTypeProperty = serializedRoundInfo.FindProperty("roundZoneType");
                    SerializedProperty slicesProperty = serializedRoundInfo.FindProperty("slices");

                    // Slices dizisini istenen boyuta ayarla
                    while (slicesProperty.arraySize < _sliceCount)
                        slicesProperty.arraySize++;
                    while (slicesProperty.arraySize > _sliceCount)
                        slicesProperty.arraySize--;

                    // RoundType göster
                    EditorGUILayout.PropertyField(roundTypeProperty);

                    // Slices dizisini göster
                    DrawSlicesEditor(slicesProperty);

                    // Değişiklik olduysa kaydet
                    if (serializedRoundInfo.hasModifiedProperties)
                    {
                        serializedRoundInfo.ApplyModifiedProperties();
                    }
                }

                GUILayout.EndVertical();
            }
        }

        GUI.color = Color.white;
    }

    private void DrawSlicesEditor(SerializedProperty slicesProperty)
    {
        EnsureSliceFoldoutSize(slicesProperty.arraySize);

        for (int i = 0; i < slicesProperty.arraySize; i++)
        {
            // Artık her slice, bir objectReference => SliceInfoSO
            SerializedProperty sliceSoProperty = slicesProperty.GetArrayElementAtIndex(i);
            SlicePrizeDataSO sliceSo = sliceSoProperty.objectReferenceValue as SlicePrizeDataSO;

            // Hata var mı diye kontrol edelim
            string errorMessage = ValidateSlice(sliceSo);
            GUI.color = string.IsNullOrEmpty(errorMessage) ? Color.black : Color.red;

            _sliceFoldoutArray[i] = EditorGUILayout.Foldout(
                _sliceFoldoutArray[i],
                string.IsNullOrEmpty(errorMessage)
                    ? $"Slice {i}"
                    : $"Slice {i} - {errorMessage}",
                true
            );

            if (_sliceFoldoutArray[i])
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUI.color = Color.white;

                // Slice Info SO alanı
                sliceSoProperty.objectReferenceValue = EditorGUILayout.ObjectField(
                    $"SlicePrizeData {i}",
                    sliceSo,
                    typeof(SlicePrizeDataSO),
                    false
                );

                // Eğer atanmamışsa => "Create New SliceInfoSO" butonu
                if (sliceSo == null && GUILayout.Button("Create New SliceInfoSO", GUILayout.Height(25)))
                {
                    CreateNewScriptableObject($"SlicePrizeDataSO{i}", "Assets/ScriptableObjects/SlicePrizeDataSOs",out SlicePrizeDataSO newSliceInfoSo);
                    sliceSoProperty.objectReferenceValue = newSliceInfoSo;
                }
                else if (sliceSo != null)
                {
                    // SliceInfoSO varsa => kendi alanlarını göster (type, prize, possibilities vs.)
                    SerializedObject sliceSoSerialized = new SerializedObject(sliceSo);
                    SerializedProperty typeProp = sliceSoSerialized.FindProperty("type");
                    SerializedProperty prizeProp = sliceSoSerialized.FindProperty("prize");
                    SerializedProperty possibilitiesProp = sliceSoSerialized.FindProperty("possibilities");

                    EditorGUILayout.PropertyField(typeProp);

                    // Direct ise => prize göster
                    if ((SlicePrizeDataType)typeProp.enumValueIndex == SlicePrizeDataType.Direct)
                    {
                        EditorGUILayout.PropertyField(prizeProp);
                    }
                    // Possibility ise => possibilities listesi göster
                    else
                    {
                        DrawPossibilitiesEditor(possibilitiesProp);
                    }

                    // Değişikliği uygula
                    if (sliceSoSerialized.hasModifiedProperties)
                    {
                        sliceSoSerialized.ApplyModifiedProperties();
                    }
                }

                GUILayout.EndVertical();
            }
        }

        GUI.color = Color.white;
    }

    private void DrawPossibilitiesEditor(SerializedProperty possibilitiesProperty)
    {
        if (GUILayout.Button("Add Possibility", GUILayout.Height(25)))
        {
            possibilitiesProperty.arraySize++;
        }

        for (int i = 0; i < possibilitiesProperty.arraySize; i++)
        {
            SerializedProperty possibilityProperty = possibilitiesProperty.GetArrayElementAtIndex(i);
            SerializedProperty categorizedPrizeProperty = possibilityProperty.FindPropertyRelative("categorizedPrize");
            SerializedProperty possibilityPercentProperty = possibilityProperty.FindPropertyRelative("possibilityPercent");

            GUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.PropertyField(categorizedPrizeProperty);
            EditorGUILayout.PropertyField(possibilityPercentProperty);

            if (GUILayout.Button("Remove Possibility", GUILayout.Height(20)))
            {
                possibilitiesProperty.DeleteArrayElementAtIndex(i);
            }

            GUILayout.EndVertical();
        }
    }
    
    private void LoadFromSpinGameSO()
    {
        if (_spinGameSO != null)
        {
            _sliceCount = _spinGameSO.sliceCount;
            _roundCount = _spinGameSO.roundCount;
            _safeZoneInterval = _spinGameSO.safeZoneInterval;
            _superZoneInterval = _spinGameSO.superZoneInterval;
            EnsureListSize(ref _roundInfoSOs, _roundCount);
            EnsureFoldoutArraySize();
            _roundInfoSOs = _spinGameSO.rounds.ToList();
        }
    }

    private void SaveToSpinGameSO()
    {
        if (_spinGameSO != null)
        {
            _spinGameSO.sliceCount = _sliceCount;
            _spinGameSO.roundCount = _roundCount;
            _spinGameSO.safeZoneInterval = _safeZoneInterval;
            _spinGameSO.superZoneInterval = _superZoneInterval;
            _spinGameSO.rounds.Clear();
            _spinGameSO.rounds = _roundInfoSOs.ToList();
        }
    }

    private string ValidateRound(RoundDataSO roundData)
    {
        if (roundData == null)
        {
            return "Round property is null";
        }

        // Tüm slice'ların geçerli olup olmadığına bakmak isterseniz
        for (int i = 0; i < _sliceCount; i++)
        {
            if (i < roundData.slices.Count)
            {
                var sliceSo = roundData.slices[i];
                var result = ValidateSlice(sliceSo);
                if (!string.IsNullOrEmpty(result))
                    return "Some Slices are invalid";
            }
        }
        return string.Empty;
    }

    private string ValidateSlice(SlicePrizeDataSO sliceSo)
    {
        if (sliceSo == null)
            return "SlicePrizeData is missing";

        if (sliceSo.type == SlicePrizeDataType.Direct && sliceSo.prize == null)
        {
            return "Prize cant be empty (Direct Slice)";
        }
        else if (sliceSo.type == SlicePrizeDataType.Possibility)
        {
            if (sliceSo.possibilities == null || sliceSo.possibilities.Count == 0)
            {
                return "Possibility missing";
            }
            //float totalPercent = 0f;
            for (int i = 0; i < sliceSo.possibilities.Count; i++)
            {
                var possibility = sliceSo.possibilities[i];
                if (possibility.categorizedPrize == null)
                {
                    return "CategorizedPrize missing";
                }
                //totalPercent += possibility.possibilityPercent;
            }
            // if (Mathf.Abs(totalPercent - 100f) > 0.01f)
            // {
            //     return "Toplam yüzdelik %100 değil";
            // }
        }
        return string.Empty;
    }

    private void SpawnSlices()
    {
        float angleStep = 360f / _sliceCount;

        for (int i = 0; i < _sliceCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject spawnedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(_slicePrefab, _sliceParent);
            spawnedPrefab.transform.localPosition = Vector3.zero;
            spawnedPrefab.transform.localRotation = rotation;
            spawnedPrefab.transform.localScale = Vector3.one;

            Undo.RegisterCreatedObjectUndo(spawnedPrefab, "Spawn Slices");
        }

        Debug.Log($"{_sliceCount} slices spawned.");
    }

    private Color GetRoundColor(RoundZoneType roundZoneType)
    {
        return roundZoneType switch
        {
            RoundZoneType.StandardZone => Color.white,
            RoundZoneType.SafeZone => Color.yellow,
            RoundZoneType.SuperZone => Color.green,
            _ => Color.gray,
        };
    }

    private void DrawHeader(string title, Color color)
    {
        Color defaultColor = GUI.color;
        GUI.color = color;
        GUILayout.BeginHorizontal(EditorStyles.helpBox);
        GUILayout.Label(title, EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
        GUI.color = defaultColor;
    }

    private T DrawObjectField<T>(string label, T value) where T : Object
    {
        return (T)EditorGUILayout.ObjectField(label, value, typeof(T), true);
    }

    private void CreateNewScriptableObject<T>(string defaultName,string filePath, out T createdObject) where T : ScriptableObject
    {
        T instance = ScriptableObject.CreateInstance<T>();

        string path = EditorUtility.SaveFilePanelInProject(
            "Save ScriptableObject",
            defaultName,
            "asset",
            "Please enter a file name to save the ScriptableObject to",
            filePath
        );

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = instance;
        }

        createdObject = instance;
    }

    private void EnsureListSize<T>(ref List<T> list, int requiredSize) where T : class
    {
        while (list.Count < requiredSize)
            list.Add(null);
        while (list.Count > requiredSize)
            list.RemoveAt(list.Count - 1);
    }

    private void EnsureFoldoutArraySize()
    {
        if (_roundFoldoutArray == null || _roundFoldoutArray.Length != _roundCount)
        {
            _roundFoldoutArray = new bool[_roundCount];
        }
    }

    private void EnsureSliceFoldoutSize(int size)
    {
        if (_sliceFoldoutArray == null || _sliceFoldoutArray.Length != size)
        {
            _sliceFoldoutArray = new bool[size];
        }
    }

    private string ZoneTypeWarning(int i)
    {
        if (!_roundInfoSOs[i])
            return string.Empty;
            
        if (i == 0)
        {
            if (_roundInfoSOs[i].roundZoneType != RoundZoneType.SafeZone)
            {
                return "This Round Supposed to be SafeZone";
            } 
            // _roundInfoSOs[i].roundZoneType = RoundZoneType.SafeZone;
                
        }
        else if ((i + 1) % Mathf.Clamp(_superZoneInterval, 1, Mathf.Infinity) == 0)
        {
            if (_roundInfoSOs[i].roundZoneType != RoundZoneType.SuperZone)
            {
                return "This Round Supposed to be SuperZone";
            } 
            //_roundInfoSOs[i].roundZoneType = RoundZoneType.SuperZone;
        }
        else if ((i + 1) % Mathf.Clamp(_safeZoneInterval, 1, Mathf.Infinity) == 0)
        {
            if (_roundInfoSOs[i].roundZoneType != RoundZoneType.SafeZone)
            {
                return "This Round Supposed to be SafeZone";
            } 
            //_roundInfoSOs[i].roundZoneType = RoundZoneType.SafeZone;
        }
        else
        {
            if (_roundInfoSOs[i].roundZoneType != RoundZoneType.StandardZone)
            {
                return "This Round Supposed to be StandartZone";
            } 
            //_roundInfoSOs[i].roundZoneType = RoundZoneType.StandardZone;
        }

        return string.Empty;
    }
}