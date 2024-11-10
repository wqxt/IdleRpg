using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystemTests
{
    private Pawn _pawn;
    private PawnHealth _pawnHealth;
    private HealthView _healthView;
    private HealthObserver _healthObserver;

    [SetUp]
    public void Setup()
    {
        // Инициализируем Pawn и его компоненты
        _pawn = new GameObject().AddComponent<Pawn>();
        _pawn.PawnConfiguration = ScriptableObject.CreateInstance<PawnConfiguration>();
        _pawnHealth = new PawnHealth(_pawn);

        // Инициализируем HealthView и его компоненты
        _healthView = new GameObject().AddComponent<HealthView>();
        var maxHealthText = new GameObject().AddComponent<TextMeshProUGUI>();
        var currentHealthText = new GameObject().AddComponent<TextMeshProUGUI>();
        var healthSlider = new GameObject().AddComponent<Slider>();
        _healthView.Initialize(currentHealthText, maxHealthText, healthSlider);

        // Инициализируем HealthObserver и его компоненты
        _healthObserver = new HealthObserver();
        _healthObserver._healthView = new[] { _healthView };

        // Устанавливаем начальные значения
        _pawn.PawnConfiguration.Type = "Character";
        _pawn.PawnConfiguration.MaxHealthValue = 100;
        _pawn.PawnConfiguration.CurrentHealthValue = 100;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_pawn.gameObject);
        Object.DestroyImmediate(_pawn.PawnConfiguration);
        Object.DestroyImmediate(_healthView.gameObject);
        _healthObserver = null;

    }

    [Test]
    public void PawnHealthChangesWhenTakeDamage()
    {
        // Arrange
        int initPawnHealth = _pawn.PawnConfiguration.CurrentHealthValue;
        int damage = 200;

        // Act
        _pawnHealth.TakeDamage(damage, "Enemy"); // Наносим урон типу Character от типа Enemy

        // Assert
        Assert.Less(_pawn.PawnConfiguration.CurrentHealthValue, initPawnHealth, "Health value did not decrease after taking damage."); // Проверяем, что значение здоровья уменьшилось
        Assert.GreaterOrEqual(_pawn.PawnConfiguration.CurrentHealthValue, 0, "Health value should not be less than 0."); // Проверяем, что значение здоровья не отрицательно
    }

    [Test]
    public void PawnHealthViewChangesWhenTakeDamage()
    {
        // Arrange
        int initialPawnHealth = _pawn.PawnConfiguration.CurrentHealthValue;
        _healthView.SetupHealth(initialPawnHealth);
        _healthView.tag = "Character";
        int damage = 200;

        // Act
        _pawnHealth.TakeDamage(damage, "Enemy");
        _healthObserver.UpdatePawnHealthView(_pawn.PawnConfiguration.CurrentHealthValue, _pawn.PawnConfiguration.Type);

        // Получаем значения после урона через публичные свойства
        int updatedHealthTextValue = _healthView.CurrentHealthTextValue;
        float updatedHealthSliderValue = _healthView.HealthSliderValue;

        // Assert
        Assert.Less(updatedHealthTextValue, initialPawnHealth, "Health text did not decrease after taking damage."); // Проверяем, что отображение значения здоровья уменьшилось
        Assert.GreaterOrEqual(updatedHealthTextValue, 0, "Health text should not be less than 0."); // Проверяем, что отображение значения здоровья не отрицательно


        Assert.Less(updatedHealthSliderValue, initialPawnHealth, "Health slider did not decrease after taking damage."); // Проверяем, что отображение значения слайдера здоровья уменьшилось
        Assert.GreaterOrEqual(updatedHealthSliderValue, 0, "Health slider should not be less than 0.");// Проверяем, что отображение значения слайдера здоровья не отрицательно
    }

    [Test]
    public void TriggersPawnDeathEventWhenHealthZero()
    {
        // Arrange
        _pawn.PawnConfiguration.CurrentHealthValue = 10;
        _pawn.PawnConfiguration.Type = "Character";
        bool deathEventTrigger = false;
        _pawnHealth.PawnDeath += type => deathEventTrigger = true;

        // Act
        _pawnHealth.TakeDamage(10, "Enemy");

        // Assert
        Assert.IsTrue(deathEventTrigger); // Проверяем, что триггер смерти работает корректно
    }
}