using System;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using TrainingTask.Data;
using TrainingTask.Data.EF;

namespace TrainingTask.Tests.CoreTests.EFTest
{
    [TestFixture]
    public abstract class TestFixtureDbBase
    {
        [SetUp]
        public void Setup()
        {
            UnitOfWork = new EFUnitOfWorkFactory(
                    new TrainingTaskDbContext(new DbContextOptionsBuilder<TrainingTaskDbContext>()
                        .UseSqlServer(
                            "Data Source=QWS-PFR-10;Initial Catalog=TrainingTaskDb;User ID=ttuser;Password=ttuser;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                        ).Options
                    ),
                    new MapperConfiguration(opt => opt.AddMaps(AppDomain.CurrentDomain.GetAssemblies())).CreateMapper())
                .CreateUnitOfWork();
        }

        [TearDown]
        public void Teardown()
        {
            UnitOfWork.Rollback();
        }

        protected UnitOfWork UnitOfWork { get; private set; }
    }
}