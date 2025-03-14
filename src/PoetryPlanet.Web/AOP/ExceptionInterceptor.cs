using System.Reflection;
using System.Text.Json;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;

namespace PoetryPlanet.Web;

/// <summary>
/// 实现对方法的 AOP 异常统一处理，
/// 如果发生异常，记录异常并返回方法签名的默认值。
/// 可以通过以下几种方式将拦截器应用到服务的方法上
/// 1. [assembly: ExceptionInterceptor(AttributeTargetTypes = "PoetryPlanet.Web.Services.*")]
/// 2. [assembly: ExceptionInterceptor(AttributeTargetTypes = "PoetryPlanet.Web.Services.DebugService")]
/// 3. [ExceptionInterceptor] MethodName()
/// </summary>
[PSerializable]
[MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Public)]
public class ExceptionInterceptor : MethodInterceptionAspect
{
    public override async Task OnInvokeAsync(MethodInterceptionArgs args)
    {
        try
        {
            await args.ProceedAsync();
        }
        catch (Exception e)
        {
            var fullName = "";
            try
            {
                var message = $"调用方法 {args.Method.ReflectedType}.{args.Method.Name} 时发生异常：{e.Message}";
                Console.WriteLine(message);

                // 如果异步方法返回值类型为 Task，则返回一个已完成的任务
                var methodInfo = (MethodInfo)args.Method;
                if (methodInfo.ReturnType.Name == "Task")
                {
                    args.ReturnValue = Task.CompletedTask;
                    return;
                }

                // 如果异步方法返回值类型为 Task<T> ，则返回对应内部类型的默认值
                var innerType = methodInfo.ReturnType.GetGenericArguments()[0];
                object? value = null;

                // 如果泛型为可空类型，则返回 Task<null>
                if (innerType.Name == "Nullable`1")
                {
                    args.ReturnValue = await Task.FromResult(value);
                    return;
                }

                // 否则返回对应的类型的默认值 Task<default>
                fullName = innerType.FullName;
                value = Activator.CreateInstance(innerType);
                Console.WriteLine($"为类型 {fullName} 创建默认实例 {JsonSerializer.Serialize(value)}");
                args.ReturnValue = await Task.FromResult(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"为类型 {fullName} 创建默认返回值时发生错误：{ex.Message}");
            }
        }
    }

    public override void OnInvoke(MethodInterceptionArgs args)
    {
        var fullName = "";
        try
        {
            args.Proceed();
        }
        catch (Exception e)
        {
            var message = $"调用方法 {args.Method.ReflectedType}.{args.Method.Name} 时发生异常（{e.Message}），";

            try
            {
                var methodInfo = (MethodInfo)args.Method;

                if (methodInfo.ReturnType.Name == "Void") return;
                if (methodInfo.ReturnType.Name == "Nullable`1") return;

                fullName = methodInfo.ReturnType.FullName;

                args.ReturnValue = Activator.CreateInstance(methodInfo.ReturnType);
                message += $"为该方法返回类型为 {fullName} 的默认值：{JsonSerializer.Serialize(args.ReturnValue)}";
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                message += $"，为该方法返回类型 {fullName} 创建默认值时发生错误（{ex.Message}），返回 null";
                Console.WriteLine(message);
            }
        }
    }
}