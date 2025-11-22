# SubscribesAndDiscounts

Учебный проект по функциональному синтаксису C# в рамках курса ООП.


## Цель проекта

Познакомиться с современными функциональными возможностями C# (версия 14, .NET 10) и применить их для упрощения объектно-ориентированного кода без нарушения принципов ООП.


## Описание

Проект реализует систему расчёта стоимости подписки с учётом:
- Статуса подписчика (Trial, Base, Pro, Student)
- Стажа пользователя (влияет на прогрессивные скидки)
- Региона (налоговые ставки для EU и US)
- Количества устройств (доплата за ≥3 устройства)

Исходный императивный код с множеством `if/else` и временных переменных был переписан с использованием функциональных конструкций C#.


## Применённые функциональные возможности C#

### 1. **Primary Constructor** (C# 12+)
Компактное объявление конструктора прямо в заголовке класса:
```csharp
public class Subscriber(string id, string region, SubscriptionStatus status)
```


### 2. **Выражения-члены (Expression-bodied Members)**
Свойства с валидацией через тернарные операторы и throw-выражения:
```csharp
public string Id { get; } = string.IsNullOrWhiteSpace(id) 
    ? throw new ArgumentException("Id required") 
    : id;
```


### 3. **Switch-выражения с Pattern Matching**
Замена громоздких `if/else` на декларативные switch-выражения:
```csharp
private static double ApplyStatusDiscount(SubscriptionStatus status, int tenure, double basePrice)
=> status switch
{
    SubscriptionStatus.Trial => 0,
    SubscriptionStatus.Student => basePrice * 0.5,
    SubscriptionStatus.Pro => tenure switch
    {
        >= 24 => basePrice * 0.85,
        >= 12 => basePrice * 0.9,
        _ => basePrice
    },
    SubscriptionStatus.Base => basePrice,
    _ => throw new ArgumentException("Invalid subscription status")
};
```


### 4. **Enum вместо строк**
Типобезопасность через перечисление `SubscriptionStatus`:
```csharp
public enum SubscriptionStatus : byte
{
    Trial, Base, Pro, Student
}
```


### 5. **Локальные функции**
Композиция бизнес-логики через именованные локальные функции:
```csharp
public static double CalcTotal(Subscriber s)
{
    ArgumentNullException.ThrowIfNull(s);
    
    double PriceAfterStatus() => ApplyStatusDiscount(s.Status, s.TenureMonths, s.BasePrice);
    double WithDevices(double x) => ApplyDeviceTax(s.Devices, x);
    double WithTax(double x) => ApplyTax(s.Region, x);
    
    return WithTax(WithDevices(PriceAfterStatus()));
}
```


### 6. **Инициализаторы объектов**
Создание коллекций с инициализацией свойств:
```csharp
var subscribers = new List<Subscriber>
{
    new Subscriber("A-1", "EU", SubscriptionStatus.Trial) {
        TenureMonths = 0, Devices = 1, BasePrice = 9.99 },
    new Subscriber("B-2", "US", SubscriptionStatus.Pro) {
        TenureMonths = 18, Devices = 4, BasePrice = 14.99 }
};
```


### 7. **Guard Clauses**
Лаконичная проверка аргументов:
```csharp
ArgumentNullException.ThrowIfNull(s);
```


## Сборка и запуск (Arch Linux)

### Требования
- .NET SDK 10.0


### Команды
```bash
# Сборка проекта
dotnet build
# После чего запускаем исполняемый файл

# Запуск проекта
dotnet run
```


### Особенности сборки
В проекте явно указан `RuntimeIdentifier` для корректной работы на Arch Linux:

```xml
<RuntimeIdentifier>linux-x64</RuntimeIdentifier>
```

Если ваша ОС не Linux, рекомендуется удалить эту строку для автоматического определения Runtime Identifier


## Структура проекта

```
SubscribesAndDiscounts/
├── Program.cs           # Точка входа с демонстрационными данными
├── Subscriber.cs        # Модель подписчика с валидацией
├── BillingService.cs    # Сервис расчёта стоимости
└── SubscribesAndDiscounts.csproj
```


## Результат

Код стал:
- **Компактнее** — сократился на ~40% строк
- **Декларативнее** — читается как набор бизнес-правил, а не процедурных шагов
- **Безопаснее** — валидация на уровне типов и свойств
- **Поддерживаемее** — изменения локализованы, легко расширять
