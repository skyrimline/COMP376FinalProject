using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

#if UNITY_EDITOR

using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#endif

using UnityEngine;

[System.Serializable]
public class SCGSprtieTracker {

    public string name;
    public string location;

    public List<string> childs = new List<string> ();

    public SCGSprtieTracker (string name, string location) {
        this.name = name;
        this.location = location;

    }
    public void AddChild (string c) {
        childs.Add (c);
    }

}

[System.Serializable]
public class SCGSprtieTrackerDatabase {

    public List<SCGSprtieTracker> entry = new List<SCGSprtieTracker> ();

    public SCGSprtieTrackerDatabase (List<SCGSprtieTracker> entry) {
        this.entry = entry;
    }

}

public class SCGSaveSystem : MonoBehaviour {

    static string path;

    static SCGSprtieTrackerDatabase spriteDatabase;

    static string spriteRootFolder () {
        return Application.dataPath + "/Resources/SCG/Data/";
    }

    static string spriteDatabasePath () {

        return spriteRootFolder () + "SpriteDatabase.bytes";
    }

    static string cleanSpriteDatabasePath () {
        return "SCG/Data/SpriteDatabase";
    }

    public static List<Color> skinColors = new List<Color> ();

    public static List<Color> hairColors = new List<Color> ();

    public static List<Color> dyeColors = new List<Color> ();

    public static List<Color> leatherColors = new List<Color> ();

