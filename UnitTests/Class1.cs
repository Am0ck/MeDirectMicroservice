using MeDirectMicroservice.Controllers;
using MeDirectMicroservice.Data;
using MeDirectMicroservice.DataAccess.Interfaces;
using MeDirectMicroservice.DataAccess.Repositories;
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