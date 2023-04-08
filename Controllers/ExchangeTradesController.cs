using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeDirectMicroservice.Data;
using MeDirectMicroservice.Models;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace MeDirectMicroservice.Controllers
{
    //public string _baseUrl = "https://api.apilayer.com/exchangerates_data/convert?";
    public class ExchangeTradesController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        private readonly ApplicationDbContext _context;
        private string baseUrl = "https://api.apilayer.com/exchangerates_data/";
        private string cacheKey = "currencyCacheKey";
        //                          https://api.apilayer.com/exchangerates_data/symbols
        //private string baseUrl = "https://api.apilayer.com/exchangerates_data/convert?";
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }


        public ExchangeTradesController(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        // GET: ExchangeTrades
        public async Task<IActionResult> Index()
        {
              return _context.ExchangeTrades != null ? 
                          View(await _context.ExchangeTrades.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ExchangeTrades'  is null.");
        }

        // GET: ExchangeTrades/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ExchangeTrades == null)
            {
                return NotFound();
            }

            var exchangeTrade = await _context.ExchangeTrades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeTrade == null)
            {
                return NotFound();
            }

            return View(exchangeTrade);
        }

        // GET: ExchangeTrades/Create
        public IActionResult Create()
        {
            //List<Currency> list = new List<Currency>();
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Currency> currencies))
            {
                Console.WriteLine("Found currencies in Cache");
            }
            else 
            {
                //List<Currency> list = new List<Currency>();
                List<string> list = new List<string>();
                var client = new RestClient(baseUrl + "symbols");
                //client.Timeout = -1;

                var request = new RestRequest();
                request.AddHeader("apikey", "0VLlhN0Nmh11ulWksWNhl1C9OPqY1DSh");

                var response = client.Execute(request);
                Console.WriteLine(response.Content);
                dynamic rsp = JObject.Parse(response.Content);
                Console.WriteLine(response.Content);
                Console.WriteLine(rsp.symbols.ToString());
                //foreach
                //currencies = JsonConvert.DeserializeObject<List<Currency>>(rsp.symbols);
                var cd = JsonConvert.DeserializeObject<Dictionary<string,string>>(rsp.symbols.ToString());
               //JObject jo = JObject.Parse(rsp.symbols.ToString());
                foreach (var key in cd.Keys)
                {
                    //Currency curr = new Currency();
                    //curr.CurrencySymbol = key;
                    //curr.CountryName = cd[key];
                    list.Add(cd[key]+" ("+key+")");
                    Console.WriteLine(key+" : " + cd[key]);
                }
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal);
                _memoryCache.Set(cacheKey, list, cacheEntryOptions);
                ViewBag.symbols = list.AsEnumerable();
                return View();
            }
           
            return View(currencies);
        }

        // POST: ExchangeTrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExchangeTrade exchangeTrade, string CurrencyFrom, string CurrencyTo)
        {
            //https://api.apilayer.com/exchangerates_data/convert?
            Console.WriteLine("CurrencyFrom: " + CurrencyFrom.Split('(', ')')[1]);
            Console.WriteLine("CurrencyTo: " + CurrencyTo.Split('(', ')')[1]);
            string endpoint = baseUrl + "convert?to=" + CurrencyTo.Split('(', ')')[1] + "&from=" + CurrencyFrom.Split('(', ')')[1] + "&amount=" + exchangeTrade.AmountToTrade;
            //var client = new RestClient(baseUrl+ "convert?to=" + exchangeTrade.CurrencyTo+"&from="+exchangeTrade.CurrencyFrom+"&amount="+exchangeTrade.AmountToTrade);
            Console.WriteLine("ENDPOINT: " + endpoint);
            var client = new RestClient(endpoint);
            //client.Timeout = -1;

            var request = new RestRequest();
            request.AddHeader("apikey", "0VLlhN0Nmh11ulWksWNhl1C9OPqY1DSh");

            var response = client.Execute(request);
            Console.WriteLine(response.Content);
            dynamic rsp = JObject.Parse(response.Content);
            Console.WriteLine(response.Content);
            Console.WriteLine(rsp.result);
            Console.WriteLine(rsp.info.timestamp);
            Console.WriteLine(UnixTimeStampToDateTime((Double) rsp.info.timestamp));
            exchangeTrade.TradedAmount = rsp.result;
            exchangeTrade.ExchangeTime = UnixTimeStampToDateTime((Double)rsp.info.timestamp);


            if (ModelState.IsValid)
            {
                _context.Add(exchangeTrade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exchangeTrade);
        }

        // GET: ExchangeTrades/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ExchangeTrades == null)
            {
                return NotFound();
            }

            var exchangeTrade = await _context.ExchangeTrades.FindAsync(id);
            if (exchangeTrade == null)
            {
                return NotFound();
            }
            return View(exchangeTrade);
        }

        // POST: ExchangeTrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,AmountToTrade,CurrencyFrom,CurrencyTo,TradedAmount,ExchangeTime")] ExchangeTrade exchangeTrade)
        {
            if (id != exchangeTrade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exchangeTrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExchangeTradeExists(exchangeTrade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exchangeTrade);
        }

        // GET: ExchangeTrades/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ExchangeTrades == null)
            {
                return NotFound();
            }

            var exchangeTrade = await _context.ExchangeTrades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeTrade == null)
            {
                return NotFound();
            }

            return View(exchangeTrade);
        }

        // POST: ExchangeTrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ExchangeTrades == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExchangeTrades'  is null.");
            }
            var exchangeTrade = await _context.ExchangeTrades.FindAsync(id);
            if (exchangeTrade != null)
            {
                _context.ExchangeTrades.Remove(exchangeTrade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExchangeTradeExists(long id)
        {
          return (_context.ExchangeTrades?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ClearCache()
        {
            _memoryCache.Remove(cacheKey);

            return RedirectToAction("Index");
        }
    }
}
