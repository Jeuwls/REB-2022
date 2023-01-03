namespace REB {
    
    public static class Test {

        public static void RunTest() {

            string csvfile = "files" + Path.DirectorySeparatorChar + "log.csv";

            string[] xmls = {"a1t2r1.xml", "a1t2r2.xml" ,"a1t2r3.xml", "a1t2r4.xml", "a1t2r5.xml",
                             "a1t2r6.xml", "a1t2r7.xml", "a1t2r8.xml", "full-dcr-graph.xml"};

            foreach (string xml in xmls) {
                System.Console.WriteLine("Testing DCR graph: {0} vs 'log.csv'", xml);
                string pathxml = "files" + Path.DirectorySeparatorChar +  xml;
                Program.TestGraph(pathxml ,csvfile);
            }
        }
    }
}