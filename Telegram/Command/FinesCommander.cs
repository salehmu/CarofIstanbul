using Models.DataModels;
using Services;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    private readonly TypeManager<Bill> _billManager = new();

    private async Task AddType(Update update, TypeOfBill typeOfBill)
    {
        var bill = new Bill(type: typeOfBill);
        await _client.SendMessageAsync(update.ChatId(), Arabic.EnterPrice);
        (update, bill.Price) = await ReadPrice(update);
        await _client.SendMessageAsync(update.ChatId(), Arabic.EnterPicture);
        (_, bill.Image) = await ReadPicture(update);
        await _billManager.Add(bill);
    }

    public async Task Fine(Update update) => await AddType(update, TypeOfBill.Fine);
    public async Task HGS(Update update) => await AddType(update, TypeOfBill.HGS);
    public async Task RegularMaintenance(Update update) => await AddType(update, TypeOfBill.RegularMaintenance);
    
    public async Task CycleMaintenance(Update update) => await AddType(update, TypeOfBill.CycleMaintenance);
}