    [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void LoadColors () {

        SCGColorDatabase design = SCGSaveSystem.LoadColorDatabase ();
        if (design != null) {
            skinColors = design.LoadColors (design.skinColors);
            hairColors = design.LoadColors (design.hairColors);
            dyeColors = design.LoadColors (design.dyeColors);
            leatherColors = design.LoadColors (design.leatherColors);
        }

    }

#if UNITY_EDITOR
    public static void SaveColorPalette (Color[] skin, Color[] hair, Color[] dye, Color[] leather, bool fromOutside) {
        path = GetPath ();

        SCGColorDatabase design = new SCGColorDatabase (skin, hair, dye, leather);
        BinaryFormatter formatter = new BinaryFormatter ();
        FileStream stream = new FileStream (path, FileMode.Create);

        formatter.Serialize (stream, design);
        stream.Close ();
        SCGDesignCenter.dirty = fromOutside;

        if (!fromOutside) RepaintInspector (typeof (CharacterEditor2D));
    }
    public static void RepaintInspector (System.Type t) {
        Editor[] ed = (Editor[]) Resources.FindObjectsOfTypeAll<Editor> ();
        for (int i = 0; i < ed.Length; i++) {
            if (ed[i].GetType () == t) {
                ed[i].Repaint ();
                return;
            }
        }
    }

#endif

    static void ValidateFolders () {

        if (!Directory.Exists (spriteRootFolder ()))
            Directory.CreateDirectory (spriteRootFolder ());
    }

    public static void Initialize () {

        if (Application.isEditor) {

            ValidateFolders ();

            string folder = Application.dataPath + "/Resources/";

            List<SCGSprtieTracker> trackers = new List<SCGSprtieTracker> ();

            if (Directory.Exists (folder)) {

                var info = new DirectoryInfo (folder);
                FileInfo[] fileInfos;
                fileInfos = info.GetFiles ("*.*", SearchOption.AllDirectories);
                trackers.Clear ();

                for (int j = 0; j <= fileInfos.Length - 1; j++) {
                    if (GetType (fileInfos[j].Name) != "error") {

                        string rf = fileInfos[j].FullName;

                        rf = rf.Substring (rf.IndexOf ("\\Resources\\", 0) + 11, rf.Length - rf.IndexOf ("\\Resources\\", 0) - 11);
                        string cleanPath = GetNames (rf.Replace ('\\', '/'));

                        SCGSprtieTracker temp = new SCGSprtieTracker (GetNames (fileInfos[j].Name), cleanPath);
                        Sprite[] tempSprites = Resources.LoadAll<Sprite> (cleanPath);

                        if (tempSprites.Length > 0) {

                            for (int i = 0; i <= trackers.Count - 1; i++) {
                                if (trackers[i].name == temp.name) {

                                    trackers[i].location = temp.location;
                                    for (int k = 0; k <= tempSprites.Length - 1; k++) {
                                        trackers[i].AddChild (tempSprites[i].name);

                                    }
                                    return;
                                }
                            }
                        }

                        for (int i = 0; i <= tempSprites.Length - 1; i++) {
                            temp.AddChild (tempSprites[i].name);

                        }

                        trackers.Add (temp);

                    }
                }

                SCGSprtieTrackerDatabase database = new SCGSprtieTrackerDatabase (trackers);

                BinaryFormatter formatter = new BinaryFormatter ();
                FileStream stream = new FileStream (spriteDatabasePath (), FileMode.Create);

                formatter.Serialize (stream, database);
                stream.Close ();

            }

        }

        PrepareSpriteDatabase ();
    }

    static void PrepareSpriteDatabase () {

        TextAsset asset = Resources.Load (cleanSpriteDatabasePath ()) as TextAsset;

        if (asset) {
            Stream stream = new MemoryStream (asset.bytes);
            BinaryFormatter formatter = new BinaryFormatter ();
            spriteDatabase = formatter.Deserialize (stream) as SCGSprtieTrackerDatabase;
            stream.Close ();
        }

    }

    public static SCGColorDatabase LoadColorDatabase () {

        if (Application.isEditor) {

            path = GetPath ();

            if (File.Exists (path)) {
                BinaryFormatter formatter = new BinaryFormatter ();
                FileStream stream = new FileStream (path, FileMode.Open);

                SCGColorDatabase design = formatter.Deserialize (stream) as SCGColorDatabase;

                stream.Close ();

                return design;
            } else {
                return null;
            }
        } else {

            string tempPath = "SCG/Data/DesignCenterData";
            TextAsset asset = Resources.Load (tempPath) as TextAsset;
            Stream stream = new MemoryStream (asset.bytes);
            BinaryFormatter formatter = new BinaryFormatter ();
            SCGColorDatabase design = formatter.Deserialize (stream) as SCGColorDatabase;
            stream.Close ();
            return design;
        }

    }

    static string GetPath () {
        return Application.dataPath + "/Resources/SCG/Data/DesignCenterData.bytes";

    }

    public static Sprite LoadSprite (string spriteName) {

        if (string.IsNullOrEmpty (spriteName)) return null;

        if (File.Exists (spriteDatabasePath ()) || Application.isEditor == false) {

            if (spriteName.Contains ("#")) {

                string strippedName = spriteName;
                int index = 0;

                for (int i = spriteName.Length - 1; i >= 0; i--) {
                    if (spriteName[i] == '#') {
                        strippedName = spriteName.Substring (0, i);
                        index = int.Parse (spriteName.Substring (i + 1, spriteName.Length - (i + 1)));
                    }
                }

                return GetSpriteFromDatabase (strippedName, index);
            } else {
                return GetSpriteFromDatabase (spriteName);
            }

        }

        return null;

    }

    static Sprite GetSpriteFromDatabase (string spriteName) {

        if (string.IsNullOrEmpty (spriteName)) return null;

        for (int i = 0; i <= spriteDatabase.entry.Count - 1; i++) {

            if (spriteDatabase.entry[i].name == spriteName) {

                return Resources.Load<Sprite> (spriteDatabase.entry[i].location);
            } else if (spriteDatabase.entry[i].childs.Contains (spriteName)) {

                Sprite[] sprites = Resources.LoadAll<Sprite> (spriteDatabase.entry[i].location);
                for (int j = 0; j <= sprites.Length - 1; j++) {

                    if (sprites[j].name == spriteName) {

                        return sprites[j];
                    } else {

                    }
                }
                return null;
            }
        }

        return null;

    }

    static Sprite GetSpriteFromDatabase (string spriteName, int index) {

        if (spriteDatabase == null) {
            PrepareSpriteDatabase ();
        }

        if (string.IsNullOrEmpty (spriteName)) return null;

        for (int i = 0; i <= spriteDatabase.entry.Count - 1; i++) {

            if (spriteDatabase.entry[i].name == spriteName) {

                return Resources.LoadAll<Sprite> (spriteDatabase.entry[i].location) [index];
            }
        }
        return null;

    }

    public static string GetSpriteName (Sprite sprite) {

        if (sprite) {
            string spriteName = sprite.name;
            return spriteName;
        }

        return null;

    }

    private static string GetNames (string files) {

        string fn = files;

        for (int i = 0; i < fn.Length; i++) {
            if (fn[i] == '.') {
                return fn.Substring (0, i);
            }
        }
        return "";
    }

    private static string GetType (string files) {

        string fn = files;

        for (int i = fn.Length - 1; i >= 0; i--) {
            if (fn[i] == '.') {
                string result = (fn.Substring (i, fn.Length - i));

                if (result == ".psd" || result == ".png" || result == ".jpg" || result == ".bmp" || result == ".iff" ||
                    result == ".hdr" || result == ".exr" || result == ".pict") {
                    return fn.Substring (i, fn.Length - i);

                } else {
                    return "error";
                }

            }
        }
        return "";
    }

    public static void SaveBodyData (string path, CharacterBody2D b) {

        SCGSaveSystem.Initialize ();

        string fullpath = GetCurrentLoadPath (path);

        CharacterBodyData body = new CharacterBodyData (b);

        BinaryFormatter formatter = new BinaryFormatter ();
        FileStream stream = new FileStream (fullpath, FileMode.Create);

        formatter.Serialize (stream, body);
        stream.Close ();

    }

    public static CharacterBodyData LoadBodyData (string path) {
        return LoadBodyData (path, CharacterBodyData.Type.All);
    }

    public static CharacterBodyData LoadBodyData (string path, CharacterBodyData.Type loadType) {

        SCGSaveSystem.Initialize ();

        string fullpath = GetCurrentLoadPath (path);

        if (File.Exists (fullpath)) {
            BinaryFormatter formatter = new BinaryFormatter ();
            FileStream stream = new FileStream (fullpath, FileMode.Open);
            CharacterBodyData body = formatter.Deserialize (stream) as CharacterBodyData;
            stream.Close ();

            body.loadType = loadType;

            return body;
        } else {

            TextAsset asset = Resources.Load (path) as TextAsset;
            Stream stream = new MemoryStream (asset.bytes);
            BinaryFormatter formatter = new BinaryFormatter ();
            CharacterBodyData body = formatter.Deserialize (stream) as CharacterBodyData;
            stream.Close ();

            body.loadType = loadType;

            return body;

        }

    }

    static void ValidateProfileFolder (string path) {

        string result = path;
        for (int i = path.Length - 1; i >= 0; i--) {
            if (path[i] == '/') {
                result = path.Substring (0, i);
                break;
            }
        }
        if (!Directory.Exists (result)) Directory.CreateDirectory (result);
    }

    static string GetCurrentLoadPath (string path) {

        if (Application.isEditor) {

            string result = Application.dataPath + "\\Resources\\" + path + ".bytes";
            ValidateProfileFolder (result);
            return result;

        } else {

            string result = Application.dataPath + "\\" + path + ".bytes";
            ValidateProfileFolder (result);
            return result;

        }
    }

}

#if UNITY_EDITOR
class SCGBuildProcessor : IPreprocessBuildWithReport {
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild (BuildReport report) {
        SCGSaveSystem.Initialize ();

    }

}

[InitializeOnLoad]
public class SCGStartup {
    static SCGStartup () {

        SCGSaveSystem.LoadColors ();

    }
}

public class ResourcesInitializer : AssetPostprocessor {

