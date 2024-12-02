using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // Menu tạm dừng

    private bool isPaused = false; // Trạng thái tạm dừng

    void Update()
    {
        // Kiểm tra khi nhấn phím P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume(); // Tiếp tục game
            }
            else
            {
                Pause(); // Tạm dừng game
            }
        }

        // Điều chỉnh trạng thái con trỏ chuột theo menu
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true); // Hiển thị menu
        Time.timeScale = 0; // Dừng thời gian game
        isPaused = true; // Cập nhật trạng thái tạm dừng
    }

    public void Resume()
    {
        pauseMenu.SetActive(false); // Ẩn menu
        Time.timeScale = 1; // Tiếp tục thời gian game
        isPaused = false; // Cập nhật trạng thái
    }

    public void Home()
    {
        Time.timeScale = 1; // Đặt lại thời gian bình thường
        SceneManager.LoadScene("Main Menu"); // Chuyển về menu chính
    }

    public void Restart()
    {
        Time.timeScale = 1; // Đặt lại thời gian bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart scene hiện tại
    }
}