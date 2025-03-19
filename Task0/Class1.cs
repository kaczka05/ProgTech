namespace DataLayer
{
    public class Numbers
    {
       private int num_1;
       private int num_2;


        public void setNum_1(int x)
        {
            num_1 = x;
        }

        public void setNum_2(int x)
        {
            num_2 = x;
        }

        public int GetNum1() { return num_1; }
        public int GetNum2() { return num_2; }
    }
}

namespace LogicLayer
{
    using DataLayer;
    
    public class TaskLogic
    {
        private Numbers num;
        public TaskLogic() 
        { 
            num = new Numbers(); 
        }

        public void SetNumbers(int x, int y)
        {
            num.setNum_1(x);
            num.setNum_2(y);
            
        }

        public int AddNumbers()
        {
            return num.GetNum1() + num.GetNum2();
        }

        public int SubstractNumbers()
        {
            return num.GetNum1() - num.GetNum2();
        }

        public int MultiplyNumbers()
        {
            return num.GetNum1() * num.GetNum2();
        }

        public int DivideNumbers()
        {
            if(num.GetNum2()!=0)
            {
                return num.GetNum1() / num.GetNum2();
            }
            else
            {
                return 0;
            }

        }
    }
}





