using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using TheHungerGames.Models;
using TheHungerGames.Util;

namespace TheHungerGames.ModelProviders;

public class KleiJsonModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        try
        {
            await DecodeJsonBody(bindingContext);
        }
        catch
        {
            await DecodeKleiBody(bindingContext);
        }
    }

    private static async Task DecodeKleiBody(ModelBindingContext bindingContext)
    {
        var body = await bindingContext.HttpContext.Request.ReadBodyAsync();
        var json = await body.KleiEncodingToJson();
        var result = JsonConvert.DeserializeObject<List<AnalyticsEvent>>(json);
        
        bindingContext.Result = ModelBindingResult.Success(result);
    }

    private static async Task DecodeJsonBody(ModelBindingContext bindingContext)
    {
        var body = await bindingContext.HttpContext.Request.ReadBodyAsync();
        var result = JsonConvert.DeserializeObject<List<AnalyticsEvent>>(body);
        bindingContext.Result = ModelBindingResult.Success(result);
    }
}