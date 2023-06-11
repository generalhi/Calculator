namespace Calculator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string text;
                if (args.Length > 0)
                {
                    text = args[0];
                }
                else
                {
#if DEBUG
                    //text = " 2,222+2-3,1 *2";
                    //text = " 2.222+2-3.1 *2";
                    //text = " 2+2(2+2)2";
                    //text = "3+4*2/(1-5)+2"; // 3
                    //text = "2+2"; // 4
                    //text = "2+2*2"; // 6
                    //text = "2+2/2"; // 3
                    //text = "(2+2)2+(2+2)/2"; // 10
                    text = "(2+(2+(2(2+(2+2)))))2+(2+2)/2"; // 34
#endif
                }

                var errors = new Errors();
                var parser = new Parser();
                var calculator = new Calculator();
                var result = calculator.Run(text, parser, errors);

                if (result == null)
                {
                    if (errors.IsPresent)
                    {
                        errors.Print();
                    }
                }
                else
                {
                    Console.WriteLine($"Result = {result}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
