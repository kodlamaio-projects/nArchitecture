using Application.Features.Auth.Rules;
using Application.Features.Users.Profiles;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Test.Application.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Org.BouncyCastle.Asn1.Ocsp;
using StarterProject.Tests.Mocks.FakeDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests.Mocks.Repositories.Auth
{
    public class MockUserRepository
    {
        public static IUserRepository GetUserMockRepository()
        {
            #region GetAsync Mock
            var mockRepo = new Mock<IUserRepository>();
            mockRepo
                .Setup(
                    s =>
                        s.GetAsync(
                            It.IsAny<Expression<Func<User, bool>>>(),
                            It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>(),
                            It.IsAny<CancellationToken>()
                        )
                )
                .ReturnsAsync(
                    (
                        Expression<Func<User, bool>> predicate,
                        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include,
                        bool withDeleted,
                        bool enableTracking,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        User user = null;
                        if (predicate != null)
                            user = UserFakeData.Data.Where(predicate.Compile()).FirstOrDefault();
                        return user;
                    }
                );
            #endregion
            return mockRepo.Object;
        }
    }
}
