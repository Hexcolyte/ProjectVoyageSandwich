using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoyageSandwich.Shell.UI
{
    public class UIManager : MonoBehaviour
    {
        private Label _scoreLabel;
        private Label _rhythmLabelIndicator;
        private void OnEnable()
        {
            VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            _scoreLabel = rootVisualElement.Q<Label>("Score");
            _rhythmLabelIndicator = rootVisualElement.Q<Label>("RhythmLabelIndicator");
        }

        public void UpdateScoreLabel()
        {
            _scoreLabel.text = "It works!";
        }

        public void SetPerfectLabel()
        {
            StartCoroutine("HideLabel");
            _rhythmLabelIndicator.text = "Perfect";
            _rhythmLabelIndicator.style.color = Color.green;
        }

        public void SetGoodLabel()
        {
            StartCoroutine("HideLabel");
            _rhythmLabelIndicator.text = "Good";
            _rhythmLabelIndicator.style.color = Color.yellow;
        }

        public void SetBadLabel()
        {
            StartCoroutine("HideLabel");
            _rhythmLabelIndicator.text = "Bad";
            _rhythmLabelIndicator.style.color = Color.red;
        }

        private IEnumerator HideLabel()
        {
            yield return new WaitForSeconds(0.2f);
            _rhythmLabelIndicator.text = "";
        }
    }

}
