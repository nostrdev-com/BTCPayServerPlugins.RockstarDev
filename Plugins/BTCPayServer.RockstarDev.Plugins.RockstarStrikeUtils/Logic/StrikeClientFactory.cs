using System;
using System.Linq;
using System.Threading.Tasks;
using BTCPayServer.RockstarDev.Plugins.RockstarStrikeUtils.Data;
using BTCPayServer.RockstarDev.Plugins.RockstarStrikeUtils.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Strike.Client;

namespace BTCPayServer.RockstarDev.Plugins.RockstarStrikeUtils.Logic;

public class StrikeClientFactory(
    RockstarStrikeDbContextFactory strikeDbContextFactory,
    IServiceProvider serviceProvider,
    ILoggerFactory loggerFactory)
{
    private string DbKey => DbSettingKeys.StrikeApiClient.ToString();
    
    public async Task<bool> TestAndSaveApiKeyAsync(string apiKey)
    {
        var client = InitClient(apiKey);

        try
        {
            // Test the client with a simple request
            var balances = await client.Balances.GetBalances();
            if (!balances.IsSuccessStatusCode)
            {
                var logger = loggerFactory.CreateLogger<StrikeClientFactory>();
                logger.LogInformation($"The connection failed, check API key. Error: {balances.Error?.Data?.Code} {balances.Error?.Data?.Message}");
                return false;
            }

            await using var db = strikeDbContextFactory.CreateContext();
            var setting = await db.Settings.SingleOrDefaultAsync(a => a.Key == DbKey);

            if (setting is null)
                db.Settings.Add(new DbSetting { Key = DbKey, Value = apiKey });
            else
                setting.Value = apiKey;

            await db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ClientExistsAsync()
    {
        await using var db = strikeDbContextFactory.CreateContext();
        var apiKey = db.Settings.SingleOrDefault(a => a.Key == DbKey)?.Value;

        return apiKey != null;
    }


    public async Task<StrikeClient> ClientCreateAsync()
    {
        await using var db = strikeDbContextFactory.CreateContext();
        var apiKey = db.Settings.SingleOrDefault(a => a.Key == DbKey)?.Value;
        if (apiKey is null)
        {
            throw new InvalidOperationException("API key not found in the database.");
        }

        return InitClient(apiKey);
    }

    private StrikeClient InitClient(string apiKey)
    {
        var client = serviceProvider.GetRequiredService<StrikeClient>();
        client.ApiKey = apiKey;
        //client.Environment = environment;
        client.ThrowOnError = false;

        //if (serverUrl != null)
        //    client.ServerUrl = serverUrl;
        return client;
    }
}