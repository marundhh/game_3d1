using UnityEngine;
using UnityEngine.SceneManagement; // Để chuyển scene khi cần
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject loseMenu; // Kéo UI Panel menu thua vào đây
    public Text messageText;    // Text để hiển thị "You Lose" hoặc "You Win"

    private bool isGamePaused = false;

    void Start()
    {
        // Đảm bảo menu được ẩn khi bắt đầu game
        if (loseMenu != null)
        {
            loseMenu.SetActive(false);
        }
    }

    // Gọi hàm này khi người chơi thua
    public void PlayerLose(string message)
    {
        // Hiển thị thông báo
        if (messageText != null)
        {
            messageText.text = message;
        }

        // Hiển thị menu
        if (loseMenu != null)
        {
            loseMenu.SetActive(true);
        }

        // Dừng game
        PauseGame();
    }

    // Hàm dừng game
    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Dừng toàn bộ thời gian trong game
    }

    // Hàm tiếp tục game (nếu cần)
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Khôi phục thời gian trong game
    }

    // Nút Retry
    public void RetryGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại scene hiện tại
    }

    // Nút Quit hoặc về menu chính
    public void QuitToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu"); // Đặt tên scene menu chính
    }
}
 