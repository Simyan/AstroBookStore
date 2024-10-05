using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Order;
using Marten;
using Marten.Events;
using Marten.Events.Daemon.Internals;
using Marten.Events.Projections;
using Marten.Storage;
using Xunit;

namespace Tests
{
    public class OrderTests 
    {
        [Fact]
        public void Sanity()
        {
            int i = 1 + 1;
            Assert.Equal(2, i);
        }

        [Fact]
        public async Task RebuildOrderSummaryProjection()
        {
            using var store = DocumentStore.For(options =>
            {
                options.Connection(
                        "Host=localhost;Database=Astro;Username=postgres;Password=Gems@2009"
                    );
                options.Projections.Add<OrderSummaryProjection>(ProjectionLifecycle.Async);
            });

            using var daemon = await store.BuildProjectionDaemonAsync();
            await daemon.RebuildProjectionAsync<OrderSummaryProjection>(CancellationToken.None);
            await using var query = store.QuerySession();

            var result = await query.LoadAsync<OrderSummary>("9/7/2024");
            Assert.NotNull(result);
        }
    }
}
