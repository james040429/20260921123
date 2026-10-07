using UnityEngine;

public class Uvscroll : MonoBehaviour
{
    Material _mat;

    public float _speed = 0.05f;
    //------------------------
    void Awake()
    {
        _mat = GetComponent<Renderer>().material;
    }
    //------------------------
    void Update()
    {
        Vector2 ofs = _mat.mainTextureOffset;
        ofs.y += _speed * Time.deltaTime;

        _mat.mainTextureOffset = ofs;
    }
    //------------------------

}// public class Sky : MonoBehaviour
 //==========================================================