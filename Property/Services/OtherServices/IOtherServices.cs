using Property.Models;

namespace Property.Services.OtherServices
{
    public interface IOtherServices
    {
        (bool, int) CheckIfInteger(string number);

	}
}
