namespace AirportRESRfulApi.BLL.Tests
{
    using System;
    using NUnit.Framework;
    using FakeItEasy;
    using AirportRESRfulApi.BLL;
    using AirportRESRfulApi.DAL.Interfaces;
    using FluentValidation;
    using AutoMapper;
    using Shared.DTO;
    using AirportRESRfulApi.DAL.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    [TestFixture]
    public class TicketsServiceTests
    {
        private  IUnitOfWork unitOfWork;
        private  IRepository<Ticket> repository;
        private  IMapper mapper;
        private  IValidator<TicketDto> validator;

        private List<Ticket> ticketList;

        public TicketsServiceTests()
        {
            unitOfWork = A.Fake<IUnitOfWork>();
            repository = A.Fake<IRepository<Ticket>>();
            validator = A.Fake<IValidator<TicketDto>>();

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<TicketDto, Ticket>();
            }).CreateMapper();

            A.CallTo(unitOfWork)
                .Where(call => call.Method.Name == "Set")
                .WithReturnType<IRepository<Ticket>>()
                .Returns(repository);
        } 

        [SetUp]
        public void Init()
        {
            ticketList = new List<Ticket>
                 {
                    new Ticket
                    { Id = 1, Flight = null, FlightId = 1,IsSold = false, PlaseNumber = 1,FlightNumber = "TY26", Price = 200},
                    new Ticket
                    { Id = 2, Flight = null, FlightId = 1,IsSold = false, PlaseNumber = 2,FlightNumber = "TY26", Price = 200},
                    new Ticket
                    { Id = 3, Flight = null, FlightId = 1,IsSold = true, PlaseNumber = 3,FlightNumber = "QW11", Price = 200},
                    new Ticket
                    { Id = 4, Flight = null, FlightId = 1,IsSold = true, PlaseNumber = 4,FlightNumber = "QW11", Price = 200}
                 };
        }

        private void SetCollection(int id)
        {
            A.CallTo(repository)
               .Where(call => call.Method.Name == "Get")
               .WithReturnType<IEnumerable<Ticket>>()
               .Returns(ticketList.Where(x => x.Id == id));
        }


        [TestCase(1)]
        [TestCase(2)]
        public void BuyById_TicketId_TicketIsSold_true(int id)
        {
            SetCollection(id);

            Services.TicketsService ts = new Services.TicketsService(unitOfWork, mapper, validator);

            var result = ts.BuyById(id);

            Assert.IsTrue(result.IsSold);
        }

        [TestCase(3)]
        [TestCase(4)]
        public void BuyById_TicketId_Ticket_null(int id)
        {
            SetCollection(id);

            Services.TicketsService ts = new Services.TicketsService(unitOfWork, mapper, validator);

            var result = ts.BuyById(id);

            Assert.IsNull(result);
        }

        [TestCase(4)]
        [TestCase(3)]
        public void ReturnById_TicketId_TicketIsSold_true(int id)
        {
            SetCollection(id);

            Services.TicketsService ts = new Services.TicketsService(unitOfWork, mapper, validator);

            var result = ts.ReturnById(id);

            Assert.IsFalse(result.IsSold);
        }

        [TestCase(2)]
        [TestCase(1)]
        public void ReturnById_TicketId_Ticket_null(int id)
        {
            SetCollection(id);

            Services.TicketsService ts = new Services.TicketsService(unitOfWork, mapper, validator);

            var result = ts.ReturnById(id);

            Assert.IsNull(result);
        }

        public void ReturnById_TicketId_Ticket_null(int id)
        {
            SetCollection(id);

            Services.TicketsService ts = new Services.TicketsService(unitOfWork, mapper, validator);

            var result = ts.ReturnById(id);

            Assert.IsNull(result);
        }

        public void BuyById_TicketId_Ticket_null(int id)
        {
            SetCollection(id);

            Services.TicketsService ts = new Services.TicketsService(unitOfWork, mapper, validator);

            var result = ts.BuyById(id);

            Assert.IsNull(result);
        }


    }
}
