using UnityEngine;
using UnityEngine.UI;

namespace EasyProgressBar {
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    public class ProgressBar : MonoBehaviour {
        private const string ShaderName = "Unlit/Easy Progress Bar";

        private Image _image;
        private Material _material;

        private int _mainTexPropertyID;
        private int _mainColorPropertyID;
        private int _startColorPropertyID;
        private int _endColorPropertyID;
        private int _backColorPropertyID;
        private int _gradientPropertyID;
        private int _roundnessSizePropertyID;
        private int _borderSizePropertyID;
        public int _fillAmountPropertyID;
        private int _sizePropertyID;

        [SerializeField] private Color _startColor = Color.white;
        [SerializeField] private Color _endColor = Color.white;
        [SerializeField] private Color _backColor = Color.black;
        [SerializeField, Range(0f, 1f)] private float _gradient = 0f;
        [SerializeField, Range(0f, 1f)] private float _roundness = 0.5f;
        [SerializeField, Range(0f, 1f)] private float _borderSize = 0.15f;
        [SerializeField, Range(0f, 1f)] public float _fillAmount = 1f;

        private void Awake() {
            CacheShaderPropertyIDs();
            InitializeMaterial();
            UpdateView();
        }

        private void CacheShaderPropertyIDs() {
            _mainTexPropertyID = Shader.PropertyToID("_MainTex");
            _mainColorPropertyID = Shader.PropertyToID("_MainColor");
            _startColorPropertyID = Shader.PropertyToID("_StartColor");
            _endColorPropertyID = Shader.PropertyToID("_EndColor");
            _backColorPropertyID = Shader.PropertyToID("_BackColor");
            _gradientPropertyID = Shader.PropertyToID("_Gradient");
            _roundnessSizePropertyID = Shader.PropertyToID("_Roundness");
            _borderSizePropertyID = Shader.PropertyToID("_BorderSize");
            _fillAmountPropertyID = Shader.PropertyToID("_FillAmount");
            _sizePropertyID = Shader.PropertyToID("_Size");
        }

        private void InitializeMaterial() {
            _image = GetComponent<Image>();
            _material = new Material(Shader.Find(ShaderName));
            _image.material = _material;
        }

        private void OnValidate() {
            UpdateView();
        }

        private void Update() {
            UpdateView();
        }

        private void UpdateView() {
            if (_image != null && _material != null) {
                Texture texture = _material.GetTexture(_mainTexPropertyID);

                if (_image.sprite != null && _image.sprite.texture != texture) {
                    _material.SetTexture(_mainTexPropertyID, _image.sprite.texture);
                } else {
                    _material.SetTexture(_mainTexPropertyID, null);
                }

                _material.SetColor(_mainColorPropertyID, _image.color);
                _material.SetColor(_startColorPropertyID, _startColor);
                _material.SetColor(_endColorPropertyID, _endColor);
                _material.SetColor(_backColorPropertyID, _backColor);
                _material.SetFloat(_gradientPropertyID, _gradient);
                _material.SetFloat(_roundnessSizePropertyID, _roundness);
                _material.SetFloat(_borderSizePropertyID, _borderSize);
                _material.SetFloat(_fillAmountPropertyID, _fillAmount);

                Vector2 scale = transform.lossyScale;
                Rect rect = _image.rectTransform.rect;
                Vector4 size = new Vector4(scale.x * rect.width, scale.y * rect.height, 0, 0);

                _material.SetVector(_sizePropertyID, size);
            }
        }

        private void OnDestroy() {
            if (_material != null) {
                if (Application.isPlaying) {
                    Destroy(_material);
                } else {
                    DestroyImmediate(_material);
                }

                _material = null;
            }
        }
    }
}
