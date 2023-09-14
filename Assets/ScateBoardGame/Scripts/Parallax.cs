using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    float distance;

    [Range(0, 0.5f)]
    public float speed = 0.3f;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.GAMERUNNING)
        {
            distance += Time.deltaTime * speed;
            mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        }
        
    }
}
