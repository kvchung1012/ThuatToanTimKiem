using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindWayTesst.Solve
{
    public  class PrintTable
    {
        public StreamWriter writetext;
        public PrintTable(string path)
        {
            writetext = new StreamWriter(path);
        }

        public  int SIZE = 80;
        public  int SIZE_L = 200;
        public  void Write(string str)
        {
            writetext.Write(str);
        }

        public  void PrintLine()
        {
            writetext.WriteLine(new string('-', SIZE + (SIZE_L - SIZE) / 7));
        }

        public  void PrintRow(string[] columns)
        {
            int width = (SIZE - columns.Length) / columns.Length;
            string row = "|";
            int index = 0;
            foreach (string column in columns)
            {
                if (index == columns.Length - 1)
                {
                    width = (SIZE_L - columns.Length) / columns.Length;
                }
                index++;
                row += AlignCentre(column, width) + "|";
            }

            writetext.WriteLine(row);
        }

        public  string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        public void Close()
        {
            writetext.Close();
        }
    }
}
