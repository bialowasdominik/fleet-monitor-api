using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Exceptions;
using FleetMonitorAPI.Models.Queries;
using FluentValidation;

namespace FleetMonitorAPI.Models.Validators
{
    public class DeviceQueryValidator : AbstractValidator<PaginationQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        private string[] allowedSortColumn = { nameof(Device.Id), nameof(Device.Name) };
        public DeviceQueryValidator()
        {
            RuleFor(d => d.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(d => d.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                        context.AddFailure("PageSize", ExceptionDictionary.WrongItemRange);
                });

            RuleFor(d => d.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortColumn.Contains(value))
                .WithMessage(ExceptionDictionary.SortIsOptional);
        }
    }
}
