using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.IO;
using System.Data.SqlClient;

namespace MeMawKnowsBestCrime
{
    class Program
    {
        private static string _sqlConnectionString;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("MeMawConfiFile.json");

            var configuration = builder.Build();
            _sqlConnectionString = configuration["ConnectionStrings"];
            var items = File.ReadAllLines("./CrimeData_2020_2021_2022.csv").
                Skip(1)
                .Select(line => line.Split(","))
                .Select(i => new CrimeData
                {
                    Year = int.Parse(i[0]),
                    States = Convert.ToString(i[1]),
                    AgencyName = Convert.ToString(i[2]),
                    Population1 = float.Parse(i[3]),
                    TotalOffenses = float.Parse(i[4]),
                    CrimesAgainstPersons = float.Parse(i[5]),
                    CrimesAgainstProperty = float.Parse(i[6]),
                    CrimesAgainstSociety = float.Parse(i[7]),
                    AssaultOffenses = float.Parse(i[8]),
                    AggravatedAssault = float.Parse(i[9]),
                    SimpleAssault = float.Parse(i[10]),
                    Intimidation = float.Parse(i[11]),
                    HomicideOffenses = float.Parse(i[12]),
                    MurderandNonnegligentManslaughter = float.Parse(i[13]),
                    NegligentManslaughter = float.Parse(i[14]),
                    JustifiableHomicide = float.Parse(i[15]),
                    HumanTraffickingOffenses = float.Parse(i[16]),
                    CommercialSexActs = float.Parse(i[17]),
                    InvoluntaryServitude = float.Parse(i[18]),
                    KidnappingAbduction = float.Parse(i[19]),
                    SexOffenses = float.Parse(i[20]),
                    Rape = float.Parse(i[21]),
                    Sodomy = float.Parse(i[22]),
                    SexualAssaultWithanObject = float.Parse(i[23]),
                    Fondling = float.Parse(i[24]),
                    Incest = float.Parse(i[25]),
                    StatutoryRape = float.Parse(i[26]),
                    Arson = float.Parse(i[27]),
                    Bribery = float.Parse(i[28]),
                    BurglaryBreakingEntering = float.Parse(i[29]),
                    CounterfeitingForgery = float.Parse(i[30]),
                    DestructionDamageVandalismofProperty = float.Parse(i[31]),
                    Embezzlement = float.Parse(i[32]),
                    ExtortionBlackmail = float.Parse(i[33]),
                    FraudOffenses = float.Parse(i[34]),
                    FalsePretensesSwindleConfidenceGame = float.Parse(i[35]),
                    CreditCardAutomatedTellerMachineFraud = float.Parse(i[36]),
                    Impersonation = float.Parse(i[37]),
                    WelfareFraud = float.Parse(i[38]),
                    WireFraud = float.Parse(i[39]),
                    IdentityTheft = float.Parse(i[40]),
                    HackingComputerInvasion = float.Parse(i[41]),
                    LarcenyTheftOffenses = float.Parse(i[42]),
                    Pocketpicking = float.Parse(i[43]),
                    Pursesnatching = float.Parse(i[44]),
                    Shoplifting = float.Parse(i[45]),
                    TheftFromBuilding = float.Parse(i[46]),
                    TheftFromCoinOperatedMachineorDevice = float.Parse(i[47]),
                    TheftFromMotorVehicle = float.Parse(i[48]),
                    TheftofMotorVehiclePartsorAccessories = float.Parse(i[49]),
                    AllOtherLarceny = float.Parse(i[50]),
                    MotorVehicleTheft = float.Parse(i[51]),
                    Robbery = float.Parse(i[52]),
                    StolenPropertyOffenses = float.Parse(i[53]),
                    AnimalCruelty = float.Parse(i[54])



        });

            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                var insertCrimeData = @"INSERT INTO MeMawBestKnowBest.dbo.MeMawKnowsBestCrime VALUES (

