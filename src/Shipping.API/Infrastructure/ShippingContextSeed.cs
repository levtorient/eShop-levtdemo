namespace eShop.Shipping.API.Infrastructure;

public class ShippingContextSeed : IDbSeeder<ShippingContext>
{
    // Must match the ID used in Identity.API UsersSeed for the "shipper" user
    private const string DefaultShipperUserId = "a1b2c3d4-e5f6-7890-abcd-ef1234567890";

    public async Task SeedAsync(ShippingContext context)
    {
        // Seed default shipper if none exists
        if (!await context.Shippers.AnyAsync())
        {
            var defaultShipper = new Shipper(
                name: "Default Shipper",
                phone: "1234567890",
                userId: DefaultShipperUserId,
                currentWarehouseId: 1
            );

            context.Shippers.Add(defaultShipper);
            await context.SaveChangesAsync();
        }
    }
}
