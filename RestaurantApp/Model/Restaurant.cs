using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace RestaurantApp.Model;

public class Restaurant
{
    public string Name { get; }
    public string Address { get; }
    public string PhoneNumber { get; }
    public string DirectorFullname { get; }

    [JsonConstructor]
    private Restaurant(string name, string address, string phoneNumber, string directorFullname)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        DirectorFullname = directorFullname;
    }

    public class Builder
    {
        private string? _name;
        private string? _address;
        private string? _phoneNumber;
        private string? _director;

        public Builder SetName(string name)
        {
            _name = Validator.RequireNotBlank(name, nameof(name));
            return this;
        }

        public Builder SetAddress(string address)
        {
            _address = Validator.RequireNotBlank(address, nameof(address));
            return this;
        }

        public Builder SetPhoneNumber(string phoneNumber)
        {
            _phoneNumber = Validator.RequireNumeric(Validator.RequireNotBlank(phoneNumber, nameof(phoneNumber)), nameof(phoneNumber));
            return this;
        }

        public Builder SetDirectorFullname(string director)
        {
            _director = Validator.RequireNotBlank(director, nameof(director));
            return this;
        }
        
        public Restaurant Build()
        {
            var name = Validator.RequireNotNull(_name, nameof(_name));
            var address = Validator.RequireNotNull(_address, nameof(_address));
            var phone = Validator.RequireNotNull(_phoneNumber, nameof(_phoneNumber));
            var director = Validator.RequireNotNull(_director, nameof(_director));

            return new Restaurant(name, address, phone, director);
        }
    }
}