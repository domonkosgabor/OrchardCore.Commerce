using OrchardCore.Commerce.AddressDataType;
using OrchardCore.ContentManagement;

namespace OrchardCore.Commerce.Fields;

public class AddressField : ContentField
{
    public Address Address { get; set; } = new();
    public string UserAddressToSave { get; set; }
}
