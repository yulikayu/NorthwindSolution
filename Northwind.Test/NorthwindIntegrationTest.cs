using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Contracts.Dto.Category;
using Northwind.Domain.Base;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Persistence.Base;
using Northwind.Services;
using Northwind.Services.Abstraction;
using Northwind.Test.Mapping;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Northwind.Test
{
    public class NorthwindIntegrationTest
    {
        private static IConfigurationRoot Configuration;
        private static DbContextOptionsBuilder<NorthwindContext> optionsBuilder;

        private static MapperConfiguration mapperConfig;
        private static IMapper mapper;
        private static IServiceProvider serviceProvider;
        private static IRepositoryManager repositoryManager;

        public NorthwindIntegrationTest()
        {
            BuilderConfiguration();
            SetupOptions();
        }

        /*   [Fact]
           public void TestGetCategoryService()
           {
               using (var context = new NorthwindContext(optionsBuilder.Options))
               {
                   repositoryManager = new RepositoryManager(context);
                   IServiceManager serviceManager = new ServiceManager(repositoryManager, mapper);

                   var category = serviceManager.CategoryService.GetAllCategory(false);

                   //assert
                   category.ShouldNotBeNull();
                   category.Result.Count().ShouldBe(9);
               }
           }*/

        /*[Fact]
        public void TestCreateCategoryService()
        {
            using (var context = new NorthwindContext(optionsBuilder.Options))
            {
                repositoryManager = new RepositoryManager(context);
                IServiceManager serviceManager = new ServiceManager(repositoryManager, mapper);

                var categoryDto = new CategoryForCreateDto
                {
                    CategoryName = "Toys",
                    Description = "Mainan Anak"
                };
                serviceManager.CategoryService.Insert(categoryDto);

                //assert
                categoryDto.CategoryName.ShouldSatisfyAllConditions();
            }
        }*/

        [Fact]
        public void TestGetCategoryRepo()
        {
            using (var context = new NorthwindContext(optionsBuilder.Options))
            {
                //act
                repositoryManager = new RepositoryManager(context);
                var category = repositoryManager.CategoryRepository.GetAllCategory(false);

                //assert
                category.ShouldNotBeNull();
                category.Result.Count().ShouldBe(9);
            }
        }

        /*[Fact]
        public void TestCreateCategoryRepo()
        {
            using (var context = new NorthwindContext(optionsBuilder.Options))
            {
                //act
                repositoryManager = new RepositoryManager(context);

                //define model category
                var categoryModel = new Category
                {
                    CategoryName = "Movie",
                    Description = "Movie Entertaiment"
                };

                repositoryManager.CategoryRepository.Insert(categoryModel);
                repositoryManager.Save();

                categoryModel.CategoryId.ShouldBeEquivalentTo(9);

                //assert
                var category = repositoryManager.CategoryRepository.GetAllCategory(false);

                category.ShouldNotBeNull();
                category.Result.Count().ShouldNotBe(10);
            }
        }*/
        public void BuilderConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private void SetupOptions()
        {
            optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("NorthwindDb"));

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            serviceProvider = services.BuildServiceProvider();

            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();
        }
    }
}
