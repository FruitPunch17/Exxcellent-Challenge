//WEATHER CHALLENGE
solveProblem("weather.csv", 0, 1, 2);

//FOOTBALL CHALLENGE
solveProblem("football.csv", 0, 5, 6);

//this method solves the given problem, it gets the data from the file with name "filename" and finds the index where the minimum difference between the two columns col1 and col2 is attained
//and outputs the entry that lies in the row of this argmin and in the column of nameCol
void solveProblem(string filename, int nameCol, int col1, int col2)
{
    //get data and stop if the returned value is an empty array
    string[][] data = extractDataFromCSV(filename);
    if (data == Array.Empty<string[]>()) { return; }

    //these are the columns corresponding to maxtemp and mintemp (respectively goals and goals allowed) in the data sheet where the first row has been removed since it contains the title
    //if any entry of one of these columns is not convertible to an integer the method is stopped
    int[] array1;
    int[] array2;
    try
    {
        array1 = (from i in Enumerable.Range(1, data.Length - 1) select Int32.Parse(data[i][col1])).ToArray();
        array2 = (from i in Enumerable.Range(1, data.Length - 1) select Int32.Parse(data[i][col2])).ToArray();
    }
    catch (System.FormatException)
    {
        Console.WriteLine("A column contained a string that cannot be converted to an integer.");
        return;
    }

    //find argmin and stop if it was not calculated properly because of faulty inputs
    int arg_min = findArgMinAbsoluteDifferenceArrays(array1, array2);
    if (arg_min == -1) { return; }

    //output solution, note that 1 is added to the argmin because the first row needs to be skipped
    Console.WriteLine(data[arg_min + 1][nameCol]);
}

//reads the csv file with name "filename" and returns a 2d array with the data, returns an empty 2d array if file does not exist
string[][] extractDataFromCSV(string filename)
{
    //check if file exists
    if (!File.Exists(filename))
    {
        Console.WriteLine("File does not exist.");
        return Array.Empty<string[]>();
    }

    //read file and split it into lines
    string allData = System.IO.File.ReadAllText(filename);
    string[] rows = allData.Split(new char[] { '\n' });

    //split the rows into single entries and write them into the 2d array "result"
    string[][] result = new string[rows.Length][];
    for (int i = 0; i < rows.Length; i++)
    {
        string[] row_entries = rows[i].Split(new char[] { ',' });
        result[i] = row_entries;
    }

    return result;
}

//finds minimum of absolute value of difference of two integer arrays, returns the index where minimum is attained (argmin),
//returns -1 if either input array is empty or if they do not have the same length
int findArgMinAbsoluteDifferenceArrays(int[] array1, int[] array2)
{
    //check if one of the input arrays is empty or they do not have the same length
    if (array1.Length == 0 || array2.Length == 0 || array1.Length != array2.Length)
    {
        Console.WriteLine("At least one of the arrays is empty or they do not have the same length.");
        return -1;
    }

    //initialize minimum as difference of the first entries of the two input arrays
    int min = Math.Abs(array1[0] - array2[0]);
    int arg_min = 0;

    //check all following differences and update minimum and argmin if smaller difference is found
    for (int i = 1; i < array1.Length; i++)
    {
        int x = Math.Abs(array1[i] - array2[i]);
        if (x < min)
        {
            min = x;
            arg_min = i;
        }
    }
    return arg_min;
}

//methods for testing
void printTable(string[][] data)
{
    for (int i = 0; i < data.Length; i++)
    {
        for (int j = 0; j < data[i].Length; j++)
        {
            Console.Write(data[i][j] + ",");
        }
        Console.WriteLine();
    }
}

void testMin()
{
    int[] randArray1 = (from i in Enumerable.Range(1, 20) select new Random().Next(-100, 100)).ToArray();
    int[] randArray2 = (from i in Enumerable.Range(1, 20) select new Random().Next(-100, 100)).ToArray();
    for (int i = 0; i < randArray1.Length; i++)
    {
        Console.WriteLine(randArray1[i] + " " + randArray2[i]);
    }
    Console.WriteLine(findArgMinAbsoluteDifferenceArrays(randArray1, randArray2));
}