    static bool initialized = false;
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {

        if (Directory.Exists (Application.dataPath + "\\SCG\\Resources")) {
            if (!initialized) {

                MoveDirectory (Application.dataPath + "\\SCG\\Resources", Application.dataPath + "\\Resources");

                Debug.Log ("Initialized Smart Character Generator 2D.");
                AskForSetting ();
                initialized = true;
            }
        }

        SCGSaveSystem.LoadColors ();
    }

    static void AskForSetting () {

        const string GraphicsSettingsAssetPath = "ProjectSettings/GraphicsSettings.asset";
        SerializedObject graphicsManager = new SerializedObject (UnityEditor.AssetDatabase.LoadAllAssetsAtPath (GraphicsSettingsAssetPath) [0]);
        SerializedProperty sortMode = graphicsManager.FindProperty ("m_TransparencySortMode");
        SerializedProperty sortAxis = graphicsManager.FindProperty ("m_TransparencySortAxis");

        if (sortMode.intValue != 3 || sortAxis.vector3Value != new Vector3 (0, 1f, 0f)) {
            if (EditorUtility.DisplayDialog ("Smart Character Generator 2D", "Would you like to set Transparency Sort Mode of this project to Custom Axis and sort each SCG character by Y position value? ", "Yes", "No")) {
                sortMode.intValue = 3;
                sortAxis.vector3Value = new Vector3 (0, 1f, 0f);
            }
        }
        graphicsManager.ApplyModifiedProperties ();

    }

    public static void MoveDirectory (string source, string target) {
        var stack = new Stack<Folders> ();
        stack.Push (new Folders (source, target));

        while (stack.Count > 0) {
            var folders = stack.Pop ();
            Directory.CreateDirectory (folders.Target);
            foreach (var file in Directory.GetFiles (folders.Source, "*")) {
                string targetFile = Path.Combine (folders.Target, Path.GetFileName (file));
                if (File.Exists (targetFile)) File.Delete (targetFile);
                File.Move (file, targetFile);
            }

            foreach (var folder in Directory.GetDirectories (folders.Source)) {
                stack.Push (new Folders (folder, Path.Combine (folders.Target, Path.GetFileName (folder))));
            }
        }
        Directory.Delete (source, true);
    }
    public class Folders {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public Folders (string source, string target) {
            Source = source;
            Target = target;
        }
    }
}

#endif