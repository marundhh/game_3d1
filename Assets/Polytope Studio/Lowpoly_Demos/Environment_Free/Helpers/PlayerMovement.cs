using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển

    void Update()
    {
        // Nhận input từ bàn phím
        float moveX = Input.GetAxis("Horizontal"); // A/D hoặc mũi tên trái/phải
        float moveZ = Input.GetAxis("Vertical");   // W/S hoặc mũi tên lên/xuống

        // Tạo vector di chuyển
        Vector3 move = new Vector3(moveX, 0, moveZ);

        // Di chuyển nhân vật
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
