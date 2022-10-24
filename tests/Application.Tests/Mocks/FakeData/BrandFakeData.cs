using Core.Test.Application.FakeData;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Tests.Mocks.FakeData
{
    public class BrandFakeData : BaseFakeData<Brand>
    {
        public override List<Brand> CreateFakeData()
        {
            var data = new List<Brand>
            {
                new()
                {
                    Id = 1,
                    Name =  "Mercedes"
                },
                new()
                {
                    Id = 2,
                    Name =  "BMW"
                }
            };
            return data;
        }
    }
}
