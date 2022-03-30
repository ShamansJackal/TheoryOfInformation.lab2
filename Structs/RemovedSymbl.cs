namespace TheoryOfInformation.lab1.Structs
{
    public struct RemovedSymbl
    {
        public int index;
        public char symble;

        public RemovedSymbl(int ind, string str)
        {
            index = ind;
            symble = str[ind];
        }

        public override string ToString()
        {
            return $"{index}:{symble}";
        }
    }
}
