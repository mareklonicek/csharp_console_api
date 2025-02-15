using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace httpClient_api
{
    class Program
    {
        private static readonly HttpClient _client = new HttpClient();

        static async Task Main()
        {
            var service = new UserService();

            while (true)
            {
                // Zobraz nabídku a čekej na vstup uživatele
                  var today = DateTime.Now.ToString("dd.MM.yyyy");
                Console.WriteLine("WELCOME IN TESTING MY .NET C# CLI API APP \n");
                Console.WriteLine("1. GET Users / Získat všechny uživatele");
                Console.WriteLine("2. GET User by ID / Získat uživatele zadáním ID (1-10)");
                // Console.WriteLine("3. CREATE User");
                // Console.WriteLine("4. UPDATE User");
                // Console.WriteLine("5. DELETE User");
                Console.WriteLine("3. Zobrazit aktuální měnové kurzy České národní banky. Dnes je "+today+".\n");
                Console.WriteLine("Chci ukončit program: zadej ano/ne\n");
                Console.Write("VYBER OPERACI: zadej 1 nebo 2 nebo 3...\n\n");
                var choice = Console.ReadLine();

                if (choice.ToLower() == "ano")
                {
                    Console.WriteLine("\nDěkuji za vyzkoušení mého C# CLI API programu.\n\nStiskněte libovolnou klávesu pro ukončení programu...");
               
                    Console.ReadKey(true);
                    break; // Ukončí smyčku
                }

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await service.GetUsers();
                            break;
                        case "2":
                            Console.Write("Zadej ID: ");
                            int id = int.Parse(Console.ReadLine()!);
                            await service.GetUser(id);
                            break;
                        // case "3":
                        //     await service.CreateUser(new User { Name = "Nový Uživatel", Email = "test@email.com" });
                        //     break;
                        // case "4":
                        //     Console.Write("Zadej ID k aktualizaci: ");
                        //     int updateId = int.Parse(Console.ReadLine()!);
                        //     await service.UpdateUser(updateId, new User { Name = "Upravený Uživatel", Email = "update@email.com" });
                        //     break;
                        // case "5":
                        //     Console.Write("Zadej ID k smazání: ");
                        //     int deleteId = int.Parse(Console.ReadLine()!);
                        //     await service.DeleteUser(deleteId);
                        //     break;
                        case "3":
                            await GetCurrencyRates();
                            break;
                        default:
                            Console.WriteLine("Neplatná volba.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Došlo k chybě: {ex.Message}");
                }

                // Čekání na vstup před dalším opakováním nabídky
                Console.WriteLine("Stiskněte libovolnou klávesu pro pokračování...");
                Console.ReadKey();
                Console.Clear(); // Vymaže obrazovku
            }
        }

        // Metoda pro získání měnových kurzů
        static async Task GetCurrencyRates()
        {
            try
            {
                var url = "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt?date=DD.MM.RRRR";
                var response = await _client.GetStringAsync(url);
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při získávání měnových kurzů: {ex.Message}");
            }
        }
    }
}
