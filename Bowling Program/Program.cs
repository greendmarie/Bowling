using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_2
{
    class Program
    {
        //Variables
        static int rollOne;
        static int rollTwo;
        static int[,] scoreCard = new int[11, 2];

        static void Main(string[] args)
        {
            for (int frame = 0; frame < 10; frame++)
            {
                bool badValues = true;

                //Input and validation
                do
                {
                    //User input roll 1
                    Console.WriteLine("Give me roll 1 for frame {0}", frame + 1);
                    bool itsANumber = int.TryParse(Console.ReadLine(), out rollOne);

                    //Is the number valid and the pins dont overflow.
                    if (itsANumber && rollOne <= 10)
                    {
                        badValues = false;
                    }
                    else
                    {
                        Console.WriteLine("You entered a number that passed 10, please enter a different number");
                    }
                }
                while (badValues);
                scoreCard[frame, 0] = rollOne;

                //Strike skip
                if (rollOne == 10)
                {
                    rollTwo = 0;
                }
                else
                {
                    bool badNumbers = true;
                    //Input and validation
                    do
                    {
                        Console.WriteLine("Give me roll 2 for frame {0}", frame + 1);
                        bool itsANumber1 = int.TryParse(Console.ReadLine(), out rollTwo);

                        if (itsANumber1 && rollOne + rollTwo <= 10)
                        {
                            badNumbers = false;
                        }
                        else
                        {
                            Console.WriteLine("You entered a number that passed 10, please enter a different number");
                        }
                    } while (badNumbers);
                }

                //Updating running total and assigning bonus points
                scoreCard[frame, 0] = rollOne;
                scoreCard[frame, 1] = rollTwo;
                scoreCard[10, 0] += rollOne;
                scoreCard[10, 0] += rollTwo;
                BonusPoints(frame);

                //Calling special method for frame ten
                if (frame == 9)
                {
                    FrameTen();
                }

                Console.WriteLine(scoreCard[10, 0]);
            }
            Console.ReadLine();
        }

        //Bonus points for strikes and spares
        static void BonusPoints(int frame)
        {   

            //Spcial case for frame one
            if (frame == 1)
            {
                //strike
                //Consecutive strikes
                if (scoreCard[frame - 1, 0] == 10 && scoreCard[frame, 0] == 10)
                {
                    //adds shot1 to runningTotal
                    scoreCard[10, 0] += scoreCard[frame, 0];
                }
                //One strike
                else if (scoreCard[frame - 1, 0] == 10 && scoreCard[frame, 0] != 10)
                {
                    // last frame strike, current frame is not
                    //adds shot1 + shot2 to runningTotal
                    scoreCard[10, 0] += scoreCard[frame, 0];
                    scoreCard[10, 0] += scoreCard[frame, 1];
                }

                //spare
                else if (scoreCard[frame - 1, 0] + scoreCard[frame - 1, 1] == 10)
                {
                    //add shot1 to runningTotal
                    scoreCard[10, 0] += rollOne;
                }
            }
            //checks 1 frame back
            else if (frame > 1)
            {
                //checks 1 back
                //Consecutive strikes
                if (scoreCard[frame - 1, 0] == 10 && scoreCard[frame, 0] == 10)
                {
                    scoreCard[10, 0] += rollOne;
                }
                // single strike
                // shot1 + shot2 to runningTotal
                else if (scoreCard[frame - 1, 0] == 10 && scoreCard[frame, 0] != 10)
                {
                    scoreCard[10, 0] += rollOne;
                    scoreCard[10, 0] += rollTwo;
                }
                //single spare
                else if (scoreCard[frame - 1, 0] + scoreCard[frame - 1, 1] == 10)
                {
                    //add shot1 to runningTotal
                    scoreCard[10, 0] += rollOne;
                }
                //checks 2 back
                //strike
                if (scoreCard[frame - 2, 0] == 10 && scoreCard[frame - 1, 0] == 10)
                {
                    scoreCard[10, 0] += scoreCard[frame, 0];
                }
            }
        }
        //Special method for final frame bonus rolls
        static void FrameTen()
        {
            bool badValues = true;

            //Checking for strike in current frame
            if (scoreCard[9, 0] == 10)
            {
                // Input and validation for roll one
                do
                {
                    Console.WriteLine("Enter Bonus Roll 1");
                    bool itsANumber = (int.TryParse(Console.ReadLine(), out rollOne));

                    //Is the number valid and the pins dont overflow.
                    if (itsANumber && (rollOne >= 0 && rollOne <= 10))
                    {
                        badValues = false;
                    }
                    else
                    {
                        badValues = true;
                        Console.WriteLine("You entered a number that passed 10, please enter a different number");
                    }
                } while (badValues);

                //Input and validation for roll 2
                do
                {
                    //adds shotTwo to runningTotal
                    Console.WriteLine("Enter Bonus Roll 2");
                    bool itsANumber = (int.TryParse(Console.ReadLine(), out rollTwo));

                    //Is the number valid and the pins dont overflow.
                    if (itsANumber && (rollTwo >= 0 && rollTwo <= 10) && (rollOne + rollTwo <= 20))
                    {
                        badValues = false;
                    }
                    else
                    {
                        badValues = true;
                        Console.WriteLine("You entered a number that passed 10, please enter a different number");
                    }
                }
                while (badValues);

                //Special case for bonus points on previous frame
                if (scoreCard[8, 0] == 10)
                {
                    scoreCard[10, 0] += rollOne;
                }

                //Updating running total
                scoreCard[10, 0] += rollOne;
                scoreCard[10, 0] += rollTwo;
                rollTwo = 0;

            }
            //Check for spare in final frame
            else if (scoreCard[9, 0] + scoreCard[9, 1] == 10)
            {
                //Input and validation for roll one
                do
                {
                    Console.WriteLine("Enter Bonus Roll 1");
                    bool itsANumber = (int.TryParse(Console.ReadLine(), out rollOne));

                    //Is the number valid and the pins dont overflow.
                    if (itsANumber && (rollOne >= 0 && rollOne <= 10))
                    {
                        badValues = false;
                    }
                    else
                    {
                        badValues = true;
                        Console.WriteLine("You entered a number that passed 10, please enter a different number");
                    }
                } while (badValues);

                //Updating running total
                scoreCard[10, 0] += rollOne;
            }
        }
    }
}
