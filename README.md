# SEMS

## 简介
SMES是基于EF Code First模式的Web框架的个人最佳实践,  

## 相关技术
* 技术框架：.NET Framework 4.5
* 技术平台：ASP.NET MVC5 + WebAPI
* 数据存储：EntityFramework 6
* 数据序列化：使用JSON.NET作为JSON序列化的主要工具
* 数据映射：AutoMapper，主要用于数据传输对象DTO与数据实体模型Model之间的相互转化，免于繁杂的对象属性赋值
* IoC组件：SimpleInjector，全局批量注册，用于处理IoC接口与实现的批量映射，实现接口调用与实现的解耦
* 日志记录：定义通用日志记录接口与基础API，日志输出方式可以使用现成的任意日志组件（如log4net）
* 前端：基于BootStrap Metronic的MD规范的模板+AngularJS,调用WebApi