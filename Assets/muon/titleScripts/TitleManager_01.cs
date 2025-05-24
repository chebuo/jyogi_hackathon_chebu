using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理のために必要
using UnityEngine.UI; // Button, Image, Textを使うために必要
using System.Collections.Generic; // Listを使うために必要

public class TitleManager_01 : MonoBehaviour // クラス名をTitleManager_01に変更
{
    [Header("■ ボタン設定")]
    [Tooltip("スタートボタンをここに設定します。")]
    public Button startButton;
    [Tooltip("遊び方ボタンをここに設定します。")]
    public Button howToPlayButton;

    [Header("■ シーン設定")]
    [Tooltip("ゲームプレイシーンの名前を設定します。（例: MainScene）")]
    public string gamePlaySceneName = "MainScene"; // 実際のゲームプレイシーン名

    // --- 遊び方ギャラリー設定 ---
    [Header("■ 遊び方ギャラリー設定")]
    [Tooltip("遊び方ギャラリーの全体パネルをここに設定します。")]
    public GameObject howToPlayPanel; // 遊び方ギャラリー全体を覆うパネル
    [Tooltip("遊び方画像を表示するImageコンポーネントをここに設定します。")]
    public Image howToPlayImage; // 遊び方画像を表示するImage
    [Tooltip("次の画像へ進むボタンをここに設定します。")]
    public Button nextImageButton; // 次の画像へ
    [Tooltip("前の画像へ戻るボタンをここに設定します。")]
    public Button previousImageButton; // 前の画像へ
    [Tooltip("遊び方ギャラリーを閉じるボタンをここに設定します。")]
    public Button closeHowToPlayButton; // ギャラリーを閉じるボタン

    [Tooltip("遊び方として表示する画像のリストをここに設定します。")]
    public List<Sprite> howToPlaySprites; // 遊び方画像のリスト

    private int currentImageIndex = 0; // 現在表示中の画像のインデックス
    // --- 遊び方ギャラリー設定ここまで ---


    void Start()
    {
        // ボタンが設定されていれば、クリックイベントを登録
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        }

        // 遊び方ギャラリーのボタンイベント登録
        if (nextImageButton != null)
        {
            nextImageButton.onClick.AddListener(OnNextImageButtonClicked);
        }
        if (previousImageButton != null)
        {
            previousImageButton.onClick.AddListener(OnPreviousImageButtonClicked);
        }
        if (closeHowToPlayButton != null)
        {
            closeHowToPlayButton.onClick.AddListener(OnCloseHowToPlayButtonClicked);
        }

        // 遊び方ギャラリーパネルは最初非表示
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }
    }

    // スタートボタンが押された時の処理
    void OnStartButtonClicked()
    {
        Debug.Log("スタートボタンが押されました！");
        SceneManager.LoadScene(gamePlaySceneName);
    }

    // 遊び方ボタンが押された時の処理
    void OnHowToPlayButtonClicked()
    {
        Debug.Log("遊び方ボタンが押されました！");
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true); // 遊び方ギャラリーを表示
            currentImageIndex = 0; // 最初の画像から開始
            UpdateHowToPlayImage(); // 画像を更新
        }
    }

    // 次の画像へ進むボタンが押された時の処理
    void OnNextImageButtonClicked()
    {
        Debug.Log("次へボタンが押されました！");
        if (howToPlaySprites != null && howToPlaySprites.Count > 0)
        {
            currentImageIndex = (currentImageIndex + 1) % howToPlaySprites.Count; // 次の画像へ（ループ）
            UpdateHowToPlayImage();
        }
    }

    // 前の画像へ戻るボタンが押された時の処理
    void OnPreviousImageButtonClicked()
    {
        Debug.Log("戻るボタンが押されました！");
        if (howToPlaySprites != null && howToPlaySprites.Count > 0)
        {
            currentImageIndex--;
            if (currentImageIndex < 0)
            {
                currentImageIndex = howToPlaySprites.Count - 1; // 前の画像へ（ループ）
            }
            UpdateHowToPlayImage();
        }
    }

    // 閉じるボタンが押された時の処理
    void OnCloseHowToPlayButtonClicked()
    {
        Debug.Log("閉じるボタンが押されました！");
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false); // 遊び方ギャラリーを非表示
        }
    }

    // 表示中の画像を更新する内部メソッド
    void UpdateHowToPlayImage()
    {
        if (howToPlayImage != null && howToPlaySprites != null && howToPlaySprites.Count > 0)
        {
            howToPlayImage.sprite = howToPlaySprites[currentImageIndex]; // 現在のインデックスの画像を表示
        }
    }

    // シーンが破棄される前にイベントリスナーを解除する（メモリリーク防止のため推奨）
    void OnDestroy()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(OnStartButtonClicked);
        }
        if (howToPlayButton != null)
        {
            howToPlayButton.onClick.RemoveListener(OnHowToPlayButtonClicked);
        }
        if (nextImageButton != null)
        {
            nextImageButton.onClick.RemoveListener(OnNextImageButtonClicked);
        }
        if (previousImageButton != null)
        {
            previousImageButton.onClick.RemoveListener(OnPreviousImageButtonClicked);
        }
        if (closeHowToPlayButton != null)
        {
            closeHowToPlayButton.onClick.RemoveListener(OnCloseHowToPlayButtonClicked);
        }
    }
    public void IMOKESU()
    {
        Destroy(GameObject.Find("haikei_01"));
    }
}