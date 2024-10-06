using UnityEngine;

namespace Nhantt.Level11
{
    public class Level11Envelope : MonoBehaviour
    {

        public void OnDoneStep()
        {
            Debug.Log(" Done Step ");
        }

        public void OnBeginDrag()
        {
            Debug.Log(" On Begin Drag");
        }

        public void OnEndDrag()
        {
            Debug.Log(" On End Drag");

        }


    }
}
