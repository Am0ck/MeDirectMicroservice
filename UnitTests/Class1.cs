using MeDirectMicroservice.Controllers;
using MeDirectMicroservice.Data;
using MeDirectMicroservice.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
namespace UnitTests
{
    public class Class1
    {
        //[SetUp]
        //public void Setup()
        //{
        //    private readonly IMemoryCache _memoryCache;
        //    private readonly IConfiguration _config;
        //    private readonly ILogger<ExchangeTradesController> _logger;
        //    private readonly ApplicationDbContext _context;
        //    //ExchangeTradesController etc = new ExchangeTradesController(_context, _memoryCache, _logger, _config);
        //}
        //[Test]
        //public void LimitTest()
        //{
        //    //etc.Limit
        //}
        [Test]
        public void UnixTimeStampToDateTime_Works()
        {
            DateTime dt2 = Utilities.UnixTimeStampToDateTime(946686630);
            Assert.That(new DateTime(2000, 1, 1, 1, 30, 30), Is.EqualTo(dt2));
        }
        [Test]
        public void UnixTimeStampToDateTime_Unequal()
        {
            //DateTime dt = new DateTime(2000, 1, 1, 1, 30, 30);
            DateTime dt2 = Utilities.UnixTimeStampToDateTime(94668663);
            Assert.That(DateTime.Now, Is.GreaterThanOrEqualTo(dt2));
        }
    }
}