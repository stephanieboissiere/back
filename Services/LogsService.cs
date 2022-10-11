using AMO_4.Data;
using AMO_4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMO_4.Services
{
    public class LogsService
    {

        //private readonly MyWebApiContext _context;
        //public LogsService(MyWebApiContext context)
        //{

        //    _context = context;

        //}
        //    public async Task<List<Log>> GetAsync() => await _context.Logs.Include(b => b.produit).Include(c => c.user).ToListAsync();


        //    public async Task<List<Log>> GetDate(string dateStart, string dateEnd)
        //    {
        //        DateTime datefilterStart;
        //        DateTime datefilterEnd;
        //        if (DateTime.TryParse(dateStart, out datefilterStart) && DateTime.TryParse(dateEnd, out datefilterEnd))
        //        {

        //            return await _context.Logs.Find(x => x.date_heure >= datefilterStart && x.date_heure < datefilterEnd).SortByDescending(p => p.date_heure).ToListAsync();
        //        }
        //        return await _context.Logs.Find(x => x.date_heure >= DateTime.Today && x.date_heure < DateTime.Today.AddDays(1)).Limit(2000).ToListAsync();


        //    }
        //    //requete pour charger les logs du jour
        //    public async Task<List<Log>> GetDateNow()
        //    {
        //        return await _context.Logs.Find(x => x.date_heure >= DateTime.Today && x.date_heure < DateTime.Today.AddDays(1)).ToListAsync();
        //    }

        //    //requete pour rechercher un mot
        //    public async Task<List<Log>> GetWord(string word)
        //    {
        //        return await _context.Logs.Find(x => x.description.Contains(word)).ToListAsync();
        //    }

        //    // public async Task<List<Log>> GetAsync() => await _logsCollection.Find(_ => true).ToListAsync();
           //public async Task<Log?> GetAsync(int id) => await _context.Logs.FindAsync(id);
        //    public async Task CreateAsync(Log newLog) => await _context.Logs.InsertOneAsync(newLog);
        //public async Task RemoveAsync(int id) => _context.Logs.Remove(log); await _context.SaveChangesAsync();
        //await _context.SaveChangesAsync();
        //    public async Task UpdateAsync(int id, Log updatedLog) => await _context.Logs.ReplaceOneAsync(x => x.Id == id, updatedLog);


        //   // public Dictionary<string, IProcess> Process = new Dictionary<string, IProcess>()
        //   //{
        //   //        { "Date", new ProcessOrderByDate()},
        //   //        { "Criticite", new ProcessOrderByCriticite()},
        //   //        { "LogId", new ProcessOrderByLogId()},
        //   //        { "ErrorCode", new ProcessOrderByErrorCode()},
        //   //        { "Version", new ProcessOrderByVersion()}
        //   // };
        //    //public async Task<List<Log>> GetLogList(string orderparams)
        //    //{
        //    //    var logs = await GetAsync();

        //    //    return Process[orderparams].process(logs);


        //    //}




    }
}
