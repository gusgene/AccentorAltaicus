using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.MsSql;
using Entities.Models;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UseCases;
using UseCases.Order.Dto;
using UseCases.Order.Queries.GetById;
using Xunit;

namespace AccentorAltaicus.Tests
{
    public class OrderQueriesTests : InMemoryTestBase
    {

        private IRequestHandler<GetOrderByIdQuery, OrderDto> _handler;
        protected override void Reset()
        {
            _handler = new GetOrderByIdQueryHandler(DbContext, _mapper);

        }

        [Fact]
        public async Task GetOrderByIdQueryHandler_IdNotFoundExcept_ShouldBeOK()
        {
            var query = new GetOrderByIdQuery() { Id = 5 };
            Func<Task> act = () => _handler.Handle(query, CancellationToken.None);
            await act.Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GetOrderByIdQueryHandler_IdFoundExcept_ShouldBeOK()
        {
            const int id = 1;
            var query = new GetOrderByIdQuery() { Id = id };
            var dto = await _handler.Handle(query, CancellationToken.None);
            dto.Id.Should().Be(id);
            dto.Total.Should().Be(21);
            
        }
    }
}
