namespace Shuriken
{
    using UnityEditor;
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    /**
    * Assetsで画像を選択の上、
    * Assets>ShurikenSetSprite > 
    * 「対象オブジェクト」に画像を貼りたいSpriteRendererが子に含まれるオブジェクトを選択 > 
    * 「スプライト変更」を押下
    * で画像をSprite Rendererに貼るUnityエディタ拡張。
    * This File licensed under the terms of the MIT license
    * Copyright (c) 2018 vrshuriken
    * <version>1.0.0</version>
    */
    public class ShurikenSetSprite : EditorWindow
    {
        const int PRIORITY = 10002;
        GameObject selectedParentObject;

        [MenuItem("Assets/Shuriken SetSprite", false, PRIORITY)]
        static void Open()
        {
            EditorWindow.GetWindow<ShurikenSetSprite>("ShurikenSetSprite");
        }

        // Windowのクライアント領域のGUI処理を記述
        void OnGUI()
        {
            GUIStyle bold = new GUIStyle()
            {
                //alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
            };

            EditorGUILayout.HelpBox("1. 「対象オブジェクト」に画像を貼りたいSpriteRendererが子に含まれるオブジェクトを選択してください。\n"
                + "2. 「スプライト変更」を押すとAssetsで選択した画像(Texture2D)がspriteにセットされます。\n"
                + "※ 画像を張る順序は画像ファイル名順・Hierarchy上の並び順です。", MessageType.Info);

            EditorGUILayout.LabelField("このオブジェクト以下のSprite Rendererを変更", bold);
            selectedParentObject = EditorGUILayout.ObjectField("対象オブジェクト", selectedParentObject, typeof(GameObject), true) as GameObject;


            if (GUILayout.Button("スプライト変更"))
            {
                //DebugName();
                Apply();
                
            }
        }



        void Apply()
        {
            Texture2D[] texture2dArr = GetTexture2DListFromSelectedAsset();
            List <GameObject> spriteRendererList = FindSpriteRenderer(selectedParentObject);


            // Texture -> Spriteに変換する
            Sprite[] spriteArr = new Sprite[texture2dArr.Length];
            int i = 0;
            foreach (Texture2D tex in texture2dArr)
            {
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                spriteArr[i] = sprite;
                i++;
            }


            // spriteRendererにspriteを適用
            foreach (GameObject obj in spriteRendererList)
            {

                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    continue;
                }

                // spriteの変更をCtrl+zで戻せるように変更前に記憶しておきます
                Undo.RecordObject(spriteRenderer, "apply sprite");

                // sprite変更
                spriteRenderer.sprite = spriteArr[i % spriteArr.Length];
                spriteRenderer.name = texture2dArr[i % spriteArr.Length].name;
                i++;
            }

            Debug.Log("apply sprite");
        }

        /**
         * デバッグ用
         */
        void DebugName()
        {
            List<GameObject> spriteRendererList = FindSpriteRenderer(selectedParentObject);
            foreach (GameObject obj in spriteRendererList)
            {
                Debug.Log("child spriteRenderer :" + obj.name);
            }

            Texture2D[] texture2dArr = GetTexture2DListFromSelectedAsset();
            foreach (Texture2D tex in texture2dArr)
            {
                Debug.Log("asset texture2d:" + tex.name);
            }
        }

        /**
         * assetで選択しているオブジェクトのうちTexture2Dを配列で返却する
         */
        Texture2D[] GetTexture2DListFromSelectedAsset()
        {
            var texture2dsortedDict = new SortedDictionary<string, Texture2D>(); // ファイル名順で並べ替えるため
            
            foreach (UnityEngine.Object obj in Selection.objects) // assetで選択しているもの
            {
                if (obj == null) { continue; }
                if (obj.GetType() != typeof(Texture2D)) { continue; }
                texture2dsortedDict.Add(obj.name, (Texture2D)obj);
            }
            var texture2dList = new Texture2D[texture2dsortedDict.Count];
            texture2dsortedDict.Values.CopyTo(texture2dList,0);
            return texture2dList;
        }

        /**
         * 指定GameObjectの子(孫)を探索しSpriteRendererのみリストで返却する
         */
        List<GameObject> FindSpriteRenderer(GameObject targetObject)
        {
            var spriteRendererList = new List<GameObject>();
            if (targetObject == null)
            {
                return spriteRendererList;
            }
            foreach (Transform child in targetObject.GetComponentsInChildren<Transform>()) //子(孫)すべて
            {
                if (null == child.GetComponent<SpriteRenderer>())
                {
                    continue;
                }
                spriteRendererList.Add(child.gameObject);
            }
            return spriteRendererList;
        }


    }
}
 