# shuriken_set_sprite
Unity Extending the Editor.

## できること
Assets以下に置いた画像をワールドに配置したSpriteRendererに順番に一括で貼り付けできます。  

## 動作確認環境
Unity 2017.4.15f1

## 導入方法
Editor/ShurikenSetSprite.cs
をEditorフォルダごとUnityのAssets直下にコピーしてください。  
UnityのAssets > (画像ファイル選択し)右クリックメニュー に「Shuriken SetSprite」が追加されます。

## 利用方法
### 1
![2018-12-16 7](https://user-images.githubusercontent.com/45710234/50051602-f030f180-0158-11e9-955e-450489e87f60.png)
Hierarchy右クリック>2D Object>Spriteを押してSpriteを配置(複数)します。　　

### 2
![2018-12-16 10](https://user-images.githubusercontent.com/45710234/50051607-31290600-0159-11e9-9243-e08221ef3fe5.png)
Spriteに指定したい画像をAssetsから選択し、右クリックメニュー >「Shuriken SetSprite」 を押下します。　　

### 3
![2018-12-16 11](https://user-images.githubusercontent.com/45710234/50051611-5c135a00-0159-11e9-84b5-601b5265de13.png)
「対象オブジェクト」に先ほど配置したSpriteの親オブジェクトをD&Dで指定し、「スプライト変更」を押下します。

### 4
![2018-12-16 12](https://user-images.githubusercontent.com/45710234/50051624-87964480-0159-11e9-94d2-2a3e1cae89ad.png)
Spriteに画像が適用されます。
画像名で順番が変わります、日付をファイル名に付けるタイプであれば日付順に並びます。
Spriteの名前は画像名にリネームされます。
