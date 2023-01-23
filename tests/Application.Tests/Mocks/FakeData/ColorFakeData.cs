using Core.Test.Application.FakeData;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Tests.Mocks.FakeData
{
    public class ColorFakeData : BaseFakeData<Color>
    {
        public override List<Color> CreateFakeData()
        {
            var data = new List<Color> {
                new() {
                    Id = 1,
                    Name = "Red"
                },
                new() {
                    Id = 2,
                    Name = "Blue"
                }
            };
            return data;
        }
    }
}