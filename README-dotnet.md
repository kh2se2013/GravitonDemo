在 Graviton 运行 dotnet 应用程序

# 搭建运行环境

```
$ sudo yum install dotnet-sdk-8.0 -y
$ dotnet --version
8.0.117
```

通过以上步骤，即可完成 dotnet 8 环境搭建

# 运行应用程序

1. 获取项目代码并启动应用程序

```
cd ~/GravitonDemo/graviton-demo-dotnet
dotnet restore
dotnet run --urls http://0.0.0.0:5009
```

2. 通过浏览器访问上一步部署的应用（http://EC2-IP:5009，留意 Web 服务端口在安全组已开放）。运行正常的话，可以看到 Web 应用返回了实例相关的信息。