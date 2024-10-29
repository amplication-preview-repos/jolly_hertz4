using Microsoft.AspNetCore.Mvc;
using MoniteBackendService.APIs.Common;
using MoniteBackendService.Infrastructure.Models;

namespace MoniteBackendService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ProductFindManyArgs : FindManyInput<Product, ProductWhereInput> { }
