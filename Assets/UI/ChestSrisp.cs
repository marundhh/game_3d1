using UnityEngine;
using UnityEngine.EventSystems;

public class ChestScript : MonoBehaviour
{
    public GameObject winnerText;

    void Start()
    {
        if (winnerText != null)
        {
            winnerText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winnerText != null)
            {
                winnerText.SetActive(true); // Hiện bảng Winner
            }

            PauseGame();
            Debug.Log("Winner!");
        }
    }

    private void Update()
    {
        // Kiểm tra nếu chuột đang ở trên UI thì bỏ qua mọi hành động
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Dừng game
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
