  > сделать авторизацию, аутенфикацию и идентификацию
  > добавление изображений для Customer и Item
  > написать интеграционные тесты
  > добавить кэширование
  > завернуть в Docker
  > добавить OpenTelemetry
  > fix namespaces
  > попробовать AutoMapper/Manual mapping
---
# Основная текущая задача
  > попробовать FluentValidation
  > refactor validation in ItemEntity
  > пофиксить DI
  > integration tests

System.AggregateException: "Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateTitle Lifetime: Transient ImplementationType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateTitle': Unable to resolve service for type 'Microsoft.Extensions.Logging.ILogger' while attempting to activate 'Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateTitle'.) (Error while validating the service descriptor 'ServiceType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateDescription Lifetime: Transient ImplementationType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateDescription': Unable to resolve service for type 'Microsoft.Extensions.Logging.ILogger' while attempting to activate 'Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateDescription'.) (Error while validating the service descriptor 'ServiceType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdatePrice Lifetime: Transient ImplementationType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdatePrice': Unable to resolve service for type 'Microsoft.Extensions.Logging.ILogger' while attempting to activate 'Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdatePrice'.) (Error while validating the service descriptor 'ServiceType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateQuantity Lifetime: Transient ImplementationType: Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateQuantity': Unable to resolve service for type 'Microsoft.Extensions.Logging.ILogger' while attempting to activate 'Store.App.CQRS.Items.Commands.Update.UpdateChain.UpdateQuantity'.) (Error while validating the service descriptor 'ServiceType: Store.App.CQRS.Items.Commands.Update.UpdateChain.RefreshUpdatedAt Lifetime: Transient ImplementationType: Store.App.CQRS.Items.Commands.Update.UpdateChain.RefreshUpdatedAt': Unable to resolve service for type 'Microsoft.Extensions.Logging.ILogger' while attempting to activate 'Store.App.CQRS.Items.Commands.Update.UpdateChain.RefreshUpdatedAt'.)"