                    @Year, @States, @AgencyName, @Population1, @TotalOffenses, @CrimesAgainstPersons, @CrimesAgainstProperty,
                    @CrimesAgainstSociety, @AssaultOffenses, @AggravatedAssault, @SimpleAssault, @Intimidation, 
                    @HomicideOffenses, @MurderandNonnegligentManslaughter, @NegligentManslaughter, @JustifiableHomicide,
                    @HumanTraffickingOffenses, @CommercialSexActs, @InvoluntaryServitude, @KidnappingAbduction, 
                    @SexOffenses, @Rape, @Sodomy, @SexualAssaultWithanObject, @Fondling, @Incest, @StatutoryRape,
                    @Arson, @Bribery, @BurglaryBreakingEntering, @CounterfeitingForgery, 
                    @DestructionDamageVandalismofProperty, @Embezzlement,
                    @ExtortionBlackmail, @FraudOffenses, @FalsePretensesSwindleConfidenceGame,
                    @CreditCardAutomatedTellerMachineFraud, @Impersonation, @WelfareFraud, @WireFraud, 
                    @IdentityTheft, @HackingComputerInvasion, @LarcenyTheftOffenses, @Pocketpicking,
                    @Pursesnatching, @Shoplifting, @TheftFromBuilding, @TheftFromCoinOperatedMachineorDevice,
                    @TheftFromMotorVehicle, @TheftofMotorVehiclePartsorAccessories, @AllOtherLarceny, 
                    @MotorVehicleTheft, @Robbery, @StolenPropertyOffenses, @AnimalCruelty);";

