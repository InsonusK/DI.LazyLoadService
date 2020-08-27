# DI.LazyLoadService
Extension for IServiceCollection: registration lazy load services

Add methods AddLazySingleton, AddLazyScoped, AddLazyTransient

```csharp
class ClassWithLazyDependency
{
    public ClassWithLazyDependency(Lazy<ISomeClass> someClass)
    {
    }
}

var _sc = new ServiceCollection();
_sc.AddTransient<ClassWithLazyDependency>();
_sc.AddLazySingleton<ISomeClass, SomeClass>();
var _classWithLazyDependency = _sp.GetRequiredService<ClassWithLazyDependency>();
```
