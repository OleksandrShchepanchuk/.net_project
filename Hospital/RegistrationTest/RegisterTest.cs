using Hospital.Controllers;

namespace RegistrationTest;

public class RegisterTest
{
    [Test]
    [TestCase("yakyis-maksym99@lnu.edu.ua",true)]
    [TestCase("htos_hz_hto@lnu.edu.ua",true)]
    [TestCase("example@mail.box",true)]
    [TestCase("wrong!address@nodomain",false)]
    [TestCase("@example.com",false)]
    [TestCase("user@",false)]
    [TestCase("user@ex^ample.com",false)]
    [TestCase("user@[example.com]",false)]
    [TestCase("user@example@com",false)]
    [TestCase("invalidemail.com",false)]
    [TestCase("user@examples",false)]
    public void Validate_Email(string email,bool expectedResult)
    {
        bool isValid = UsersController.validateEmail(email);
        Assert.AreEqual(expectedResult,isValid);
       
    }
}