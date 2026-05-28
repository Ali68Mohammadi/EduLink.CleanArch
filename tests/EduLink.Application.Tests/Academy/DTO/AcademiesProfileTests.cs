using AutoMapper;
using EduLink.Application.Academies.Commands.CreateAcademy;
using EduLink.Application.Academies.Commands.UpdateAcademy;
using EduLink.Application.Academies.Dtos;
using EduLink.Domain.Entities;
using FluentAssertions;

namespace EduLink.Application.UnitTests.Academy.DTO;

public class AcademiesProfileTests

{
    private readonly IMapper _mapper;

    public AcademiesProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AcademiesProfile>());

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForAcademyToAcdemyDto_MapsCorrectly()
    {
        //arrange


        var academy = new Domain.Entities.Academy
        {
            Id = 1,
            Name = "Test Academy",
            Description = "Test Description",
            Category = "IT",
            IsOnline = true,
            ContactEmail = "test@test.com",
            Address = new Domain.Entities.Address
            {
                Street = "123 Test St",
                City = "Test City",
                PostalCode = "12345",
            },
        };

        //act
        var academyDto = _mapper.Map<AcademyDto>(academy);

        //assert
        academyDto.Should().NotBeNull();
        academyDto.Id.Should().Be(academy.Id);
        academyDto.Name.Should().Be(academy.Name);
        academyDto.Description.Should().Be(academy.Description);
        academyDto.Category.Should().Be(academy.Category);
        academyDto.IsOnline.Should().Be(academy.IsOnline);
        academyDto.Street.Should().Be(academy.Address?.Street);
        academyDto.City.Should().Be(academy.Address?.City);
        academyDto.PostalCode.Should().Be(academy.Address?.PostalCode);

    }

    [Fact]
    public void CreateMap_ForCreateAcademyCommandToAcademy_MapsCorrectly()
    {
        //arrange


        var command = new CreateAcademyCommand
        {
            Name = "Test Academy",
            Description = "Test Description",
            Category = "IT",
            IsOnline = true,
            ContactEmail = "test@test.com",
            ContactNumber = "12345678932",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12345",
        };

        //act
        var academy = _mapper.Map<Domain.Entities.Academy>(command);

        //assert
        academy.Should().NotBeNull();
        academy.Name.Should().Be(academy.Name);
        academy.Description.Should().Be(academy.Description);
        academy.Category.Should().Be(academy.Category);
        academy.IsOnline.Should().Be(academy.IsOnline);
        academy.ContactEmail.Should().Be(academy.ContactEmail);
        academy.ContactNumber.Should().Be(academy.ContactNumber);
        academy.Address.Should().NotBeNull();
        academy.Address?.Street.Should().Be(command.Street);
        academy.Address?.City.Should().Be(command.City);
        academy.Address?.PostalCode.Should().Be(command.PostalCode);

    }

    [Fact]
    public void CreateMap_ForUpdateAcademyCommandToAcademy_MapsCorrectly()
    {
        //arrange


        var command = new UpdateAcademyCommand
        {
            Id = 1,
            Name = "Update Test Academy",
            Description = "Update Test Description",
            IsOnline = true,
        };

        //act
        var academy = _mapper.Map<Domain.Entities.Academy>(command);

        //assert
        academy.Should().NotBeNull();
        academy.Id.Should().Be(academy.Id);
        academy.Name.Should().Be(academy.Name);
        academy.Description.Should().Be(academy.Description);
        academy.IsOnline.Should().Be(academy.IsOnline);


    }
}