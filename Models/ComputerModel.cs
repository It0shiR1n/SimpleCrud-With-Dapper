using ComputerStore;

using System;

using Dapper;
using Microsoft.Data.SqlClient;

namespace ComputerStore{
    public class Computer{
        public int ComputerID { get; set; }

        public string? Motherboard { get; set; }

        public int CPUCores { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public decimal Price { get; set; }

        public Computer() {
            if (Motherboard == null){
                this.Motherboard = "";

            }

            if (CPUCores == null) {
                this.CPUCores = 0;

            }

        }

    }

    public class Operations {
        public static bool InsertComputer(SqlConnection ConnDB, Computer newComputer) {

            string queryInsert = @"INSERT INTO TablesDB.TBComputer
            (Motherboard, CPUCores, ReleaseDate, Price) 
            VALUES 
            (@Motherboard, @CPUCores, @ReleaseDate, @Price)";

            var InsertComputer = new {
                Motherboard = newComputer.Motherboard,
                CPUCores = newComputer.CPUCores,
                ReleaseDate = newComputer.ReleaseDate,
                Price = newComputer.Price,

            };

            int result = ConnDB.Execute(queryInsert, InsertComputer);

            if (result != 0){
                return true;

            }else {
                return false;

            }

        }

        public static bool RemoveComputer(SqlConnection ConnDB, int idComputer){

            try {
                string queryCheck = "SELECT IDComputer FROM TablesDB.TBComputer WHERE IDComputer = "+idComputer;
                ConnDB.QuerySingle(queryCheck);

                try {
                    string queryRemove = "DELETE FROM TablesDB.TBComputer WHERE IDComputer = "+idComputer;
                    ConnDB.Execute(queryRemove, idComputer);
                    
                    return true;

                }catch (InvalidOperationException){
                    return false;

                }

                

            }catch (InvalidOperationException){
                return false;

            }
        
            
            
        
            
            

        }

        public static bool UpdateComputer(SqlConnection ConnDB, int PcDb, int ColumnUpdated, dynamic information){
            try {
                string queryCheck = "SELECT IDComputer FROM TablesDB.TBComputer WHERE IDComputer = @id";
                var queryValues = new {
                    id = PcDb,

                };

                ConnDB.QuerySingle(queryCheck, queryValues);

                try {
                    if (ColumnUpdated == 1){
                        string queryUpdate = "UPDATE TablesDB.TBComputer SET Motherboard = @info WHERE IDComputer = @idpc";
                        var queryValuesUp = new {
                            info = information.ToString(),
                            idpc = PcDb,

                        };

                        ConnDB.Execute(queryUpdate, queryValuesUp);
                        return true;


                    }else if (ColumnUpdated == 2){
                        string queryUpdate = "UPDATE TablesDB.TBComputer SET CPUCores = @info WHERE IDComputer = @idpc";
                        var queryValuesUp = new {
                            info = int.Parse(information), 
                            idpc = PcDb,

                        };

                        ConnDB.Execute(queryUpdate, queryValuesUp);
                        return true;



                    }else if (ColumnUpdated == 3){
                        string queryUpdate = "UPDATE TablesDB.TBComputer SET ReleaseDate = @info WHERE IDComputer= @idpc";
                        DateTime informationParsed;

                        if (DateTime.TryParse(information, out informationParsed)){
                            var queryValuesUp = new {
                                info = informationParsed, 
                                idpc = PcDb,

                            };

                            ConnDB.Execute(queryUpdate, queryValuesUp);
                            return true;

                        }else {
                            return false;

                        }


                    }else if (ColumnUpdated == 4){
                        string queryUpdate = "UPDATE TablesDB.TBComputer SET Price = @info WHERE IDComputer = @idpc";
                        var queryValuesUp = new {
                            info = decimal.Parse(information),
                            idpc = PcDb,

                        };

                        ConnDB.Execute(queryUpdate, queryValuesUp);
                        return true;


                    }else {
                        return false;

                    }

                }catch (System.InvalidOperationException){
                    return false;

                }

            }catch (System.InvalidOperationException) {
                return false;

            }

        }

        public static bool ShowAllComputers(SqlConnection ConnDB){
            
            try {
                string queryShowAll = "SELECT IDComputer, Motherboard, CPUCores, ReleaseDate, Price  FROM TablesDB.TBComputer";
                IEnumerable<dynamic> response = ConnDB.Query<dynamic>(queryShowAll);

                foreach (var computer in response){
                    string price = computer.Price.ToString("0.00");

                    Console.WriteLine($"ID: {computer.IDComputer}");
                    Console.WriteLine($"Motherboard: {computer.Motherboard}");
                    Console.WriteLine($"CPU Cores: {computer.CPUCores}");
                    Console.WriteLine($"Release: {computer.ReleaseDate}");
                    Console.WriteLine($"Price: {price}");
                        
                    Console.WriteLine("");
                    Console.WriteLine("============================================");
                    
                }

                return true;

            }catch (System.InvalidOperationException){
                return false;

            }


        }

        public static bool ShowComputer(SqlConnection ConnDB, int ShowComputer){
            try {
                string queryCheck = "SELECT IDComputer FROM TablesDB.TBComputer WHERE IDComputer = "+ShowComputer;
                ConnDB.QuerySingle(queryCheck);

                try {
                    string queryShow = "SELECT IDComputer, Motherboard, CPUCores, ReleaseDate, Price FROM TablesDB.TBComputer WHERE IDComputer = "+ShowComputer;
                    dynamic response = ConnDB.QuerySingle(queryShow);

                    string price = response.Price.ToString("0.00");

                    Console.WriteLine($"ID: {response.IDComputer}");
                    Console.WriteLine($"Motherboard: {response.Motherboard}");
                    Console.WriteLine($"CPU Cores: {response.CPUCores}");
                    Console.WriteLine($"Release: {response.ReleaseDate}");
                    Console.WriteLine($"Price: {price}");
                    return true;


                } catch (System.InvalidOperationException){
                    return false;

                }

            }catch (System.InvalidOperationException){
                return false;

            }
        }
    }
}