                var selectCommand = "SELECT COUNT (*) from MeMawBestKnowBest.dbo.MeMawKnowsBestCrime";
                var selectSqlCommand = new SqlCommand(selectCommand, connection);
                var results = (int)selectSqlCommand.ExecuteScalar();
                if (results > 0)
                {
                    var deleteCommand = "DELETE FROM  MeMawBestKnowBest.dbo.MeMawKnowsBestCrime";
                    var deleteSqlCommand = new SqlCommand(deleteCommand, connection);
                    deleteSqlCommand.ExecuteNonQuery();

                }
                foreach (var item in items)
                {
                    var command = new SqlCommand(insertCrimeData, connection);

                    // Adding parameters for each variable
                    command.Parameters.AddWithValue("@year", item.Year);
                    command.Parameters.AddWithValue("@states", item.States);
                    command.Parameters.AddWithValue("@agencyname", item.AgencyName);
                    command.Parameters.AddWithValue("@population1", item.Population1);
                    command.Parameters.AddWithValue("@totaloffenses", item.TotalOffenses);
                    command.Parameters.AddWithValue("@crimesagainstpersons", item.CrimesAgainstPersons);
                    command.Parameters.AddWithValue("@crimesagainstproperty", item.CrimesAgainstProperty);
                    command.Parameters.AddWithValue("@crimesagainstsociety", item.CrimesAgainstSociety);
                    command.Parameters.AddWithValue("@assaultoffenses", item.AssaultOffenses);
                    command.Parameters.AddWithValue("@aggravatedassault", item.AggravatedAssault);
                    command.Parameters.AddWithValue("@simpleassault", item.SimpleAssault);
                    command.Parameters.AddWithValue("@intimidation", item.Intimidation);
                    command.Parameters.AddWithValue("@homicideoffenses", item.HomicideOffenses);
                    command.Parameters.AddWithValue("@murderandnonnegligentmanslaughter", item.MurderandNonnegligentManslaughter);
                    command.Parameters.AddWithValue("@negligentmanslaughter", item.NegligentManslaughter);
                    command.Parameters.AddWithValue("@justifiablehomicide", item.JustifiableHomicide);
                    command.Parameters.AddWithValue("@humantraffickingoffenses", item.HumanTraffickingOffenses);
                    command.Parameters.AddWithValue("@commercialsexacts", item.CommercialSexActs);
                    command.Parameters.AddWithValue("@involuntaryservitude", item.InvoluntaryServitude);
                    command.Parameters.AddWithValue("@kidnappingabduction", item.KidnappingAbduction);
                    command.Parameters.AddWithValue("@sexoffenses", item.SexOffenses);
                    command.Parameters.AddWithValue("@rape", item.Rape);
                    command.Parameters.AddWithValue("@sodomy", item.Sodomy);
                    command.Parameters.AddWithValue("@sexualassaultwithanobject", item.SexualAssaultWithanObject);
                    command.Parameters.AddWithValue("@fondling", item.Fondling);
                    command.Parameters.AddWithValue("@incest", item.Incest);
                    command.Parameters.AddWithValue("@statutoryrape", item.StatutoryRape);
                    command.Parameters.AddWithValue("@arson", item.Arson);
                    command.Parameters.AddWithValue("@bribery", item.Bribery);
                    command.Parameters.AddWithValue("@burglarybreakingentering", item.BurglaryBreakingEntering);
                    command.Parameters.AddWithValue("@counterfeitingforgery", item.CounterfeitingForgery);
                    command.Parameters.AddWithValue("@destructiondamagevandalismofproperty", item.DestructionDamageVandalismofProperty);
                    command.Parameters.AddWithValue("@embezzlement", item.Embezzlement);
                    command.Parameters.AddWithValue("@extortionblackmail", item.ExtortionBlackmail);
                    command.Parameters.AddWithValue("@fraudoffenses", item.FraudOffenses);
                    command.Parameters.AddWithValue("@falsepretensesswindleconfidencegame", item.FalsePretensesSwindleConfidenceGame);
                    command.Parameters.AddWithValue("@creditcardautomatedtellermachinefraud", item.CreditCardAutomatedTellerMachineFraud);
                    command.Parameters.AddWithValue("@impersonation", item.Impersonation);
                    command.Parameters.AddWithValue("@welfarefraud", item.WelfareFraud);
                    command.Parameters.AddWithValue("@wirefraud", item.WireFraud);
                    command.Parameters.AddWithValue("@identitytheft", item.IdentityTheft);
                    command.Parameters.AddWithValue("@hackingcomputerinvasion", item.HackingComputerInvasion);
                    command.Parameters.AddWithValue("@larcenytheftoffenses", item.LarcenyTheftOffenses);
                    command.Parameters.AddWithValue("@pocketpicking", item.Pocketpicking);
                    command.Parameters.AddWithValue("@pursesnatching", item.Pursesnatching);
                    command.Parameters.AddWithValue("@shoplifting", item.Shoplifting);
                    command.Parameters.AddWithValue("@theftfrombuilding", item.TheftFromBuilding);
                    command.Parameters.AddWithValue("@theftfromcoinoperatedmachineordevice", item.TheftFromCoinOperatedMachineorDevice);
                    command.Parameters.AddWithValue("@theftfrommotorvehicle", item.TheftFromMotorVehicle);
                    command.Parameters.AddWithValue("@theftofmotorvehiclepartsoraccessories", item.TheftofMotorVehiclePartsorAccessories);
                    command.Parameters.AddWithValue("@allotherlarceny", item.AllOtherLarceny);
                    command.Parameters.AddWithValue("@motorvehicletheft", item.MotorVehicleTheft);
                    command.Parameters.AddWithValue("@robbery", item.Robbery);
                    command.Parameters.AddWithValue("@stolenpropertyoffenses", item.StolenPropertyOffenses);
                    command.Parameters.AddWithValue("@animalcruelty", item.AnimalCruelty);

                    command.ExecuteNonQuery();

                }

            }
        }
        public static float Parse(string value)
        {
            return float.TryParse(value, out float parsedValue) ? parsedValue : default(float);
        }
    }


}




