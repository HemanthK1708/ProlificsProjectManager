using PPM.Domain;

namespace PPM.UiConsole
{
    public class UIConsole
    {
        public static void Title()
        {
            System.Console.WriteLine("--------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--        Welcome to Prolifics Project Manager        --");
            Console.ResetColor();
            System.Console.WriteLine("--------------------------------------------------------");
        }

        public int MenuDriven()
        {
            int switchChoice = 0;
            System.Console.WriteLine("--------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--                   Main Menu Module                 --");
            Console.ResetColor();
            System.Console.WriteLine("--------------------------------------------------------\n");

            System.Console.WriteLine("--           Enter 1. for Project Module              --");
            System.Console.WriteLine("--           Enter 2. for Employee Module             --");
            System.Console.WriteLine("--           Enter 3. for Role Module                 --");
            System.Console.WriteLine("--           Enter 4. to Save State                   --");
            System.Console.WriteLine("--           Enter 5. to Quit                         --");

            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("\nEnter your choice : ");
                switchChoice = int.Parse(Console.ReadLine());
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Exception Occured : " + ex.Message);
            }
            System.Console.WriteLine("\n--------------------------------------------------------");

            return switchChoice;
        }

        public void MenuExit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("--           Exiting the Project Manager              --");
            Console.ResetColor();
            System.Console.WriteLine("--------------------------------------------------------\n");
        }

        public void Default()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("--           Enter a valid choice !!!                --");
            Console.ResetColor();
        }

        public void SaveState()
        {
            SerializeData.SaveState();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\n--   Saved State successfully !!!   --\n");
            Console.ResetColor();
        }
    }
}
