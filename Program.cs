using System;
using System.Data;

using Dapper;
using Microsoft.Data.SqlClient;

namespace ComputerStore{
    public class MyComputer{
        public static void Main(string[] args){
            string connectionString = "Server=localhost;Database=<YOUR_DB_NAME>;Trusted_Connection=false;TrustServerCertificate=true;User Id=<YOUR_USERNAME>;Password=<YOUR_PASSWORD>;"; 
            SqlConnection DBConnection = new SqlConnection(connectionString);

            try {

                Console.WriteLine("[1] - Insert a new computer");
                Console.WriteLine("[2] - Remove a existent computer");
                Console.WriteLine("[3] - Update a existent computer");
                Console.WriteLine("[4] - Show all or a spefic computer");
                Console.WriteLine("[0] - Exit the program");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 0){
                    Console.WriteLine("[-] Bye!");
                    Environment.Exit(0);

                }else if (choice == 1){
                    Computer newComputer = new Computer();

                    Console.Write("[-] Please Specify your motherboard: ");
                    newComputer.Motherboard = Console.ReadLine();
                    Console.WriteLine("");

                    try{
                        Console.Write("[-] Please Specify how many cores will have on your CPU: ");
                        newComputer.CPUCores = int.Parse(Console.ReadLine());
                        Console.WriteLine("");

                    }catch (FormatException){
                        Console.WriteLine("[-] Please enter with a integer number.");
                        Environment.Exit(1);

                    }catch (ArgumentNullException) {
                        Console.WriteLine("[-] Please enter with a integer number.");
                        Environment.Exit(1);

                    }

                    newComputer.ReleaseDate = DateTime.Now;


                    try {
                        Console.Write("[-] Please Specify your budget: ");
                        newComputer.Price = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("");

                    }catch (FormatException){
                        Console.WriteLine("[-] Please enter with a decimal number.");
                        Environment.Exit(1);

                    }catch (ArgumentNullException) {
                        Console.WriteLine("[-] Please enter with a decimal number.");
                        Environment.Exit(1);

                    }
                    

                    bool response = Operations.InsertComputer(DBConnection, newComputer);

                    if (response == true){
                        Console.WriteLine("[+] Computer inserted.");
                        Environment.Exit(0);

                    }else {
                        Console.WriteLine("[-] Something went wrong.");
                        Environment.Exit(0);

                    }

                }else if (choice == 2){
                    
                    try {
                        Console.Write("[-] Please digit the ID of the computer: ");
                        int idPC = int.Parse(Console.ReadLine());
            
                        bool response = Operations.RemoveComputer(DBConnection, idPC);
                    
                        if (response == true){
                            Console.WriteLine("[+] Computer deleted.");
                            Environment.Exit(0);

                        }else {
                            Console.WriteLine("[-] Something went wrong.");
                            Environment.Exit(1);

                        }   

                    }catch (FormatException){
                        Console.WriteLine("[-] Please enter with a int number.");
                        Environment.Exit(1);

                    }catch (ArgumentNullException) {
                        Console.WriteLine("[-] You need to enteder with a int number to delete a Computer.");
                        Environment.Exit(1);

                    }

                }else if (choice == 3){

                    try {
                        Console.Write("[-] Type the id of a computer inside on Database:");
                        int idPcUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine("");

                        Console.WriteLine("What field do you want update?");
                        Console.WriteLine("[1] - Motherboard");
                        Console.WriteLine("[2] - CPUCores");
                        Console.WriteLine("[3] - Release Date");
                        Console.WriteLine("[4] - Price");

                        int fieldUpdate = int.Parse(Console.ReadLine());
                        
                        if (fieldUpdate > 4){
                            Console.WriteLine("[-] Please type e valid option.");
                            Environment.Exit(1);

                        }else {

                            Console.Write("[-] Please digit a new value, if will be a date use the follow specifics dd/MM/yyyy HH:mm::ss: ");
                            dynamic informationNew = Console.ReadLine();
                            Console.WriteLine("");

                            bool response = Operations.UpdateComputer(DBConnection, idPcUpdate, fieldUpdate, informationNew);
                            
                            if (response == true){
                                Console.Write("[+] Field will update");
                                Environment.Exit(0);

                            }else {
                                Environment.Exit(1);

                            }

                        }
                       
                    } catch (ArgumentNullException) {
                        Environment.Exit(1);

                    } catch (FormatException){
                        Environment.Exit(1);

                    }  
                    
                }else if (choice == 4){
                     try {

                        Console.WriteLine("[1] - To Show All");
                        Console.WriteLine("[2] - To Show Specific, type the ID ");

                        int HowShow = int.Parse(Console.ReadLine());
                        
                        if (HowShow == 1){
                            bool response = Operations.ShowAllComputers(DBConnection);
                            
                            if (response == true){
                                Environment.Exit(0);

                            }else {
                                Environment.Exit(1);


                            }

                            

                        }else if (HowShow == 2) {
                            Console.Write("[-] Please type the ID to select and show: ");
                            int computerSelected = int.Parse(Console.ReadLine());

                            bool response = Operations.ShowComputer(DBConnection, computerSelected);

                            if (response == true){
                                Environment.Exit(0);

                            }else {
                                Environment.Exit(1);

                            }


                        }else {
                            Console.WriteLine("[-] Please enter with a int number.");
                            Environment.Exit(1);

                        }

                        
                    }catch (FormatException){
                        Console.WriteLine("[-] Please enter with a int number.");
                        Environment.Exit(1);

                    }catch (ArgumentNullException) {
                        Console.WriteLine("[-] You need to enteder with a int number to delete a Computer.");
                        Environment.Exit(1);

                    }

                }


            }catch (FormatException){
                Console.WriteLine("[-] Please select only numbers and a valid alternative.");

            }catch (OverflowException){
                Console.WriteLine("[-] Only one digit is necessary in this conext.");

            }catch (ArgumentNullException) {
                Console.WriteLine("[-] You need to choice a valid alternative.");

            }
            


        }
    }
}
