using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplicationCH4
{
    class Class1
    {
        public bool FindMyText(string text)
        {
            bool returnValue = false;

            if (text.Length > 0)
            {
                int indexToText = richTextBox1.Find(text, RichTextBoxFinds.MatchCase);

                if (indexToText >= 0)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
    }
}
