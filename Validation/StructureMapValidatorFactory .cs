using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;

namespace CQRS_with_Redis.Validation
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        private readonly HttpConfiguration _configuration;

    public StructureMapValidatorFactory(HttpConfiguration configuration)
    {
        _configuration = configuration;
    }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}
