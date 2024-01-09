using GameLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace UI.Pages;

public class IndexModel : PageModel
{
    private const string DbUrl = "http://localhost:5221/GetRandomMonster";
    private const string BalPath = "http://localhost:5221/Game";
    public List<Round> FightLog { get; set; } = null!;
    public Monster? Monster { get; set; }
    
    [BindProperty]
    public Player? Player { get; set; } = new Player();
    
    public async Task OnPost()
    {
        var client = new HttpClient();
        var monster = await client.GetFromJsonAsync<Monster>(DbUrl);

        Monster = monster;
      
        var fight = new Fight { Player = Player, Monster = monster };
        
        var fightResponse = await client.PostAsJsonAsync(BalPath, fight);

        Console.WriteLine(fightResponse);
        var responseContent = await fightResponse.Content.ReadAsStringAsync();

        var list = JsonConvert.DeserializeObject<List<Round>>(responseContent);
        
        FightLog = list!;
        foreach (var item in list!)
        {
            foreach (var itemRound in item.Rounds!)
            {
                Console.WriteLine(itemRound.Message);
            }
        }
    }
}