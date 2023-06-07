using AutoMapper;
using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Exceptions;
using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Driver;
using FleetMonitorAPI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FleetMonitorAPI.Services
{
    public class DriverService : IDriverService
    {
        private readonly FleetMonitorDbContext _dbContext;
        private readonly IMapper _mapper;
        public DriverService(FleetMonitorDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public long Create(CreateDriverDto dto)
        {
            var driver = _mapper.Map<Driver>(dto);
            _dbContext.Drivers.Add(driver);
            _dbContext.SaveChanges();
            return driver.Id;
        }

        public void Delete(long id)
        {
            var driver = _dbContext
                .Drivers
                .FirstOrDefault(x => x.Id == id);

            if (driver is null)
                throw new NotFoundException(ExceptionDictionary.DriverNotFound);


            _dbContext.Drivers.Remove(driver);
            _dbContext.SaveChanges();

        }

        public PageResult<DriverDto> GetAll(PaginationQuery query)
        {
            var baseQuery = _dbContext
                .Drivers
                .Include(d =>d.Devices)
                .Where(d=> query.Phrase == null ||
                (d.FirstName.ToLower().Contains(query.Phrase.ToLower())) || 
                (d.LastName.ToLower().Contains(query.Phrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Driver, object>>>()
                {
                    {nameof(Driver.Id), d => d.Id},
                    {nameof(Driver.FirstName), d => d.FirstName},
                    {nameof(Driver.LastName), d => d.LastName}
                };

                var selectedColumn = columnSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var drivers = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItem = baseQuery.Count();
            var deviceDtos = _mapper.Map<List<DriverDto>>(drivers);
            var result = new PageResult<DriverDto>(deviceDtos, totalItem, query.PageSize, query.PageNumber);
            return result;
        }

        public long GetAmountOfDevices()
        {
            var amount = _dbContext.Drivers.Count();
            return amount;
        }

        public IEnumerable<DriverDto> GetList()
        {
            var drivers = _dbContext
                .Drivers
                .ToList();

            var result = _mapper.Map<List<DriverDto>>(drivers);
            return result;
        }

        public void Update(UpdateDriverDto dto, long id)
        {
            var driver = _dbContext
                .Drivers
                .FirstOrDefault(d => d.Id == id);

            if (driver is null)
                throw new NotFoundException(ExceptionDictionary.DriverNotFound);

            driver.FirstName = dto.FirstName;
            driver.LastName = dto.LastName;

            _dbContext.SaveChanges();
        }
    }
}
