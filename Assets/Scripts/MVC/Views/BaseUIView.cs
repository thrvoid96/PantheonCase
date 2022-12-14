using UnityEngine;

namespace MVC.Views
{
    /// <summary>
    /// Base class for every UI element and view.
    /// </summary>
    public class BaseUIView : MonoBehaviour
    {
        /// <summary>
        /// Method used to show view or element.
        /// </summary>
        public virtual void ShowView()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Method used to hide view or element.
        /// </summary>
        public virtual void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}