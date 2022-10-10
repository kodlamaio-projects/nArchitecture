using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests.Mocks.Repositories
{
    public static class MockBrandRepository
    {
        public static Mock<IBrandRepository> GetBrandRepository()
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Name =  "Mercedes"
                },
                new Brand
                {
                    Id = 2,
                    Name =  "BMW"
                },
            };

            var mockRepo = new Mock<IBrandRepository>();

            mockRepo.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Brand, bool>>>(),
                 It.IsAny<Func<IQueryable<Brand>, IOrderedQueryable<Brand>>>(),
                 It.IsAny<Func<IQueryable<Brand>, IIncludableQueryable<Brand, object>>>(),
                 It.IsAny<int>(),
                 It.IsAny<int>(),
                 It.IsAny<bool>(),
                 It.IsAny<CancellationToken>()))
          .ReturnsAsync((Expression<Func<Brand, bool>> expression,
          Func<IQueryable<Brand>, IOrderedQueryable<Brand>> orderBy,
          Func<IQueryable<Brand>, IIncludableQueryable<Brand, object>> include, int index, int size, bool enableTracking, CancellationToken cancellationToken
          ) => {
              IList<Brand> brandList;
              if (expression == null)
              {
                  brandList = brands;
              }
              else
              {
                  brandList = brands.Where(expression.Compile()).ToList();
              }

              Paginate<Brand> list = new()
              {
                  Items = brandList

              };
              return list;
          });


            mockRepo.Setup(r => r.AddAsync(It.IsAny<Brand>())).ReturnsAsync((Brand brand) =>
            {
                brands.Add(brand);
                return brand;
            });

            return mockRepo;

        }
    }
}
