using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyClipboard : MonoBehaviour
{
   public void copy() {
       GUIUtility.systemCopyBuffer = "8f80ebfaf62a8c33ae2adf047572604c74db8bc1daba2b43f9a65635";
       Application.ExternalCall("copy","8f80ebfaf62a8c33ae2adf047572604c74db8bc1daba2b43f9a65635");
   }

}
