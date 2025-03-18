namespace DataLayer
{
    public class Numbers
    {
       private int num_1;
       private int num_2;

    }
}

namespace LogicLayer
{
    public class TaskLogic
    {
        public int AddNumbers(int a, int b) 
        {
            return a + b;
        }
        
        public int SubstractNumbers(int a, int b)
        {
            return a - b;
        }

        public int MultiplyNumbers(int a, int b)
        {
            return a * b;
        }

        public int DivideNumbers(int a, int b)
        {
            if(b!=0)
            {
                return a / b;
            }
            else
            {
                return 0;
            }

        }
    }
}





