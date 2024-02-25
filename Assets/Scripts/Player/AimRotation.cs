using UnityEngine;

public class AimRotation : MonoBehaviour
{
    public Transform target; // Đối tượng cha
    public float speed = 5f; // Tốc độ di chuyển
    public float radius = 2f; // Bán kính

    private float angle = 0f; // Góc quay

    private void Update()
    {
        // Tính toán vị trí mới của đối tượng con
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;

        // Đặt vị trí mới của đối tượng con
        transform.position = target.position + new Vector3(x, y, 0f);

        // Tăng góc quay
        angle += speed * Time.deltaTime;
    }
}