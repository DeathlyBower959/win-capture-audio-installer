using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace win_capture_audio_installer.Classes
{
    public class Notify
    {
        public static List<Toast> toasts = new List<Toast>();
        public static void Toast(string Title, string Message, int CloseTime = 0)
        {
            var toast = new Toast();
            toasts.Add(toast);

            int maxAmount = (int)Math.Truncate((decimal)Screen.PrimaryScreen.WorkingArea.Height / toast.Height);

            if (toasts.Count > maxAmount)
            {
                toasts[0].isClosing = true;
                toasts[0].timer2.Start();
                toasts.RemoveAt(0);
            }
            toast.ShowToast(Title, Message, CloseTime);
        }
    }
}
