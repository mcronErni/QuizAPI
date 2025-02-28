using NUnit.Framework;
using Moq;
using QuizAPI;
using QuizAPI.Controllers;
using AutoMapper;
using QuizAPI.Contract.Interface;
using Microsoft.Extensions.Configuration;


namespace QuizAPI.UnitTest
{
    [TestFixture]
    public sealed class AuthControllerTests
    {

        private readonly Mock<IAuthRepository> _authRepoMock = new();
        private readonly Mock<IBootcamperRepository> _bootcamperRepoMock = new();
        private readonly Mock<IMentorRepository> _mentorRepoMock = new();
        private readonly Mock<IConfiguration> _configMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private readonly AuthController _controller;


        public void TestMethod1()
        {
        }
    }
}
