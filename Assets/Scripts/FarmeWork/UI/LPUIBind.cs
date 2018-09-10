namespace LuckyPual.UI
{
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// Bind Some Delegate Func For Yours.
    /// </summary>
    public class TTUIBind : MonoBehaviour
    {
        static bool isBind = false;

        public static void Bind()
        {
            if (!isBind)
            {
                isBind = true;
                //Debug.LogWarning("Bind For UI Framework.");

                //bind for your loader api to load UI.
                LPUIPage.delegateSyncLoadUI = Resources.Load;
                //LPUIPage.delegateAsyncLoadUI = UILoader.Load;

            }
        }
    }
}