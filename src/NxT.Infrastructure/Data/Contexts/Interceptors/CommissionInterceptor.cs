using Microsoft.EntityFrameworkCore.Diagnostics;
using NxT.Core.Contracts;
using NxT.Core.Models;

namespace NxT.Infrastructure.Data.Contexts.Interceptors;

public class CommissionInterceptor : IMaterializationInterceptor
{
    public object InitializedInstance(MaterializationInterceptionData data, object instance)
    {
        if (instance is Seller seller && seller.Commission is null)
            seller.Commission = new NoCommission();

        return instance;
    }
}