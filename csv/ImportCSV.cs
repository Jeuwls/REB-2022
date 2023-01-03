using System.IO;

namespace REB{
    public static class ImportCSV {
        public static string[,] Import (string path) {

        string[] lines = System.IO.File.ReadAllLines(path);

        // List<(string,string,string)> FormattedLines = new List<(string,string,string)>();

        string[,] result = new string[lines.Count(),3];
        for (int i = 0; i < lines.Count(); ++i) {
                string[] columns = lines[i].Split(';');
                result[i,0] = columns[0].ToLower();
                result[i,1] = columns[2].ToLower();
                result[i,2] = columns[4].ToLower();
            }
        System.Console.WriteLine(lines);
        return result;
        }       
    }
}


