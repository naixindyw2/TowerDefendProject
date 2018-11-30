using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ConfigManager
    {
        private const string EDITOR_RESOURCE_ROOT = "Assets/Data/";

        private void init()
        {
            LoadConfigFile("Config/States/elven_archer");
        }

        public static string LoadConfigFile(string path)
        {
            string errorInfo = "";
            object resourceObject =
                UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(EDITOR_RESOURCE_ROOT);
            if (resourceObject == null) {
                errorInfo = string.Format("load asset file {0} failed", path);
            } else {
                errorInfo = "";
            }


            TextAsset asset = resourceObject as TextAsset;
            if (asset == null) {
                return null;
            }

            string content = asset.text;
            return content;
        }
    }
}