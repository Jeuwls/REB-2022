using System.IO;
using System;


namespace Breakout.Level {
        /// <summary>
        /// This static class will read the information of a level file,
        /// and convert it to a string array
        /// </summary>
    public static class FileReader {  

        /// <summary>
        /// Reads a text file, and converts it to a string array.
        /// If the file does not exit, an exception is thrown.
        /// </summary>
        /// <param name="level"> A text file representet as a string </param>
        /// <returns> 
        /// a string array, read from the file 
        /// </returns>
        public static string[] ReadFile(string level) {
            try {
                System.IO.File.ReadAllLines(level);
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            }
            
            string[] lines = System.IO.File.ReadAllLines(level);

            return lines;
        }
    }
}