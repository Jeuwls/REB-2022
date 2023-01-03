# REB-2022
REB Conformance Checker

**How to run:**
    - This Conformance Checker is written in C# so make sure you have dotnet installed
    - Run dotnet build in the program folder 
    - Use dotnet run + two arguments to start the programs
        * argument 1: a path to your xml-file 
        * arguemnt 2: a path to your csv-file 
    - OR use dotnet run without arguments to run 
    
**Example**
*user@user/program> dotnet build*
*user@user/program> dotnet run files/assingment-1-xml files/dreyers-log.csv*

**Testing**


**Output**
The program outputs a line in your terminal stating: 
    *Valid traces: x, Invalid traces: y*