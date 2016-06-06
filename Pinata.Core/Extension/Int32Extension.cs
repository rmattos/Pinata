namespace Pinata
{
    public static class Int32Extension
    {
        public static int XOR(this int numOne, int numTwo)
        {
            return numOne ^ numTwo;
        }

        public static int AND(this int numOne, int numTwo)
        {
            return numOne & numTwo;
        }
    }
}
