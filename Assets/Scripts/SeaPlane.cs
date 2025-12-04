using UnityEngine;

public class SeaPlane : MonoBehaviour
{
    public Material seaMaterial; // Gán material cần di chuyển
    public float scrollSpeedX = 0.1f; // Tốc độ trục X
    public float scrollSpeedY = 0.0f; // Tốc độ trục Y

    private Vector2 offset;

    void Update()
    {
        if(gameControl.I.CurState != State.PLAYING)
            return;
        // Tăng offset theo thời gian
        offset.x += scrollSpeedX * Time.deltaTime;
        offset.y += scrollSpeedY * Time.deltaTime;

        // Gán offset mới cho material
        seaMaterial.mainTextureOffset = offset;
    }
}
