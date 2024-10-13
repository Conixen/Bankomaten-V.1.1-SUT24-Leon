namespace Bankomaten_V._1._1_SUT24_Leon
{
    internal class Program
    {                       // LEON SUT24
        static void Main(string[] args)
        {   // Meny boolean for meny and nummeber of failed attempts
            bool logInMeny = true;
            int attempts = 1;
            bool checkLogin = true;

            // Array for login: [0] = ID, [1] = full name, [2] = PIN
            string[,] users = new string[5, 3];
       
            users[0, 0] = "lejo";
            users[0, 1] = "leon Johansson";
            users[0, 2] = "1235";

            users[1, 0] = "hani970722";
            users[1, 1] = "hampus nilsson";
            users[1, 2] = "1236";

            users[2, 0] = "dasa970307";
            users[2, 1] = "david sandholm";
            users[2, 2] = "1237";

            users[3, 0] = "embe970816";
            users[3, 1] = "emil bergström";
            users[3, 2] = "1238";

            users[4, 0] = "jegu970201";
            users[4, 1] = "Jesper Gustavsson";
            users[4, 2] = "1239";
            // Array for the users diffrent types of accounts
            string[][] accounts = 
            {
                new string[] { "Privatkonto", "Sparkonto", "Lönekonto"},
                new string[] { "Privatkonto", "Sparkonto"},
                new string[] { "Semesterkonto", "Sparkonto"},
                new string[] { "Privatkonto", "Sparkonto", "Semesterkonto"},
                new string[] { "Sparkonto"},

            };
            // array with the amount of money in each account
            decimal[][] currency =
            {
                new decimal [] { 5000, 50000, 20000},
                new decimal [] { 500, 100000},
                new decimal [] { 1200, 20000},
                new decimal [] { 2000, 30000, 42400},
                new decimal [] { 75000},
            };

            // Main loop 
            while (logInMeny)
            {   // Main meny
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Välkomen till Bankomaten SUT24");
                Console.WriteLine("\nSkriv in ditt användar-ID:");
                Console.WriteLine("skriv `exit` för att avsluta");
                Console.WriteLine("-------------------------------");
                string givenId = Console.ReadLine().ToLower();

                // If the user wanna turn the program off
                if (givenId == "exit") 
                {
                    Console.WriteLine("Programmet avslutas");
                    logInMeny = false;
                    break;
                }

                // Check if the entered user ID exists in the system
                int userIndex = CheckUser(users, givenId);
                if (userIndex != -1)
                {                   
                    // If user ID exists, ask for PIN
                    Console.WriteLine("\nSkriv in din Pin:");
                    string givenPin = Console.ReadLine();

                    if (givenPin == users[userIndex,2])
                    {   // Correct user ID and Pin
                        bool mainMeny = true;
                        while (mainMeny)
                        {
                            // Main meny loop
                            Console.Clear();
                            Console.WriteLine($"Välkommen {users[userIndex, 1]}" +
                                "\nVälj ett av de förjande alternativen");
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine("\n1. Visa dina konton och saldo:" +
                                "\n2. Överförning mellan konton " +
                                "\n3. Ta ut pengar" +
                                "\n4. Avsluta");
                            Console.WriteLine("-------------------------------");
                            // Read and process the users choice
                            try 
                            {
                                int choice;
                                choice = Convert.ToInt32(Console.ReadLine());
                                switch (choice)
                                {
                                    case 1: // Show account infromation
                                        ShowAccountInfo(userIndex, accounts, currency, users);
                                        break;
                                    case 2:  // Transfer between accounts
                                        Transfer(userIndex, accounts, currency);
                                        break;
                                    case 3: // Withdraw money
                                        Withdraw(userIndex, accounts, currency);
                                        break;
                                    case 4: //Logut user
                                        Console.WriteLine($"Hej då {users[userIndex, 1]}, du loggas nu ut!");
                                        mainMeny = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    default:    // Invaild input
                                        Console.WriteLine("Inte rätt inmatning testa igen main meny");
                                        Console.ReadKey();
                                        break;
                                }
                            }
                            catch (FormatException)
                            {                   
                                // Error handling for invalid number input
                                Console.WriteLine("Fel: Ogiltig inmatning. Vänligen ange ett nummer mellan 1 och 4.");
                                Console.ReadKey(); 
                            }  
                        }
                    }
                    else if (attempts == 3)
                    {
                        // If user fails to enter correct PIN 3 times, the program exits
                        Console.WriteLine("För många misslyckade försök programmet stängs av Pin meny");
                        Console.ReadKey();
                        logInMeny = false;
                    }
                    else
                    {   // Invalid input message
                        Console.WriteLine("Något blev fel Pin meny");
                        attempts++;
                        Console.Clear();
                    }
                }
                else if (attempts == 3)
                {
                    // If user fails to enter correct PIN 3 times, the program exits
                    Console.WriteLine("För många misslyckade försök programmet stängs av login meny");
                    Console.ReadKey();
                    logInMeny = false;
                }
                else
                {   // Invalid input
                    Console.WriteLine("Något blev fel testa igen login meny");
                    Console.ReadKey();
                    attempts++;
                    Console.Clear();
                }
            }
        }
        // Method to check if the user ID exists
        static int CheckUser(string[,] users, string givenId)
        {
            for (int i = 0; i < users.GetLength(0); i++)
            {             
                // If the entered ID matches any user in the array, return the index
                if (users[i, 0].ToLower() == givenId)
                {
                    return i;
                }
            }   // Return -1 if the user ID is not found
            return -1;
        }
        // Method to display user's accounts and their balances
        static void ShowAccountInfo(int userIndex, string[][] accounts, decimal[][] currency, string[,]users) 
        {
            Console.WriteLine("Dina konton:");
            for (int i = 0; i < accounts[userIndex].Length; i++) 
            {
                string thierAccount = accounts[userIndex][i];
                decimal thierCurrency = currency[userIndex][i];
                Console.WriteLine($"{ users[userIndex, 1 ]}, { thierAccount }: { thierCurrency } kr");
            }
            Console.WriteLine("\nKlicka enter för att komma till huvudmenyn");
            Console.ReadKey();
        }
        // Method to transfer money between different accounts
        static decimal[][] Transfer(int userIndex, string[][] accounts, decimal[][] currency)
        {
            Console.WriteLine("Dina konton:");
            for (int i = 0; i < accounts[userIndex].Length; i++)
            {
                string thierAccount = accounts[userIndex][i];
                decimal thierCurrency = currency[userIndex][i];
                Console.WriteLine($"\n{thierAccount}:  {thierCurrency}kr");
            }
            Console.WriteLine("-------------------------------");

            // Choose account to transfer money from
            Console.WriteLine("Välj konto att flytta pengar från (ange nummer):");
            int fromAccount;

            if (int.TryParse(Console.ReadLine(), out fromAccount) && fromAccount >= 0 && fromAccount < accounts[userIndex].Length)
            {
                Console.WriteLine("Välj konto att flytta pengar till (ange nummer):");
                int toAccount;

                // Choose account to transfer money to
                if (int.TryParse(Console.ReadLine(), out toAccount) && toAccount >= 0 && toAccount < accounts[userIndex].Length)
                {
                    if (fromAccount == toAccount)
                    {  // Check if user selected the same account for transfer
                        Console.WriteLine("Du kan inte flytta pengar mellan samma konto.");
                        return currency;
                    }
                    else
                    {
                        // Ask the amount to transfer
                        Console.WriteLine("Hur mycket vill du flytta?");
                        decimal amount;
                        // Kontrollera att beloppet är korrekt och gör överföringen
                        if (decimal.TryParse(Console.ReadLine(), out amount))
                        {
                            if (amount <= 0)
                            {
                                Console.WriteLine("Beloppet måste vara större än 0.");
                                Console.ReadKey();
                                return currency;
                            }
                            else if (amount > currency[userIndex][fromAccount])
                            {
                                // If transfer amount is more than available balance
                                Console.WriteLine($"Överföring misslyckades! Du har endast {currency[userIndex][fromAccount]} kr på ditt {accounts[userIndex][fromAccount]}.");
                                Console.ReadKey();
                                return currency;
                            }
                            else
                            {
                                // Perform the transfer
                                currency[userIndex][fromAccount] -= amount;
                                currency[userIndex][toAccount] += amount;

                                Console.WriteLine($"Överföring lyckades! {amount} kr har flyttats från {accounts[userIndex][fromAccount]} till {accounts[userIndex][toAccount]}.");
                                Console.WriteLine($"Nytt saldo på {accounts[userIndex][fromAccount]}: {currency[userIndex][fromAccount]} kr.");
                                Console.WriteLine($"Nytt saldo på {accounts[userIndex][toAccount]}: {currency[userIndex][toAccount]} kr.");
                                Console.ReadKey();
                                return currency;
                            }
                        }
                        else
                        {                        
                            // Invalid transfer amount handling
                            Console.WriteLine("Ogiltigt belopp.");
                            Console.ReadKey();
                            return currency;
                        }
                    }
                }
                else
                {
                    // Invalid account selection
                    Console.WriteLine("Ogiltigt konto val.");
                    Console.ReadKey();
                    return currency;
                }
            }
            else
            {
                // Invalid account selection
                Console.WriteLine("Ogiltigt konto val.");
                Console.ReadKey();
                return currency;
            }
        }
        // Method to withdraw money
        static decimal[][] Withdraw(int userIndex, string[][] accounts, decimal[][] currency) 
        {
            Console.WriteLine("Dina konton:");
            for (int i = 0; i < accounts[userIndex].Length; i++)
            {
                string thierAccount = accounts[userIndex][i];
                decimal thierCurrency = currency[userIndex][i];
                Console.WriteLine($"\n{ thierAccount }:  { thierCurrency }kr");
            }
            Console.WriteLine("-------------------------------");
            Console.WriteLine("\nVälj vilket konto du vill ta ut pengar ifrån?" +
                "\nMellan 0-2");
            int accountWithdraw; 

            if (int.TryParse(Console.ReadLine(), out accountWithdraw) && accountWithdraw <= accounts.Length)
            {               
                // Ask for the withdrawal amount
                Console.WriteLine("\nHur mycket vill du ta ut?");
                decimal amount;
                if (decimal.TryParse(Console.ReadLine(), out amount)) 
                { 
                    if (amount <= 0)    // Amount should be more than 0
                    {
                        Console.WriteLine("\nBeloppet måste vara mer än 0");
                        Console.ReadKey();
                        return currency;
                    }
                    else if (amount > currency[userIndex][accountWithdraw]) 
                    {
                        // If the withdrawal amount is more than the available balance
                        Console.WriteLine($"\nUttag misslyckades! Du har endast " +
                            $"{currency[userIndex][accountWithdraw]} kr på ditt " +
                            $"{accounts[userIndex][accountWithdraw]}.");
                        Console.ReadKey();
                        return currency;
                    }
                    else 
                    {
                        // Successfully withdraw the money
                        currency[userIndex][accountWithdraw] -= amount;
                        Console.WriteLine($"\nUttag lyckades! Du har nu {currency[userIndex][accountWithdraw]} " +
                            $"kr kvar på ditt {accounts[userIndex][accountWithdraw]}.");
                        Console.ReadKey();
                        return currency;
                    }
                }
                else 
                {
                    Console.WriteLine("\nOgiltigt belopp");
                    Console.ReadKey();
                    return currency;
                }
            }   
            else 
            {
                Console.WriteLine("\nNågot blev fel withdraw konton");
                Console.ReadKey();
                return currency;
            }
            return currency;
            Console.ReadKey();
            Console.WriteLine("\nKlicka enter för att komma till huvudmenyn");
        }
            
            
        }
    }               // Vad gör du här nere och kollar finns inget mer här... eller?
