using AutoMapper;
using FluentValidation;
using NLog;
using RestSharp;
using UnitConversion;

//.Net Standard library examples

Console.WriteLine("-----------------------#1 AutoMapper-----------------------");
var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<Person, PersonDto>(); });
var mapper = configuration.CreateMapper();
Person person = new Person();
person.Name = "Vova";
var personDto = mapper.Map<PersonDto>(person);
Console.WriteLine(personDto.Name);

Console.WriteLine("-----------------------#2 NLog-----------------------");
var logConfig = new NLog.Config.LoggingConfiguration();
logConfig.AddRule(LogLevel.Trace, LogLevel.Fatal, new NLog.Targets.ConsoleTarget("logconsole"));
LogManager.Configuration = logConfig;
Logger log = LogManager.GetCurrentClassLogger();
log.Debug("This is a debug message");
log.Error(new Exception(), "This is an error message");
log.Fatal("This is a fatal message");

Console.WriteLine("-----------------------#3 UnitConversion-----------------------");
var kgToLbs = new MassConverter("kg", "lbs");
double kgValue = 75;
double lbValue = kgToLbs.LeftToRight(kgValue);
Console.WriteLine(kgValue + " kg = " + lbValue + " lbs");

Console.WriteLine("-----------------------#4 FluentValidation-----------------------");
PersonValidator personValidator = new PersonValidator();
personValidator.Validate(person);
person.Name = "Vo";
var result = personValidator.Validate(person);
Console.WriteLine(result);

Console.WriteLine("-----------------------#5 RestSharp-----------------------");
var restClient = new RestClient("https://jsonplaceholder.typicode.com/comments");
var request = new RestRequest()
    .AddQueryParameter("postId", "1");
Console.WriteLine(restClient.Get<Object>(request));

class Person
{
    private string name;

    public string Name
    {
        get => name;
        set => name = value;
    }
}

class PersonDto
{
    private string name;

    public string Name
    {
        get => name;
        set => name = value;
    }
}

class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name");
        RuleFor(x => x.Name).Length(3, 20);
    }
}