using RoofStock.Data.DTO;
using System.Threading.Tasks;

namespace RoofStock.Data.Contracts
{
    public interface IAddressData
    {
        Task<AddressResponse> GetAddressByPropertyID(int id);
    }
}