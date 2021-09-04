namespace RoofStock.Data.DTO
{
    public class AddressResponse : BaseResponse
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int ZipPlus4 { get; set; }
    }
}