/*
  using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.IO;
using System.Data.SqlClient;

namespace MeMawKnowsBestCrime
{
    class Program
    {
        private static string _sqlConnectionString;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("MeMawConfiFile.json");

            var configuration=builder.Build();
            _sqlConnectionString = configuration["ConnectionStrings"];
            var items = File.ReadAllLines("./CrimeData_2020_2021_2022.csv").
                Skip(1)
                .Select(line => line.Split(","))
                .Select(i => new CrimeData 
                {
                    Year = int.Parse(i[0]),
                    States = Convert.ToString(i[1]),
                    AgencyName = Convert.ToString(i[2]),
                    Population1 = int.Parse(i[3]),
                    TotalOffenses = int.Parse(i[4]),
                    CrimesAgainstPersons = int.Parse(i[5]),
                    CrimesAgainstProperty = int.Parse(i[6]),
                    CrimesAgainstSociety = int.Parse(i[7]),
                    AssaultOffenses = int.Parse(i[8]),
                    AggravatedAssault = int.Parse(i[9]),
                    SimpleAssault = int.Parse(i[10]),
                    Intimidation = int.Parse(i[11]),
                    HomicideOffenses = int.Parse(i[12]),
                    MurderandNonnegligentManslaughter = int.Parse(i[13]),
                    NegligentManslaughter = int.Parse(i[14]),
                    JustifiableHomicide = int.Parse(i[15]),
                    HumanTraffickingOffenses = int.Parse(i[16]),
                    CommercialSexActs = int.Parse(i[17]),
                    InvoluntaryServitude = int.Parse(i[18]),
                    KidnappingAbduction = int.Parse(i[19]),
                    SexOffenses = int.Parse(i[20]),
                    Rape = int.Parse(i[21]),
                    Sodomy = int.Parse(i[22]),
                    SexualAssaultWithanObject = int.Parse(i[23]),
                    Fondling = int.Parse(i[24]),
                    Incest = int.Parse(i[25]),
                    StatutoryRape = int.Parse(i[26]),
                    Arson = int.Parse(i[27]),
                    Bribery = int.Parse(i[28]),
                    BurglaryBreakingEntering = int.Parse(i[29]),
                    CounterfeitingForgery = int.Parse(i[30]),
                    DestructionDamageVandalismofProperty = int.Parse(i[31]),
                    Embezzlement = int.Parse(i[32]),
                    ExtortionBlackmail = int.Parse(i[33]),
                    FraudOffenses = int.Parse(i[34]),
                    FalsePretensesSwindleConfidenceGame = int.Parse(i[35]),
                    CreditCardAutomatedTellerMachineFraud = int.Parse(i[36]),
                    Impersonation = int.Parse(i[37]),
                    WelfareFraud = int.Parse(i[38]),
                    WireFraud = int.Parse(i[39]),
                    IdentityTheft = int.Parse(i[40]),
                    HackingComputerInvasion = int.Parse(i[41]),
                    LarcenyTheftOffenses = int.Parse(i[42]),
                    Pocketpicking = int.Parse(i[43]),
                    Pursesnatching = int.Parse(i[44]),
                    Shoplifting = int.Parse(i[45]),
                    TheftFromBuilding = int.Parse(i[46]),
                    TheftFromCoinOperatedMachineorDevice = int.Parse(i[47]),
                    TheftFromMotorVehicle = int.Parse(i[48]),
                    TheftofMotorVehiclePartsorAccessories = int.Parse(i[49]),
                    AllOtherLarceny = int.Parse(i[50]),
                    MotorVehicleTheft = int.Parse(i[51]),
                    Robbery = int.Parse(i[52]),
                    StolenPropertyOffenses = int.Parse(i[53]),
                    AnimalCruelty = int.Parse(i[54])


        });

            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                var insertCrimeData = @"INSERT INTO MeMawKnowsBestCrimeData.dbo.MeMawKnowsBestCrime VALUES (

                    @Year, @States, @AgencyName, @Population1, @TotalOffenses, @CrimesAgainstPersons, @CrimesAgainstProperty,
                    @CrimesAgainstSociety, @AssaultOffenses, @AggravatedAssault, @SimpleAssault, @Intimidation, 
                    @HomicideOffenses, @MurderandNonnegligentManslaughter, @NegligentManslaughter, @JustifiableHomicide,
                    @HumanTraffickingOffenses, @CommercialSexActs, @InvoluntaryServitude, @KidnappingAbduction, 
                    @SexOffenses, @Rape, @Sodomy, @SexualAssaultWithanObject, @Fondling, @Incest, @StatutoryRape,
                    @Arson, @Bribery, @BurglaryBreakingEntering, @CounterfeitingForgery, 
                    @DestructionDamageVandalismofProperty, @Embezzlement,
                    @ExtortionBlackmail, @FraudOffenses, @FalsePretensesSwindleConfidenceGame,
                    @CreditCardAutomatedTellerMachineFraud, @Impersonation, @WelfareFraud, @WireFraud, 
                    @IdentityTheft, @HackingComputerInvasion, @LarcenyTheftOffenses, @Pocketpicking,
                    @Pursesnatching, @Shoplifting, @TheftFromBuilding, @TheftFromCoinOperatedMachineorDevice,
                    @TheftFromMotorVehicle, @TheftofMotorVehiclePartsorAccessories, @AllOtherLarceny, 
                    @MotorVehicleTheft, @Robbery, @StolenPropertyOffenses, @AnimalCruelty);";

                var selectCommand = "SELECT COUNT (*) from MeMawKnowsBestCrimeData.dbo.MeMawKnowsBestCrime";
                var selectSqlCommand= new SqlCommand(selectCommand, connection);
                var results=(int)selectSqlCommand.ExecuteScalar();
                if (results >0)
                {
                    var deleteCommand = "DELETE FROM  MeMawKnowsBestCrimeData.dbo.MeMawKnowsBestCrime";
                    var deleteSqlCommand = new SqlCommand(deleteCommand, connection);
                    deleteSqlCommand.ExecuteNonQuery();

                }
                 foreach (var item in items) 
                {
                    var command = new SqlCommand(insertCrimeData, connection);

                    // Adding parameters for each variable
                    command.Parameters.AddWithValue("@year", item.Year);
                    command.Parameters.AddWithValue("@states", item.States);
                    command.Parameters.AddWithValue("@agencyname", item.AgencyName);
                    command.Parameters.AddWithValue("@population1", item.Population1);
                    command.Parameters.AddWithValue("@totaloffenses", item.TotalOffenses);
                    command.Parameters.AddWithValue("@crimesagainstpersons", item.CrimesAgainstPersons);
                    command.Parameters.AddWithValue("@crimesagainstproperty", item.CrimesAgainstProperty);
                    command.Parameters.AddWithValue("@crimesagainstsociety", item.CrimesAgainstSociety);
                    command.Parameters.AddWithValue("@assaultoffenses", item.AssaultOffenses);
                    command.Parameters.AddWithValue("@aggravatedassault", item.AggravatedAssault);
                    command.Parameters.AddWithValue("@simpleassault", item.SimpleAssault);
                    command.Parameters.AddWithValue("@intimidation", item.Intimidation);
                    command.Parameters.AddWithValue("@homicideoffenses", item.HomicideOffenses);
                    command.Parameters.AddWithValue("@murderandnonnegligentmanslaughter", item.MurderandNonnegligentManslaughter);
                    command.Parameters.AddWithValue("@negligentmanslaughter", item.NegligentManslaughter);
                    command.Parameters.AddWithValue("@justifiablehomicide", item.JustifiableHomicide);
                    command.Parameters.AddWithValue("@humantraffickingoffenses", item.HumanTraffickingOffenses);
                    command.Parameters.AddWithValue("@commercialsexacts", item.CommercialSexActs);
                    command.Parameters.AddWithValue("@involuntaryservitude", item.InvoluntaryServitude);
                    command.Parameters.AddWithValue("@kidnappingabduction", item.KidnappingAbduction);
                    command.Parameters.AddWithValue("@sexoffenses", item.SexOffenses);
                    command.Parameters.AddWithValue("@rape", item.Rape);
                    command.Parameters.AddWithValue("@sodomy", item.Sodomy);
                    command.Parameters.AddWithValue("@sexualassaultwithanobject", item.SexualAssaultWithanObject);
                    command.Parameters.AddWithValue("@fondling", item.Fondling);
                    command.Parameters.AddWithValue("@incest", item.Incest);
                    command.Parameters.AddWithValue("@statutoryrape", item.StatutoryRape);
                    command.Parameters.AddWithValue("@arson", item.Arson);
                    command.Parameters.AddWithValue("@bribery", item.Bribery);
                    command.Parameters.AddWithValue("@burglarybreakingentering", item.BurglaryBreakingEntering);
                    command.Parameters.AddWithValue("@counterfeitingforgery", item.CounterfeitingForgery);
                    command.Parameters.AddWithValue("@destructiondamagevandalismofproperty", item.DestructionDamageVandalismofProperty);
                    command.Parameters.AddWithValue("@embezzlement", item.Embezzlement);
                    command.Parameters.AddWithValue("@extortionblackmail", item.ExtortionBlackmail);
                    command.Parameters.AddWithValue("@fraudoffenses", item.FraudOffenses);
                    command.Parameters.AddWithValue("@falsepretensesswindleconfidencegame", item.FalsePretensesSwindleConfidenceGame);
                    command.Parameters.AddWithValue("@creditcardautomatedtellermachinefraud", item.CreditCardAutomatedTellerMachineFraud);
                    command.Parameters.AddWithValue("@impersonation", item.Impersonation);
                    command.Parameters.AddWithValue("@welfarefraud", item.WelfareFraud);
                    command.Parameters.AddWithValue("@wirefraud", item.WireFraud);
                    command.Parameters.AddWithValue("@identitytheft", item.IdentityTheft);
                    command.Parameters.AddWithValue("@hackingcomputerinvasion", item.HackingComputerInvasion);
                    command.Parameters.AddWithValue("@larcenytheftoffenses", item.LarcenyTheftOffenses);
                    command.Parameters.AddWithValue("@pocketpicking", item.Pocketpicking);
                    command.Parameters.AddWithValue("@pursesnatching", item.Pursesnatching);
                    command.Parameters.AddWithValue("@shoplifting", item.Shoplifting);
                    command.Parameters.AddWithValue("@theftfrombuilding", item.TheftFromBuilding);
                    command.Parameters.AddWithValue("@theftfromcoinoperatedmachineordevice", item.TheftFromCoinOperatedMachineorDevice);
                    command.Parameters.AddWithValue("@theftfrommotorvehicle", item.TheftFromMotorVehicle);
                    command.Parameters.AddWithValue("@theftofmotorvehiclepartsoraccessories", item.TheftofMotorVehiclePartsorAccessories);
                    command.Parameters.AddWithValue("@allotherlarceny", item.AllOtherLarceny);
                    command.Parameters.AddWithValue("@motorvehicletheft", item.MotorVehicleTheft);
                    command.Parameters.AddWithValue("@robbery", item.Robbery);
                    command.Parameters.AddWithValue("@stolenpropertyoffenses", item.StolenPropertyOffenses);
                    command.Parameters.AddWithValue("@animalcruelty", item.AnimalCruelty);

                    command.ExecuteNonQuery();  

                }

            }
        }
        public static int Parse(string value)
        {
            return int.TryParse(value, out int parsedValue) ? parsedValue : default(int);
        }
    }

   
}
*/